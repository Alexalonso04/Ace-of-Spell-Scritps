using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PatrolCheckpoint {
    public Transform checkpoint;
    public float stayTime;
}

public class EnemyController : MonoBehaviour {
    

    public GameObject deathParticle;
 
    [Header("Enemy Drop")]
    [SerializeField]
    public GameObject item;
    public float dropRate;
    public GameObject itemParticle; 
    // Start is called before the first frame update
    [Header("Enemy Health")]
    public int health;


    [Header("Behaviour")]
    public bool patrol;
    public PatrolCheckpoint[] patrolPoints;
    private int patrolIndex;
    public float stoppingDistance;
    private float nextCheckpointTime;

    private Transform _player;
    private Vector2 originalPosition;
    private Quaternion originalRotation;
    private NavMeshAgent agent;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        originalPosition = new Vector2(transform.position.x, transform.position.z);
        //Vector3.ProjectOnPlane(transform.position, new Vector3(0.0f, 1.0f, 0.0f));
        originalRotation = transform.rotation;
        patrolIndex = 0;
    }

    private void attack() {
    }

    private void Update() {
        if (patrol && _player == null) {
            Patrol(patrolPoints);
        } else {
            PlayerFollow();
        }
    }


    public void takeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            HUDController.Instance.UpdateHealth("Enemy", health);
            StartCoroutine(enemyDeath());
        } else {
            HUDController.Instance.UpdateHealth("Enemy", health);
            Debug.Log(health);
        }
    }

    // Dropped item has a chance to appear, based on Drop Rate.
    // If dropped spell is already in the scene, item will not drop until item is removed.
    public void dropItem(){
        int randNum = Random.Range(0,100);
            
        if(!GameObject.Find(item.name)){
            Debug.Log("Item found");
            if(randNum <= dropRate){
                Instantiate(itemParticle, transform.position, Quaternion.identity);
                GameObject droppedItem = Instantiate(item, transform.position, Quaternion.identity);
                droppedItem.name = item.name;
            }
        }else{
            Debug.Log("Item exists lol");
        }
        
    }

    private IEnumerator enemyDeath(){
        yield return new WaitForSeconds(0.1f);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        dropItem();
        Destroy(gameObject);
    }
    

    public void setPlayerLocation(Transform playerPosition) {
        _player = playerPosition;
    }

    private void PlayerFollow() {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.z);
        patrol = false;
        agent.stoppingDistance = stoppingDistance; 
        if (_player != null) {
            agent.SetDestination(_player.position);
        } else if (originalPosition != currentPosition) {
            Debug.Log("Original Position:" + originalPosition);
            patrol = true;
            agent.SetDestination(originalPosition);
        }

        if (currentPosition == originalPosition) {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.time * 0.003f);
        }
    }

    private void Patrol(PatrolCheckpoint[] patrolLocations) {
        Vector3 currentPosition;
        Vector3 targetPosition;
        agent.stoppingDistance = 0;
        if (patrol) {
            currentPosition = new Vector3(transform.position.x, 1.0f, transform.position.z);
            targetPosition = patrolLocations[patrolIndex].checkpoint.transform.position;
            Debug.Log("Next position: " + targetPosition);
            if (currentPosition != targetPosition && Time.time > nextCheckpointTime) {
                agent.SetDestination(targetPosition);
            } else if (currentPosition == targetPosition) {
                nextCheckpointTime = Time.time + patrolLocations[patrolIndex].stayTime;
                patrolIndex++;
            }

            if (patrolIndex == patrolLocations.Length) {
                patrolIndex = 0;
            }
        }
     }
}

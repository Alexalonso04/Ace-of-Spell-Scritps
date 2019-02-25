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
    // Start is called before the first frame update
    [Header("Enemy Stats")]
    public int health;
    public int attackPower;
    public float attackSpeed;
    private float _nextAttack;


    [Header("Behaviour")]
    public bool patrol;
    public PatrolCheckpoint[] patrolPoints;
    private int patrolIndex;
    private float originalStoppingDistance;
    private float nextCheckpointTime;

    public Transform playerTransform;
    private PlayerController _playerController;
    private Vector2 originalPosition;
    private Quaternion originalRotation;
    private NavMeshAgent agent;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        originalPosition = new Vector2(transform.position.x, transform.position.z);
        //Vector3.ProjectOnPlane(transform.position, new Vector3(0.0f, 1.0f, 0.0f));
        originalRotation = transform.rotation;
        patrolIndex = 0;
        originalStoppingDistance = agent.stoppingDistance;
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void attack() {
        if (Time.time > _nextAttack) {
            _nextAttack = Time.time + attackSpeed;
            if (_playerController != null) {
                Debug.Log("Can attack");
                _playerController.TakeDamage(attackPower);
            }
        }
    }

    private void Update() {
        if (patrol) {
            Patrol(patrolPoints);
        } else {
            PlayerFollow();
            if (playerTransform != null) {
                if (Vector3.Distance(transform.position, playerTransform.position) <= originalStoppingDistance) {
                    attack();
                }
            }
        }
        // if (agent.remainingDistance == 5) {
        //     Debug.Log(agent.remainingDistance + " " + agent.stoppingDistance);
        //     attack();
        // }
    }

    public void takeDamage(int damage) {
        health -= damage;
        HUDController.Instance.UpdateHealth("Enemy", health);
        if (health <= 0) {
            Destroy(gameObject);
        } 
    }

    public void setPlayer(GameObject player) {
        playerTransform = player.transform;
        //_playerController = player.GetComponent<PlayerController>();
    }

    private void PlayerFollow() {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.z);
        if (playerTransform != null) {
            agent.stoppingDistance = originalStoppingDistance; 
            agent.SetDestination(playerTransform.position);
         } else if (originalPosition != currentPosition) {
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
        Debug.Log("Patrolling");
        //if (patrol) {
            currentPosition = new Vector3(transform.position.x, 1.0f, transform.position.z);
            targetPosition = patrolLocations[patrolIndex].checkpoint.transform.position;
            if (currentPosition != targetPosition && Time.time > nextCheckpointTime) {
                agent.SetDestination(targetPosition);
            } else if (currentPosition == targetPosition) {
                nextCheckpointTime = Time.time + patrolLocations[patrolIndex].stayTime;
                patrolIndex++;
            }

            if (patrolIndex == patrolLocations.Length) {
                patrolIndex = 0;
            }
        //}
    }
}

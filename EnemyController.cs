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
        HUDController.Instance.UpdateHealth("Enemy", health);
        if (health <= 0) {
            Destroy(gameObject);
        } 
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

        /*
        if (loop) {
            for (int i = 0; i < patrolLocations.Length; i++) {
                float waitTime = patrolLocations[i].stayTime;
                Vector3 checkpoint = patrolLocations[i].checkpoint.transform.position;
                checkpoint.y = 0;
                agent.SetDestination(destination);

                while ()

                if (i == patrolLocations.Length - 2) {
                    i = -1;
                }
            }
        }
        */
    }
}

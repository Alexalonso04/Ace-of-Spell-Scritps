using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrigger : MonoBehaviour
{
    private EnemyController enemy;
    public float followDelay;
    // Start is called before the first frame update

    private void Awake() {
        enemy = gameObject.GetComponentInParent(typeof(EnemyController)) as EnemyController;
    }

    private void OnTriggerEnter(Collider other) {
        int layerMask = 1 << 2;
        layerMask = ~layerMask;
        if (other.tag == "Player") {
            transform.parent.LookAt(other.transform);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask)) {
                Debug.Log("Hit the player");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Player" && enemy.playerTransform == null) {
                    enemy.setPlayer(other.gameObject);
                    enemy.patrol = false;
                } else if (hit.collider.tag == "Player"){
                    enemy.patrol = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            enemy.patrol = true;
        }
    }
}
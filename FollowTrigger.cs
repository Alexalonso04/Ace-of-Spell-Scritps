using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrigger : MonoBehaviour
{
    private EnemyController enemy;
    // Start is called before the first frame update

    private void Awake() {
        enemy = gameObject.GetComponentInParent(typeof(EnemyController)) as EnemyController;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            transform.parent.LookAt(other.transform);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)) {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider.tag != "Wall") {
                    enemy.setPlayer(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            enemy.setPlayer(null);
        }
    }
}

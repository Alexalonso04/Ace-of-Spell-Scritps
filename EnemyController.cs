using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    // Start is called before the first frame update
    [Header("Enemy Health")]
    public int health;

    private void attack() {

    }

    public void takeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            HUDController.Instance.UpdateHealth("Enemy", health);
            Destroy(gameObject);
        } else {
            HUDController.Instance.UpdateHealth("Enemy", health);
            Debug.Log(health);
        }
    }
}

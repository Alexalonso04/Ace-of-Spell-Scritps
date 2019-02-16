using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float timeToLive;

    [Header("Damage Controller")]
    public int damage;

    void Start() {
        damage = PlayerController.damageToGive;
        Debug.Log("Damage is:  "+damage);
        StartCoroutine(destroyAfter(timeToLive));
    }

    void Update(){

    }

    IEnumerator destroyAfter(float time) {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other) {
        if (other.collider.tag == "Enemy") {
            other.collider.GetComponent<EnemyController>().takeDamage(damage);
        }
         if(other.collider.tag == "Enemy" || other.collider.tag == "Environment" ){
        	Destroy(gameObject);
        }
    }
}

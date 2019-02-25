using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float timeToLive;


    [Header("Damage Controller")]
    public int damage;

    [Header("Particle Effect")]
    public GameObject onHitEffect;

    void Start() {
        damage = PlayerController.damageToGive;
        Debug.Log("Damage is:  "+damage);
        StartCoroutine(destroyAfter(timeToLive));
    }

    IEnumerator destroyAfter(float time) {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.tag == "Enemy") {
            //other.collider.GetComponent<EnemyController>().takeDamage(damage);
            Instantiate(onHitEffect, this.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        Debug.Log("Collided with " + other.collider.tag);

    }
}

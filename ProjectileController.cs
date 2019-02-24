using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float timeToLive;

    // public ParticleSystem collisionFX;
    public AudioClip collisionAudio;
    public GameObject onHitEffect;

    [Header("Damage Controller")]
    public int damage;

    [Header("Particle Effect")]
    public GameObject onHitEffect;

    void Start() {
        damage = PlayerController.damageToGive;
        Debug.Log("Damage is:  "+damage);
    }

    void Update(){

    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.tag == "Enemy") {
            other.collider.GetComponent<EnemyController>().takeDamage(damage);
        }
         if(other.collider.tag == "Enemy" || other.collider.tag == "Environment" ){
         	// collisionFX.Play();
         	AudioSource.PlayClipAtPoint(collisionAudio, new Vector3(5, 1, 2));
            // Instantiate(onHitEffect, this.transform.position, Quaternion.identity);
            GameObject clone = (GameObject)Instantiate(onHitEffect, this.transform.position, Quaternion.identity);
            Destroy(clone, 1.0f);
            // Destroy(onHitEffect);
        	Destroy(gameObject);
        }
        Debug.Log("Collided with " + other.collider.tag);

    }
}

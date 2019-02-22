using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float timeToLive;

    // public ParticleSystem collisionFX;
    public AudioClip collisionAudio;

    [Header("Damage Controller")]
    public int damage;

    void Start() {
        damage = PlayerController.damageToGive;
        Debug.Log("Damage is:  "+damage);
    }

    void Update(){

    }

    void OnCollisionEnter(Collision other) {
        if (other.collider.tag == "Enemy") {
            other.collider.GetComponent<EnemyController>().takeDamage(damage);
        }
         if(other.collider.tag == "Enemy" || other.collider.tag == "Environment" ){
         	// collisionFX.Play();
         	AudioSource.PlayClipAtPoint(collisionAudio, new Vector3(5, 1, 2));
        	Destroy(gameObject);
        }
    }
}

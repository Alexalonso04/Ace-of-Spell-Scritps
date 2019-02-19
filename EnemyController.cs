using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void attack() {

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
}

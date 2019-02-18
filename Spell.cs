using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spell : MonoBehaviour{
    // private const float SPELL_COOL_DOWN = 3.0f;

    public string spellName;
    public Texture spellImage;
    public int spellDamage; 
    public float spellCoolDown;
    public string spellDesc;
    public AudioClip spellAudioFire;
    public AudioClip spellAudioCollision;
    public GameObject spellPrefab;

    private bool canShoot = true;
    private GameObject spellProjectile;
    public Transform projectileSpawn;
    GameObject player ;

    public float coolDown;


    // public int projectileSpeed;
    
 
    public string getName(){
        return spellName;
    }

    public Texture getImage(){
        return spellImage;
    }

    public int getDamage(){
        return spellDamage;
    }

    public float getSpellCoolDown(){
        return spellCoolDown;
    }

    public string getSpellDesc(){
        return spellDesc;
    }

    public AudioClip getAudioFire(){
        return spellAudioFire;
    }

    public AudioClip getAudioCollision(){
        return spellAudioCollision;
    }

    public GameObject getGameObject(){
        //return GameObject.Instantiate((GameObject)Resources.Load(sfire))
        return spellPrefab;
    }
    
    public Transform getProjectileSpawn(){
        //return GameObject.Instantiate((GameObject)Resources.Load(sfire))
        return projectileSpawn;
    }

    public void onClick(GameObject spell){
        if ( coolDown <= 0 ) {
            coolDown = spellCoolDown; 
            StartCoroutine(Fire(spell));

       }
    }

    public IEnumerator Fire(GameObject spell){
        // Debug.Log(canShoot);
        // if(canShoot){

            spellProjectile = spellPrefab;
            
            // player= GameObject.Find("Player");
            // projectileSpawn = player.transform.GetChild(0).GetChild(2);
            
            GameObject firedSpell = Instantiate(spellProjectile);

            firedSpell.transform.position = projectileSpawn.position;
            Vector3 originalRotation = firedSpell.transform.rotation.eulerAngles;

            firedSpell.transform.rotation = Quaternion.Euler(originalRotation.x, transform.eulerAngles.y, originalRotation.z);
            
            firedSpell.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * 36, ForceMode.VelocityChange);
            Debug.Log("Spell I'm using: " + spellName);
            playSpellAudio(spell);
                       

           // Cool down for Spell
            canShoot = false;
            yield return new WaitForSeconds (spellCoolDown);
            canShoot  = true;
           

        //     coolDown = spellCoolDown;
        //     canShoot = false;
        // }
    }
    
    public void playSpellAudio(GameObject spell){
        AudioSource.PlayClipAtPoint(spellAudioFire, new Vector3(5, 1, 2));
    }

    void Update(){
        coolDown -= Time.deltaTime;
        if(coolDown<=0)
            canShoot = true;
        // Debug.Log(coolDown);
    }
    void Awake(){

        coolDown = 0;
    }

}

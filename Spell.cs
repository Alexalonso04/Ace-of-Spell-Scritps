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

    public bool canShoot = true;
    public GameObject spellProjectile;
    public Transform projectileSpawn;
    public GameObject player ;

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
    
    // public Transform getProjectileSpawn(){
    //     //return GameObject.Instantiate((GameObject)Resources.Load(sfire))
    //     return projectileSpawn;
    // }

    // public void onClick(){
    //     if (canShoot) {
    //         coolDown = spellCoolDown; 
    //         StartCoroutine(Fire());
    //    }
    // }

    public void Fire(){                    

        if (spellName == "FireBlast"){
            GetComponent<FireBlast>().Fire();
        }        
        if (spellName == "Ice"){
            GetComponent<Ice>().Fire();
        }        
    }
    
    public void playSpellAudio(GameObject spell){
        AudioSource.PlayClipAtPoint(spellAudioFire, new Vector3(5, 1, 2));
    }
}

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

    public GameObject spellProjectile;
    public Transform projectileSpawn;
    public GameObject player ;

    public static float coolDownPercentage = 100f;
    public static float nextFireTime;
    public static bool canUseSpell = true;

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
        return spellPrefab;
    }
    
    public float setCoolDownPercentage(float percent){
    	// Debug.Log("***********************I SET IT TO ZERO***************************" + percent);
    	coolDownPercentage = percent;
    	return coolDownPercentage;
    }

    public bool canUseASpell(){
    	Debug.Log(canUseSpell);
    	return canUseSpell;
    }

    void Update(){
    	// Debug.Log("%" + coolDownPercentage);
    	if(coolDownPercentage != 0)
       		coolDownPercentage = (((nextFireTime-Time.time)/spellCoolDown)*100);
       	
       	if(coolDownPercentage<=0){    
    		canUseSpell = true;       
    	}
        else{
            canUseSpell = false;
        }

        Debug.Log("Cool down percentage: " + coolDownPercentage);

    }

    public void Fire(){   

<<<<<<< HEAD
        if(coolDownPercentage<=0){    
    	    canUseSpell = true;       
            nextFireTime = Time.time + spellCoolDown;
            coolDownPercentage = (((nextFireTime-Time.time)/spellCoolDown)*100);
=======
    if(coolDownPercentage<=0){    
    	canUseSpell = true;       
        nextFireTime = Time.time + spellCoolDown;
        coolDownPercentage = (((nextFireTime-Time.time)/spellCoolDown)*100);
>>>>>>> 0aa451b095362ffd79ecea443c90e9827348fcad
       
            if (spellName == "FireBlast"){
                GetComponent<FireBlast>().Fire();
            }        
            if (spellName == "Ice"){
                GetComponent<Ice>().Fire();
<<<<<<< HEAD
            }
            if (spellName == "Electrocute")
            {
                GetComponent<Electrocute>().Fire();
            }
=======
            }        
>>>>>>> 0aa451b095362ffd79ecea443c90e9827348fcad
        }
        else{
        	canUseSpell = false;
        }
    }
    
    public void playSpellAudio(GameObject spell){
        AudioSource.PlayClipAtPoint(spellAudioFire, new Vector3(5, 1, 2));
    }
}

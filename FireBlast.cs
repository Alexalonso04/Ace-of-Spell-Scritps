using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FireBlast : Spell{

    // void Update(){
    //       coolDownPercentage = (((nextFireTime-Time.time)/spellCoolDown)*100);
    //       Debug.Log(coolDownPercentage);
    // }

    public FireBlast(){
        spellName = "FireBlast";
        spellDamage = 2;
        spellCoolDown = 2f;
        spellDesc = "The target is attacked with an intense blast of all-consuming fire.";
    }

    public void Fire(){
        // if(coolDownPercentage<=0){           
        nextFireTime = Time.time + spellCoolDown;

            spellProjectile = spellPrefab;
            
            player= GameObject.Find("Player");
            projectileSpawn = player.transform.GetChild(0).GetChild(2);
            
            GameObject firedSpell = Instantiate(spellProjectile);

            firedSpell.transform.position = projectileSpawn.position;
            Vector3 originalRotation = firedSpell.transform.rotation.eulerAngles;

            firedSpell.transform.rotation = Quaternion.Euler(originalRotation.x, projectileSpawn.rotation.eulerAngles.y, originalRotation.z);
            
            firedSpell.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * 36, ForceMode.VelocityChange);
            // Debug.Log("Spell I'm using: " + spellName);
            playSpellAudio();
        // }

    }
    
    public void playSpellAudio(){
        AudioSource.PlayClipAtPoint(spellAudioFire, new Vector3(5, 1, 2));
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FireBlast : Spell{
	public FireBlast(){
		spellName = "FireBlast";
		spellDamage = 2;
		spellCoolDown = 5f;
		spellDesc = "The target is attacked with an intense blast of all-consuming fire.";
	}

	public void Fire(){

            spellProjectile = spellPrefab;
            
            player= GameObject.Find("Player");
            projectileSpawn = player.transform.GetChild(0).GetChild(2);
            
            GameObject firedSpell = Instantiate(spellProjectile);

            firedSpell.transform.position = projectileSpawn.position;
            Vector3 originalRotation = firedSpell.transform.rotation.eulerAngles;

            firedSpell.transform.rotation = Quaternion.Euler(originalRotation.x, transform.eulerAngles.y, originalRotation.z);
            
            firedSpell.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * 36, ForceMode.VelocityChange);
            Debug.Log("Spell I'm using: " + spellName);
            playSpellAudio();
                       

           // Cool down for Spell
            // canShoot = false;
            // yield return new WaitForSeconds (spellCoolDown);
            // canShoot  = true;

	}

	  public void playSpellAudio(){
        AudioSource.PlayClipAtPoint(spellAudioFire, new Vector3(5, 1, 2));
    }
}

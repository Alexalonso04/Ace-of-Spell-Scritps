using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ice : Spell{
	public Ice(){
		spellName = "Ice";
		spellDamage = 0;
		spellCoolDown = 5f;
		spellDesc = "Enemies around the player face an intense frostbite.";
	}

	public void Fire(){

            spellProjectile = spellPrefab;
            
            player= GameObject.Find("Player");
            projectileSpawn = player.transform.GetChild(0).GetChild(2);
            
            GameObject firedSpell = Instantiate(spellProjectile);

            firedSpell.transform.position = projectileSpawn.position;
            Vector3 originalRotation = firedSpell.transform.rotation.eulerAngles;

            firedSpell.transform.rotation = Quaternion.Euler(originalRotation.x, projectileSpawn.rotation.eulerAngles.y, originalRotation.z);
            
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrocute : Spell
{
   
    private GameObject target;
    public int radius;

    public Electrocute()
    {
        spellName = "Electrocute";
        spellDamage = 5;
        spellCoolDown = 1f;
        spellDesc = "The target is knocked back by an bolt of lightning";
    }
    
    public void Fire()
    {
        Debug.Log("Electrocute!!!!");

        spellProjectile = spellPrefab;
        //Start location of spell
        player = GameObject.Find("Player");
        projectileSpawn = player.transform.GetChild(0).GetChild(2);
        //Instantiates Lightning_Prefab
        LookforTarget();

            Debug.Log("Instantiating lightning");
            nextFireTime = Time.time + spellCoolDown;
            Instantiate(spellProjectile, projectileSpawn.position, Quaternion.identity);

    }

    public void playSpellAudio()
    {
        AudioSource.PlayClipAtPoint(spellAudioFire, new Vector3(5, 1, 2));
    }

    private GameObject LookforTarget() //Looks for Enemy within a radius and sends GameObject back
    {
        Debug.Log("Looking for target");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject mainTarget in enemies)
        {
            float distance = Vector3.Distance(mainTarget.transform.position, transform.position);
            if (distance < radius)
            {
                target = mainTarget;
                //target.GetComponent<Rigidbody>().AddForce(target.transform.position * 10);
                Debug.Log("Found Enemy");
                return mainTarget;
            }
        }

        return null;
    }

    public GameObject getTarget()
    {
        return target;
    }
}

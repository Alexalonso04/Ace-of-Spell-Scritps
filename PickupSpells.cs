using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupSpells : MonoBehaviour
{
    
    // public Prefab spellPrefab;
    public GameObject spellPrefab;

    private void OnTriggerEnter(Collider other)
    {
            if(other.tag=="Player"){
                // DELETE After implementing Equip();
                // Texture image = gameObject.GetComponent<Spell>().getImage();
                // string name = gameObject.GetComponent<Spell>().getName();
                // GameObject.Find("HUD").GetComponent<HUDController>().changeSpell(0, image, name);
                //TODO: Call Equip(spellPrefab)
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().Equip(spellPrefab);
                spellPrefab.gameObject.SetActive(false);
                // Destroy(gameObject);
            }
    }
}


// 
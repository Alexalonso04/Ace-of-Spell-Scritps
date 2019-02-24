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
                //TODO: Call Equip(spellPrefab)
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().Equip(spellPrefab);
                spellPrefab.gameObject.SetActive(false);
                // spellPrefab.GetComponent<MeshRenderer>().enabled = false;
                // spellPrefab.GetComponent<BoxCollider>().enabled = false;
                // Destroy(gameObject);
            }
    }
}


// 
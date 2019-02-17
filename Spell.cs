using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spell : MonoBehaviour{
    public string spellName;
    public Texture spellImage;
    public int spellDamage; 
    public int spellCoolDown;
    public string spellDesc;
    public AudioClip spellAudioFire;
    public AudioClip spellAudioCollision;
    public GameObject spellPrefab;

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

    public int getSpellCoolDown(){
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


}

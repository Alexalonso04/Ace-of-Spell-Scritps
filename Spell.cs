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

}

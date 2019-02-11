using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FireBlast : Spell{
	public FireBlast(){
		spellName = "FireBlast";
		spellDamage = 2;
		spellCoolDown = 5;
		spellDesc = "The target is attacked with an intense blast of all-consuming fire.";
	}
}

using UnityEngine;
using System.Collections;

public class printStat : MonoBehaviour {
	
	public Gateway gtw;
	OTSprite slot1,slot2,slot3;

	GameObject stats;
	
	public void stat(Gateway gtw){
		stats=GameObject.Find("spotStats");
		stats.GetComponent<OTTextSprite>().text="Name: " + gtw.getName() + " \nType: " + gtw.getType() + "\nAttack: " + gtw.getAtk() + " Defence: " + gtw.getDef();
		slot1=GameObject.Find("slot1").GetComponent<OTSprite>();
		slot2=GameObject.Find("slot2").GetComponent<OTSprite>();
		slot3=GameObject.Find("slot3").GetComponent<OTSprite>();
		
		//slot1.image=gtw.getslot1();
		//slot3.image=gtw.getslot2();
		//slot2.image=gtw.getslot3();


	}
	

	

}

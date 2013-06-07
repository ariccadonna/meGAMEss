using UnityEngine;
using System.Collections;

public class generateShopPanel : MonoBehaviour {
	
	GameObject pop;
	OTSprite sprite;
	referencePanel script;
	
	void Start () {
		
		pop = GameObject.Find("referencePanel");
		script = pop.GetComponent<referencePanel>();
		
		sprite = GetComponent<OTSprite>();
		sprite.onInput=click;
		
		
	
	}
	
	void click (OTObject sprite) {
	
	
		 if(Input.GetMouseButtonDown(0))
		{
			if (script.shopPanel.activeSelf==false)
			{
				script.activateShopPanel();
			}
			else
			{
				script.deactivateShopPanel();
			}	
			
			
		} 
		
	}
		
	
		
		
	

}

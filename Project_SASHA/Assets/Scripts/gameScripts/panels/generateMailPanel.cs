using UnityEngine;
using System.Collections;

public class generateMailPanel : MonoBehaviour {


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
			if (script.mailPanel.activeSelf==false)
			{
				script.activateMailPanel();
			}
			else
			{
				script.deactivateMailPanel();
			}	
		}
		
	}
}

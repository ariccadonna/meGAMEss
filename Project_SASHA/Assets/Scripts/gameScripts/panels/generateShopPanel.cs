using UnityEngine;
using System.Collections;

public class generateShopPanel : MonoBehaviour {
	
	GameObject pop;
	OTSprite sprite;
	referencePanel script;
	private bool sc = false;
	Vector3 scale;
	
	void Start () {
		
		pop = GameObject.Find("referencePanel");
		script = pop.GetComponent<referencePanel>();
		
		sprite = GetComponent<OTSprite>();
		sprite.onInput=click;
		sprite.onMouseExitOT = exit;
		scale = gameObject.transform.localScale;
		
		
	
	}
	
	void click (OTObject sprite) 
	{

		
		if(Input.GetMouseButtonUp(0))
		{
			gameObject.transform.localScale=scale;
			sc = false;
		}
	
		if(Input.GetMouseButtonDown(0))
		{
			gameObject.transform.localScale=new Vector3(gameObject.transform.localScale.x-0.2f,gameObject.transform.localScale.y-0.2f,gameObject.transform.localScale.z);
			sc = true;
			
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
	
	void exit(OTObject sprite) 
	{
		if(sc)
		{
			gameObject.transform.localScale=scale;
			sc = false;
		}
	}

}

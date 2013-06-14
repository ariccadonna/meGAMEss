using UnityEngine;
using System.Collections;

public class clickOnItem : MonoBehaviour {
	
	public bool active;
	public string sw;
	OTSprite sprite;
	NetworkManager nwm;
	
	// Use this for initialization
	void Awake () {
		
		sprite=GetComponent<OTSprite>();
		sprite.onInput=click;
		
	
	}
	
	
	
	void click(OTObject sprite)
	{
		nwm=GameObject.Find("referencePanel").GetComponent<NetworkManager>();
	
		if(Input.GetMouseButtonUp(0))
		{
			if(active)
			{
				switch(gameObject.transform.name)
				{
					case "item1":
						nwm.buyItem(sw+"1");
					break;
					case "item2":
						nwm.buyItem(sw+"2");
					break;
					case "item3":
						nwm.buyItem(sw+"3");
					break;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

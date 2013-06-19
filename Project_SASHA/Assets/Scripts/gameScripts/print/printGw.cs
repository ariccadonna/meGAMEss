using UnityEngine;
using System.Collections;

public class printGw : MonoBehaviour {
	
	hackEvent hack;
	OTTextSprite sprite;
	
	
	// Use this for initialization
	void Start () {
		
		hack = GameObject.Find("hackButton").GetComponent<hackEvent>();
		sprite =  GetComponent<OTTextSprite>();
		
		
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (gameObject.transform.name=="gwStartInfo") 
		{
			if (hack.gatewayStart)
			{
			sprite.text=hack.gatewayStart.transform.name;
			}
			else
			{
			sprite.text="NONE";
			}
			
		}
		
		if (gameObject.transform.name=="gwTargetInfo")
		{
			if (hack.gatewayTarget)
			{
			sprite.text=hack.gatewayTarget.transform.name;
			}
			else
			{
			sprite.text="NONE";
			}
		}
		
	
	}
}

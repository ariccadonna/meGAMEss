using UnityEngine;
using System.Collections;

public class hackEvent : MonoBehaviour {
	
	public GameObject gatewayStart;
	public GameObject gatewayTarget;
	OTScale9Sprite sprite;
	private NetworkManager nwm = null;
	private string target,start;
	
	// Use this for initialization
	void Start () {
		
		nwm=GameObject.Find("referencePanel").GetComponent<NetworkManager>();
		gatewayStart=null;
		gatewayTarget=null;
		sprite=GetComponent<OTScale9Sprite>();
		sprite.tintColor=Color.red;
		sprite.onInput=click;
		
		
	}
	
	void Update()
	{
		if (gatewayStart)
		{
			sprite.tintColor=Color.blue;
			
		}
		else
		{
			sprite.tintColor=Color.red;
		}
		
	}
	
	void click(OTObject sprite)
	{
		if (Input.GetMouseButtonDown(0))
		{
			if(!gatewayStart)
			{
				print ("first select the start gateway from those you own");
			}
			else
			{
				if(gatewayTarget)
				{
					target=gatewayTarget.GetComponent<Gateway>().getState();
					start=gatewayStart.GetComponent<Gateway>().getState();
					nwm.SendHackRequest(start,target);
				}
			}
		}
	}
}

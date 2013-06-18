using UnityEngine;
using System.Collections;

public class closeWindow : MonoBehaviour {
	
	public GameObject gatewayStart;
	OTSprite sprite;
	// Use this for initialization
	void Start () {
		
		sprite=GetComponent<OTSprite>();
		sprite.onInput=click;
	
	}
	
	void click(OTObject sprite)
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject.Find ("referencePanel").GetComponent<NetworkManager>().closeDisplayWindow();
		}
	}
}

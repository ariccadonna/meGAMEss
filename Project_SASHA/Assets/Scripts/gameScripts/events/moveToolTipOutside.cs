using UnityEngine;
using System.Collections;

public class moveToolTipOutside : MonoBehaviour {
	
	
	private GameObject toolTip;
	private GameObject toolTipText;
	Vector3 outside;
	hackEvent hack;
	
	
	// Use this for initialization
	void Start () {
	
		OTSprite sprite=GetComponent<OTSprite>();
		hack=GameObject.Find("hackButton").GetComponent<hackEvent>();
		//gameObject.transform.position=new Vector3(0,0,1000);
		outside = new Vector2(-130,800);
		toolTip=GameObject.Find("toolTip");
		toolTipText=GameObject.Find("toolTipText");
		
		
	}
	
	
	
	void Update(){
	
		if (Input.GetMouseButtonDown(0))
		{
			toolTip.transform.position=outside;
			
		}
	
	}
	
	

}

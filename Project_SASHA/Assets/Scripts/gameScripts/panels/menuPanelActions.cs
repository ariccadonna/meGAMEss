using UnityEngine;
using System.Collections;

public class menuPanelActions : MonoBehaviour 
{
	
	private GameObject pop;
	private OTTextSprite sprite;
	private referencePanel script;
	private bool sc = false;
	private Vector3 scale;
	private NetworkManager nwm;
	private referencePanel rp;
	
	void Start () {
		pop = GameObject.Find("referencePanel");
		script = pop.GetComponent<referencePanel>();
		sprite = gameObject.GetComponent<OTTextSprite>();
		sprite.onInput = click;
		sprite.onMouseExitOT = exit;
		scale = gameObject.transform.localScale;
		nwm = GameObject.Find("referencePanel").GetComponent<NetworkManager>();
		rp = GameObject.Find("referencePanel").GetComponent<referencePanel>();
	}
	
	void click (OTObject sprite) 
	{
		if(Input.GetMouseButtonUp(0))
		{
			string text = gameObject.GetComponent<OTTextSprite>().text;
			switch(text)
			{
				case "Back":
					rp.deactivateMenu();
					gameObject.transform.localScale=scale;
					sc = false;
				break;
				case "Tutorial":
					Debug.Log ("open tutorial here");
					gameObject.transform.localScale=scale;
					sc = false;
				break;
				case "Leave":
					nwm.leaveGame();
					gameObject.transform.localScale=scale;
					sc = false;
				break;
				case "Quit":
					nwm.CloseApplication();
				break;
			}
		}
	
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 localScale = gameObject.transform.localScale;
			transform.localScale=new Vector3(0.004f,0.002f,localScale.z);
			sc = true;
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

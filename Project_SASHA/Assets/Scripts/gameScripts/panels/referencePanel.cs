using UnityEngine;
using System.Collections;

public class referencePanel : MonoBehaviour {
	
	public GameObject bottomPanel;
	public GameObject shopPanel;
	public GameObject inventoryPanel;
	public GameObject lastSelectedGateway;
	GameObject obj;
	float a;
	float b;
	
	void Start()
	{
		bottomPanel.SetActive(false);
		lastSelectedGateway=null;	
	}

	public void activateBottomPanel(GameObject gtw){
		
		if (lastSelectedGateway!=gtw)
		{
			bottomPanel.SetActive(true); 
			bottomPanel.GetComponent<printStat>().stat(gtw.GetComponent<Gateway>());
			lastSelectedGateway=gtw;
			GameObject.Find("slot1").GetComponent<drop>().setGateway(gtw);
			GameObject.Find("slot2").GetComponent<drop>().setGateway(gtw);
			GameObject.Find("slot3").GetComponent<drop>().setGateway(gtw);
		}
		else
		{
			bottomPanel.SetActive(false);
			lastSelectedGateway=null;
		}
	
	}
	//public void deactivateBottomPanel(){bottomPanel.SetActive(false);}
	
	public void activateShopPanel(){shopPanel.SetActive(true);}
	public void deactivateShopPanel(){shopPanel.SetActive(false);}

	public void activateMailPanel(){
		inventoryPanel.GetComponent<inventory>().instantiateSW();
		inventoryPanel.SetActive(true);
	}
	public void deactivateMailPanel(){
		inventoryPanel.SetActive(false);
		inventoryPanel.GetComponent<inventory>().deleteSW();
	}
	
	

}

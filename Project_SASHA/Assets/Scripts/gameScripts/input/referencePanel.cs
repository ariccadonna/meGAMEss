using UnityEngine;
using System.Collections;

public class referencePanel : MonoBehaviour {
	
	public GameObject bottomPanel;
	public GameObject shopPanel;
	public GameObject mailPanel;
	public Gateway lastSelectedGateway;
	GameObject obj;
	float a;
	float b;
	
	void Start()
	{
	bottomPanel.SetActive(false);
	lastSelectedGateway=null;	
	}

	public void activateBottomPanel(Gateway gtw){
		
		if (lastSelectedGateway!=gtw)
		{
			bottomPanel.SetActive(true); 
			bottomPanel.GetComponent<printStat>().stat(gtw);
			lastSelectedGateway=gtw;
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

	public void activateMailPanel(){mailPanel.SetActive(true);}
	public void deactivateMailPanel(){mailPanel.SetActive(false);}
	
	

}

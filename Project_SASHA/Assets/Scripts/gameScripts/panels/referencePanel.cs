using UnityEngine;
using System.Collections;

public class referencePanel : MonoBehaviour {
	
	public GameObject bottomPanel;
	public GameObject shopPanel;
	public GameObject mailPanel;
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

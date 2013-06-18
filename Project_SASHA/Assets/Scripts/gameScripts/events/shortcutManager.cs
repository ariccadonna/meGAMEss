using UnityEngine;
using System.Collections;

public class shortcutManager : MonoBehaviour 
{
	
	private referencePanel rp;
	private hackEvent hack;
	private neutralizeEvent neutralize;
	private NetworkManager nwm;

	void Start () 
	{
		rp = GameObject.Find("referencePanel").GetComponent<referencePanel>();
		hack = GameObject.Find ("hackButton").GetComponent<hackEvent>();
		neutralize = GameObject.Find ("neutralizeButton").GetComponent<neutralizeEvent>();
		nwm = GameObject.Find("referencePanel").GetComponent<NetworkManager>();
	}
	
	void Update () 
	{
		//menù shortcut
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			if(nwm.displayWindowIsOpen)
				nwm.closeDisplayWindow();
			else
				if (rp.shopPanel.activeSelf==true)
					rp.deactivateShopPanel();
				else 
					if(rp.menuPanel.activeSelf==true)
						rp.deactivateMenu();
					else
						rp.activateMenu();
		}
		
		//hack shortcut
		if(Input.GetKeyUp(KeyCode.H))
		{
			if(!hack.gatewayStart)
			{
				nwm.DisplayWindow("MISSINGORIGIN");
			}
			
			else 
				if(!hack.gatewayTarget)
				{
					nwm.DisplayWindow("MISSINGDESTINATION");
				}
			
			if(hack.gatewayStart && hack.gatewayTarget)
			{
				string target=hack.gatewayTarget.GetComponent<Gateway>().getState();
				string start=hack.gatewayStart.GetComponent<Gateway>().getState();
				nwm.SendHackRequest(start,target);
			}
			
		}
		
		//neutralize shortcut
		if(Input.GetKeyUp(KeyCode.N))
		{
			if(!neutralize.gatewayStart)
			{
				nwm.DisplayWindow("MISSINGORIGIN");
			}
			
			else
				if(!neutralize.gatewayTarget)
				{
					nwm.DisplayWindow("MISSINGDESTINATION");
				}
			
			if(neutralize.gatewayStart && neutralize.gatewayTarget)
			{
				string target=neutralize.gatewayTarget.GetComponent<Gateway>().getState();
				string start=neutralize.gatewayStart.GetComponent<Gateway>().getState();
				nwm.SendNeutralizeRequest(start,target);
			}
		}
		
		//shop shortcut
		if(Input.GetKeyUp(KeyCode.S))
		{
			if (rp.shopPanel.activeSelf==true)
				rp.deactivateShopPanel();
			else
				rp.activateShopPanel();
		}
		
		//inventory shortcut
		if(Input.GetKeyUp(KeyCode.I))
		{
			if (rp.inventoryPanel.activeSelf==true)
				rp.deactivateInventoryPanel();
			else
				rp.activateInventoryPanel();
		}
	}
}

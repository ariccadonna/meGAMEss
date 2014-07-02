using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutManager : MonoBehaviour 
{

	private Manager manager;
    private NetworkManager nwm;
    private NotificationManager nm;
	private CollisionManager cm;

	void Start () 
	{
		manager = GameObject.Find("Manager").GetComponent<Manager>();
        nwm = manager.GetComponent<NetworkManager>();
        nm = manager.GetComponent<NotificationManager>();
		cm = manager.GetComponent<CollisionManager>();
	}
	
	void Update () 
	{
		//menù shortcut
		if(Input.GetKeyUp(KeyCode.Escape))
		{
            cm.escapeMenu.SetActive(!cm.escapeMenu.activeInHierarchy);
            cm.changeGatewayCollider();
		}
		
		//hack shortcut
		if(Input.GetKeyUp(KeyCode.H))
		{
			if(nwm.getStart().Equals(""))
                nm.DisplayWindow("MISSINGORIGIN");
			else 
                if(nwm.getEnd().Equals(""))
                    nm.DisplayWindow("MISSINGDESTINATION");
			    else
                    nwm.SendHackRequest(nwm.getStart(),nwm.getEnd(), false);
		}
		
		//neutralize shortcut
		if(Input.GetKeyUp(KeyCode.N))
		{
            if(nwm.getStart().Equals(""))
                nm.DisplayWindow("MISSINGORIGIN");
			else
                if(nwm.getEnd().Equals(""))
                    nm.DisplayWindow("MISSINGDESTINATION");
			    else
				    nwm.SendHackRequest(nwm.getStart(), nwm.getEnd(), true);
		}
		//shop shortcut
		if(Input.GetKeyUp(KeyCode.S))
		{
            if(!cm.escapeMenu.activeInHierarchy)
                gameObject.GetComponent<CollisionManager>().toggleShop();
		}

        if(Input.GetKeyUp(KeyCode.F12))
        {
            Debug.Log("Taking a screen");
            DateTime value = DateTime.Now;
            string timestamp = value.ToString("yyyyMMddHHmmssffff");
            Application.CaptureScreenshot("ps_"+ timestamp +".png");
        }
	}
}

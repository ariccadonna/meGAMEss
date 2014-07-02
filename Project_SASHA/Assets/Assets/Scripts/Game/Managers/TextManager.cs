using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Logging;

public class TextManager : MonoBehaviour {

    public Gateway gtw;
    private string player;


    public void UpdatePlayerInfo(ISFSObject data, Color myColor)
    {
        String colorName = "";
        long time = data.GetLong("time");
        int s,m,h;
        string formattedTime;

        if(myColor == Color.red)
            colorName = "red";
        if(myColor == Color.green)
            colorName = "green";
        if(myColor == Color.cyan)
            colorName = "cyan";
        if(myColor == Color.magenta)
            colorName = "magenta";
        if(myColor == Color.yellow)
            colorName = "yellow";
        
        
        int newtime = (int) time/1000;
        s = (int) newtime%60;
        m = (int) (newtime/60)%60;
        h = (int) (newtime/3600)%24;
        formattedTime = string.Format("{0:00}:{1:00}:{2:00}",h,m,s);
        
        
        GameObject stats = GameObject.Find("StatLabels");
        stats.transform.FindChild("Name").GetComponent<UILabel>().text = data.GetUtfString("name");
        stats.transform.FindChild("Color").GetComponent<UILabel>().text = colorName;
        stats.transform.FindChild("Color").GetComponent<UILabel>().color = myColor;
        stats.transform.FindChild("Money").GetComponent<UILabel>().text = data.GetInt("money").ToString();
        stats.transform.FindChild("Timer").GetComponent<UILabel>().text = formattedTime;
    }

    public void printGwInfo(Gateway gtw)
    {
        ResourcesManager rm = gameObject.GetComponent<ResourcesManager>();
        this.gtw = gtw;

        player = gameObject.GetComponent<NetworkManager>().getCurrentPlayer();
        string owner = gtw.getOwner();

        GameObject gateway = GameObject.Find("GatewayLabels");
        gateway.transform.FindChild("gw_name").GetComponent<UILabel>().text = gtw.getName();
        gateway.transform.FindChild("gw_type").GetComponent<UILabel>().text = gtw.getType();
        gateway.transform.FindChild("gw_atk").GetComponent<UILabel>().text = gtw.getAtk().ToString();
        gateway.transform.FindChild("gw_def").GetComponent<UILabel>().text = gtw.getDef().ToString();

        GameObject slot1 = GameObject.Find("Slot1");
        GameObject slot2 = GameObject.Find("Slot2");
        GameObject slot3 = GameObject.Find("Slot3");

        if (owner.Equals(player))
        {
            if (gtw.getSlot(0) != "" || gtw.getSlot(0) != "null")
                slot1.GetComponent<SpriteRenderer>().sprite = rm.getSlotImage(gtw.getSlot(0));
            else
                slot1.GetComponent<SpriteRenderer>().sprite = rm.getEmptySlot();

            if (gtw.getSlot(1) != "" || gtw.getSlot(0) != "null")
                slot2.GetComponent<SpriteRenderer>().sprite = rm.getSlotImage(gtw.getSlot(1));
            else
                slot2.GetComponent<SpriteRenderer>().sprite = rm.getEmptySlot();

            if (gtw.getSlot(2) != "" || gtw.getSlot(0) != "null")
                slot3.GetComponent<SpriteRenderer>().sprite = rm.getSlotImage(gtw.getSlot(2));
            else
                slot3.GetComponent<SpriteRenderer>().sprite = rm.getEmptySlot();
        } 
        else
        {
            slot1.GetComponent<SpriteRenderer>().sprite = rm.getUnknownSlot();
            slot2.GetComponent<SpriteRenderer>().sprite = rm.getUnknownSlot();
            slot3.GetComponent<SpriteRenderer>().sprite = rm.getUnknownSlot();
        }
    }

}

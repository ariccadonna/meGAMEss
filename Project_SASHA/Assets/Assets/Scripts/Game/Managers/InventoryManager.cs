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

public class InventoryManager : MonoBehaviour {

    private string[] sw = new string[9];
    public GameObject ItemPrefab;

    public void refreshInventory(ISFSArray inv)
    {
        for(int i = 0; i<9; i++)
        {
            sw[i] = (String) inv.GetElementAt(i);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdatePlayerInventory(ISFSArray data)
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("invItem"))
            Destroy(g);

        int i = 0;
        refreshInventory(data);
        while (i < sw.Length)
        {
            GameObject currentSW = Instantiate(ItemPrefab) as GameObject;
            currentSW.transform.name = sw[i];
            switch(i)
            {
                case 0:
                    currentSW.transform.parent = GameObject.Find("Inv1").transform;
                    break;
                case 1:
                    currentSW.transform.parent = GameObject.Find("Inv2").transform;
                    break;
                case 2:
                    currentSW.transform.parent = GameObject.Find("Inv3").transform;
                    break;
                case 3:
                    currentSW.transform.parent = GameObject.Find("Inv4").transform;
                    break;
                case 4:
                    currentSW.transform.parent = GameObject.Find("Inv5").transform;
                    break;
                case 5:
                    currentSW.transform.parent = GameObject.Find("Inv6").transform;
                    break;
                case 6:
                    currentSW.transform.parent = GameObject.Find("Inv7").transform;
                    break;
                case 7:
                    currentSW.transform.parent = GameObject.Find("Inv8").transform;
                    break;
                case 8:
                    currentSW.transform.parent = GameObject.Find("Inv9").transform;
                    break;
            }

            currentSW.transform.localPosition = new Vector3 (0,0,0);
            currentSW.transform.localScale = new Vector3 (0.6F, 0.6F, 1F);
            currentSW.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<ResourcesManager>().getSlotImage(sw[i]);
            i++;
        }
    }
}

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

public class inventory : MonoBehaviour {
	
	public GameObject invPrefab;
	GameObject obj;
	string nm;
	imgRepo imgRepo;
	private string[] sw = new string[9];
	GameObject[] swInstantiated = new GameObject[9];
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	
	public void refreshInventory(ISFSArray inv)
	{
		for(int i = 0; i<9; i++)
		{
			sw[i] = (String) inv.GetElementAt(i);
		}
	}
	
	public void instantiateSW()
	{
		imgRepo=GameObject.Find("imagesRepository").GetComponent<imgRepo>();
		int i = 0;
		while(i < sw.Length)
		{
		
			GameObject currentSW = Instantiate (invPrefab) as GameObject;
			currentSW.transform.parent = gameObject.transform;
			currentSW.transform.name = sw[i];
			currentSW.transform.localScale = new Vector3(0.05F, 0.05F, 0F);
			
			float positionX = -0.3F+0.3F*(i%3);
			float positionY = 0.3F-0.3F*(i/3);
			
			currentSW.transform.localPosition = new Vector3(positionX, positionY, 0F);
			
			currentSW.GetComponent<OTSprite>().image = imgRepo.getTxt(sw[i]);
			if(sw[i]==null || sw[i] == "")
			{
				currentSW.GetComponent<OTSprite>().draggable = false;
				currentSW.GetComponent<OTSprite>()._registerInput = false;
			}
			else
			{
				currentSW.GetComponent<OTSprite>().draggable = true;
				currentSW.GetComponent<OTSprite>()._registerInput = true;
			}
			
			
			swInstantiated[i] = currentSW;
			i++;
		}
	}
	
	public void deleteSW()
	{
		foreach(GameObject sw in swInstantiated)
			Destroy(sw);
	}
	// Update is called once per frame
	void Update () {
	}
}

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

public class buttonShopInput : MonoBehaviour {
	
	public GameObject shop2;
	OTSprite sprite;
	string name,xyz;
	imgRepo imgRepo;
	GameObject item1,item2,item3;
	OTSprite imgItem1,imgItem2,imgItem3;
	OTTextSprite dscItem1,dscItem2,dscItem3,SWdsc_txt, costItem1, costItem2, costItem3;
	Vector2 resize;
	NetworkManager nwm;
	ISFSObject shopList;
	int price;
	string desc;
	
	// Use this for initialization
	void Awake () {
		
		item1=GameObject.Find("item1");
		item2=GameObject.Find("item2");
		item3=GameObject.Find("item3");
		
		
		item1.GetComponent<clickOnItem>().active=item2.GetComponent<clickOnItem>().active=item2.GetComponent<clickOnItem>().active=false;
		
		costItem1=GameObject.Find("costItem1").GetComponent<OTTextSprite>();
		dscItem1=GameObject.Find("dscItem1").GetComponent<OTTextSprite>();
		imgItem1=GameObject.Find("imgItem1").GetComponent<OTSprite>();
		
		costItem2=GameObject.Find("costItem2").GetComponent<OTTextSprite>();
		dscItem2=GameObject.Find("dscItem2").GetComponent<OTTextSprite>();
		imgItem2=GameObject.Find("imgItem2").GetComponent<OTSprite>();
		
		costItem3=GameObject.Find("costItem3").GetComponent<OTTextSprite>();
		dscItem3=GameObject.Find("dscItem3").GetComponent<OTTextSprite>();
		imgItem3=GameObject.Find("imgItem3").GetComponent<OTSprite>();
		
		SWdsc_txt=GameObject.Find("SWdsc_txt").GetComponent<OTTextSprite>();
		
		sprite = GetComponent<OTSprite>();
		sprite.onInput=click;
		name=gameObject.name;
		imgRepo=GameObject.Find("imagesRepository").GetComponent<imgRepo>();
		
		resize=new Vector2(0.1f,0.6f);
		
		
	
	}
	
	
	void click(OTObject sprite)
	{
	
		//SWdsc_txt.text=desc;
		
				
		
		nwm=GameObject.Find("referencePanel").GetComponent<NetworkManager>();
		shopList=nwm.getShop();
		string keyName=name+"_V1";
		foreach(String currentKey in shopList.GetKeys())
		{
			if (currentKey==keyName)
			{
			   ISFSObject currentObject = shopList.GetSFSObject(currentKey);
			   price = currentObject.GetInt("PRICE");
			   desc = currentObject.GetUtfString("DESC");
			}
		}
		
		SWdsc_txt.text=desc;
		
		xyz=name+"_V1";
		imgItem1.image=imgRepo.getTxt(xyz);
		imgItem1.size=resize;
		
		if(name!="Deep_Throat")
		{
			xyz=name+"_V2";
			imgItem2.image=imgRepo.getTxt(xyz);
			imgItem2.size=resize;
			xyz=name+"_V3";
			imgItem3.image=imgRepo.getTxt(xyz);
			imgItem3.size=resize;
			dscItem1.text=name+": V1";
			dscItem2.text=name+": V2";
			dscItem3.text=name+": V3";
			item1.GetComponent<clickOnItem>().active = item2.GetComponent<clickOnItem>().active = item3.GetComponent<clickOnItem>().active=true;
			item1.GetComponent<clickOnItem>().sw = item2.GetComponent<clickOnItem>().sw = item3.GetComponent<clickOnItem>().sw = gameObject.transform.name;
			costItem1.text=(price).ToString()+"$$";
			costItem2.text=(price*2).ToString()+"$$";
			costItem3.text=(price*3).ToString()+"$$";
			
		}
		else
		{
			imgItem2.image=imgItem3.image=imgRepo.getTxt("");
			
			item1.GetComponent<clickOnItem>().active=true;
			dscItem1.text=name;
			dscItem2.text=dscItem3.text="";
			item1.GetComponent<clickOnItem>().sw = gameObject.transform.name;
			item2.GetComponent<clickOnItem>().sw = "";
			item3.GetComponent<clickOnItem>().sw = "";
			costItem2.text="";
			costItem3.text="";
		}
		
	}
	

	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Logging;

public class printObjectives : MonoBehaviour {

	private string objName;
	OTTextSprite txt;
	
	
	void Start()
	{
		txt=GetComponent<OTTextSprite>();
	}
	
	
	public void ObjectivesUpdate(ISFSObject data)
	{
		txt.text="OBJECTIVES:\n";
		
		string[] keys = data.GetKeys();
		foreach(string currentKey in keys)
		{
			ISFSObject currentObject = data.GetSFSObject(currentKey);
			txt.text=txt.text+"\n" + currentObject.GetUtfString("TYPE") 
							 + " " + currentObject.GetInt ("SPOTCONQUERED") 
							 + "/" + currentObject.GetInt ("SPOTREQUIRED");
		}
	}
}

using UnityEngine;
using System.Collections;

public class imgRepo : MonoBehaviour {
	
	
	public Texture FireWool_V1;
	public Texture FireWool_V2;
	public Texture FireWool_V3;
	public Texture Prelude_Detection_System_V1;
	public Texture Prelude_Detection_System_V2;
	public Texture Prelude_Detection_System_V3;
	public Texture LCleaner_V1;
	public Texture LCleaner_V2;
	public Texture LCleaner_V3;
	public Texture Morton_Antivirus_V1;
	public Texture Morton_Antivirus_V2;
	public Texture Morton_Antivirus_V3;
	public Texture Blaster_V1;
	public Texture Blaster_V2;
	public Texture Blaster_V3;
	public Texture Deep_Throat_V1;
	public Texture WireBass_V1;
	public Texture WireBass_V2;
	public Texture WireBass_V3;
	public Texture Brutus_V1;
	public Texture Brutus_V2;
	public Texture Brutus_V3;
	public Texture John_The_Rapper_V1;
	public Texture John_The_Rapper_V2;
	public Texture John_The_Rapper_V3;
	public Texture Thor_Garlic_Proxy_V1;
	public Texture Thor_Garlic_Proxy_V2;
	public Texture Thor_Garlic_Proxy_V3;
	public Texture questionMark;
			
	public Texture current;
	public GameObject currentSw;		
	
	public bool success;
	
	// Use this for initialization
	void Start () {
		success=false;
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Texture getTxt(string varName)
	{
		if (varName=="Brutus_V1")
			return Brutus_V1;
		if (varName=="Brutus_V2")
			return Brutus_V2;
		if (varName=="Brutus_V3")
			return Brutus_V3;
		if (varName=="FireWool_V1")
			return FireWool_V1;
		if (varName=="FireWool_V2")
			return FireWool_V2;
		if (varName=="FireWool_V3")
			return FireWool_V3;
		if (varName=="WireBass_V1")
			return WireBass_V1;
		if (varName=="WireBass_V2")
			return WireBass_V2;
			if (varName=="WireBass_V3")
			return WireBass_V3;
		if (varName=="Deep_Throat_V1")
			return Deep_Throat_V1;
		if (varName=="John_The_Rapper_V3")
			return John_The_Rapper_V3;
		if (varName=="John_The_Rapper_V1")
			return John_The_Rapper_V1;
		if (varName=="John_The_Rapper_V2")
			return John_The_Rapper_V2;
		if (varName=="Thor_Garlic_Proxy_V1")
			return Thor_Garlic_Proxy_V1;
		if (varName=="Thor_Garlic_Proxy_V2")
			return Thor_Garlic_Proxy_V2;
		if (varName=="Thor_Garlic_Proxy_V3")
			return Thor_Garlic_Proxy_V3;
		if (varName=="Morton_Antivirus_V1")
			return Morton_Antivirus_V1;
		if (varName=="Morton_Antivirus_V2")
			return Morton_Antivirus_V2;
		if (varName=="Morton_Antivirus_V3")
			return Morton_Antivirus_V3;
		if (varName=="Blaster_V1")
			return Blaster_V1;
		if (varName=="Blaster_V2")
			return Blaster_V2;
		if (varName=="Blaster_V3")
			return Blaster_V3;
		if (varName=="LCleaner_V1")
			return LCleaner_V1;
		if (varName=="LCleaner_V2")
			return LCleaner_V2;
		if (varName=="LCleaner_V3")
			return LCleaner_V3;
		if (varName=="Prelude_Detection_System_V1")
			return Prelude_Detection_System_V1;
		if (varName=="Prelude_Detection_System_V2")
			return Prelude_Detection_System_V2;
		if (varName=="Prelude_Detection_System_V3")
			return Prelude_Detection_System_V3;
		else 
			return questionMark;
		
	}
}

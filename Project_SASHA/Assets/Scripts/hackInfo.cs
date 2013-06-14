using UnityEngine;
using System.Collections;

public class hackInfo : MonoBehaviour {

	//string phrase;
	float T;
	ArrayList phrases = new ArrayList();
	int i=0;
	int j=0;
	OTTextSprite txt;
	public GameObject cursor;
	public string word;
	public bool isHacking;
	
	IEnumerator Start () 
	{
			phrases.Add("Hack in\nprogress");
			txt=GetComponent<OTTextSprite>();
			txt.text="";
			T=Time.time;
			
			
			
		while (true){
			
			if(isHacking)
			{
				phrases.RemoveAt(0);
				phrases.Add("Hack in\nprogress");
			}
			else
			{
				phrases.RemoveAt(0);
				phrases.Add("");
			}
			
				word=(string)phrases[i];
					
				while (j<word.Length)
				{
					txt.text=txt.text+word[j];
					yield return new WaitForSeconds(0.05f);
					j++;
				}
					
					
				yield return new WaitForSeconds(4);
		
				
				i++;
				if (i==phrases.Count) i=0;
				j=0;
				txt.text="";

		}
	}
}

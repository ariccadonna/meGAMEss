using UnityEngine;
using System.Collections;

public class newsRandom : MonoBehaviour {
	
	ArrayList phrases = new ArrayList();
	int i=0;
	int j=0;
	OTTextSprite txt;
	public GameObject cursor;
	public string word;
	
	IEnumerator Start () {

		phrases.Add("Masteraldo says - stacca tutto stacca tutto!-");
		phrases.Add("Seems Eraldo is losing control");
		phrases.Add("Found stoned penguin in PONG lab...");
		phrases.Add("...it speaks only HEX");
		phrases.Add("Russian bears have taken to huffin fuel");
		
		txt=GetComponent<OTTextSprite>();
		txt.text="";
		
		while (true){
			word=(string)phrases[i];
			
			while (j<word.Length)
			{
				txt.text=txt.text+word[j];
				yield return new WaitForFixedUpdate();
				j++;
			}
				
				
			yield return new WaitForSeconds(4);
			
			i++;
			if (i==phrases.Count)
				i=0;
			j=0;
			
			txt.text="";
		}
	}
}

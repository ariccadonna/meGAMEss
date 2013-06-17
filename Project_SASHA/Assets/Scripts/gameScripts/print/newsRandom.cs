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
		phrases.Add("Popular rich woman disappear in PoE");
		phrases.Add ("assaulted armored truck carrying the finest tea of the world");
		
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

using UnityEngine;
using System.Collections;

public class hackSeconds : MonoBehaviour {

	//string phrase;
	ArrayList phrases = new ArrayList();
	int i=0;
	OTTextSprite txt;
	public string word;
	private int hackSec = -1;
	
	IEnumerator Start () 
	{
		phrases.Add("0");
		phrases.Add("1");
		phrases.Add("2");
		phrases.Add("3");
		txt = GetComponent<OTTextSprite>();
		txt.text = "";
			
			
		while (true){
			if(hackSec>=0)
			{
				phrases.Clear();
				phrases.Add("Hacking\n\n"+timeFormat());
				phrases.Add("Hacking\n.\n"+timeFormat());
				phrases.Add("Hacking\n..\n"+timeFormat());
				phrases.Add("Hacking\n...\n"+timeFormat());
				hackSec-=1;
			}
			else
			{
				phrases.Clear();
				phrases.Add("");
				phrases.Add("");
				phrases.Add("");
				phrases.Add("");
			}
			
			word=(string)phrases[i];
					
			txt.text=word;
			i++;
			
			if (i==phrases.Count) i=0;
			yield return new WaitForSeconds(1f);

		}
	}
	
	public void setSeconds(int seconds)
	{
		this.hackSec = seconds;
	}
	
	private string timeFormat()
	{
		int seconds = this.hackSec%60;
		int minutes = this.hackSec/60;
		int hours = this.hackSec/3600;
		return string.Format("{0:00}:{1:00}", minutes, seconds);
	}
	
}

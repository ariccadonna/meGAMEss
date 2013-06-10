using UnityEngine;
using System.Collections;

public class printPlayerStat : MonoBehaviour {
	
	private OTTextSprite sprite;
	private string formattedTime;
	private int s,m,h;
	
	// Use this for initialization
	void Start () 
	{
		sprite=GetComponent<OTTextSprite>();
	}
	
	// Update is called once per frame
	public void printStat (string name, int money, long time) 
	{
		int newtime = (int) time/1000;
		s = (int) newtime%60;
		m = (int) (newtime/60)%60;
		h = (int) (newtime/3600)%24;
		formattedTime = string.Format("{0:00}:{1:00}:{2:00}",h,m,s);
		sprite.text="STATS:\n\nPlayer: "+ name + "\nMoney: " + money +"\nGame time: "+ formattedTime;
	}
}

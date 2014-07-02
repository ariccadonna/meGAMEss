using UnityEngine;
using System.Collections;

public class HackSecondsManager : MonoBehaviour {

	//string phrase;
	ArrayList phrases = new ArrayList();
	int i=0;
    public GameObject secondText;

	private string word;
	private int hackSec = -1;
    private int  totalSeconds = 0;
	private string action = "";
	
	IEnumerator Start () 
	{
		phrases.Add("0");
		phrases.Add("1");
		phrases.Add("2");
		phrases.Add("3");
        UILabel txt = secondText.GetComponent<UILabel>();
		txt.text = "";
			
			
		while (true){
			if(hackSec>=0)
			{
				phrases.Clear();
				phrases.Add(action+"\n"+progressBar());
				hackSec-=1;
			}
			else
			{
                totalSeconds = 0;
				phrases.Clear();
				phrases.Add("");
			}
			
			word=(string)phrases[i];
					
			txt.text=word;
			i++;
			
            if (i == phrases.Count) i=0;
			yield return new WaitForSeconds(1f);

		}
	}

	public void setAction(string action)
	{
		this.action = action;
	}
	
	public void setSeconds(int seconds)
	{
		this.hackSec = seconds;
        this.totalSeconds = seconds;
	}
	
	private string timeFormat()
	{
		int seconds = this.hackSec%60;
		int minutes = this.hackSec/60;
		return string.Format("{0:00}:{1:00}", minutes, seconds);
	}

    private string progressBar()
    {
        string ret = "[";
        int progress = 100 - ((this.hackSec * 100) / this.totalSeconds);
        for (int i = 0; i < (progress / 10) ; i++)
            ret += "X";

        for (int j = 0; j <= (9 - (progress / 10 )); j++)
            ret += " ";

        ret += "]\n";

        ret += (progress) + "%";
        return ret;
       /* int seconds = this.hackSec%60;
        int minutes = this.hackSec/60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);*/
    }
	
}

using UnityEngine;
using System.Collections;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;

public class Gateway : MonoBehaviour {
	
	private string state; //unique identifier
	private string gwName;
	private string owner;
	private int atk;
	private int def;
	private string type;
	public string[] sw;
	public string region;
    Manager mg;

	void Start()
    {
      
    }
	
	void Update(){}
	
	void OnMouseDown(){/*nwm.SendInfoRequest(this.state);*/}
	
	public void Setup(ISFSObject obj)
	{
        particleSystem.renderer.sortingLayerName = "2 Middle Lower UI";
        this.gwName = obj.GetUtfString("NAME");
		this.state = obj.GetUtfString("STATE");
		this.owner = obj.GetUtfString("OWNER");
		this.atk = obj.GetInt("ATK");
		this.def = obj.GetInt("DEF");
		this.type = obj.GetUtfString("TYPE");
		ISFSArray sws = obj.GetSFSArray("SW");
		this.region = obj.GetUtfString("REGION");
        this.sw = new string[3];
		this.sw[0] = (string) sws.GetElementAt(0);
		this.sw[1] = (string) sws.GetElementAt(1);
		this.sw[2] = (string) sws.GetElementAt(2);
        mg = GameObject.Find("Manager").GetComponent<Manager>();
        gameObject.transform.position = new Vector3((float)obj.GetDouble("X")*mg.getScale().x,(float)obj.GetDouble("Y")*mg.getScale().y,1F);
        gameObject.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
    }

    public void Update(ISFSObject obj)
    {
        this.owner = obj.GetUtfString("OWNER");
        this.atk = obj.GetInt("ATK");
        this.def = obj.GetInt("DEF");
        ISFSArray sws = obj.GetSFSArray("SW");
        this.sw = new string[3];
        this.sw[0] = (string) sws.GetElementAt(0);
        this.sw[1] = (string) sws.GetElementAt(1);
        this.sw[2] = (string) sws.GetElementAt(2);
    }
       
	public string getName()
	{
        return this.gwName;
	}
	
	public string getType()
	{
		return this.type;
	}
	
	public string getState()
	{
		return this.state;
	}
	
	public string getOwner()
	{
		return this.owner;
	}
	
	public int getAtk()
	{
		return this.atk;
	}
	
	public int getDef()
	{
		return this.def;
	}
	
	public string getRegion()
	{
		return this.region;
	}
	
	public string getSlot(int slot)
	{
		return this.sw[slot];
	}

    public bool canInstallSw(string sw)
    {
        string mustHave = sw.Substring(0, sw.Length - 1);
        int swVersion = int.Parse(sw.Substring(sw.Length-1, 1));
        if (swVersion == 3)
            mustHave = mustHave + "2";
        if (swVersion == 2)
            mustHave = mustHave + "1";
        if (swVersion == 1)
            mustHave = "\"null\"";
        if (this.sw [0] == mustHave || this.sw [1] == mustHave || this.sw [2] == mustHave)
            return true;
        else
            return false;
    }

}


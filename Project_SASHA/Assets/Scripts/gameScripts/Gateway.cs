using UnityEngine;
using System.Collections;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;

public class Gateway : MonoBehaviour {
	
	private string state; //unique identifier
	private string name;
	private string owner;
	private int atk;
	private int def;
	private string type;
	private string[] sw;
	//public GameObject networkManager = null;
	private NetworkManager nwm = null;

	// Use this for initialization
	void Start()
	{
	 	GameObject networkManager = GameObject.Find("NetworkManager");
		nwm = networkManager.GetComponent<NetworkManager>();
	}
		// Update is called once per frame
	void Update() {}
	
	public void Setup(ISFSObject obj)
	{
		this.name = obj.GetUtfString("NAME");
		this.state = obj.GetUtfString("STATE");
		this.owner = obj.GetUtfString("OWNER");
		this.atk = obj.GetInt("ATK");
		this.def = obj.GetInt("DEF");
		this.type = obj.GetUtfString("TYPE");
		ISFSArray sws = obj.GetSFSArray("SW");
		this.sw = new string[3];
		this.sw[0] = (string) sws.GetElementAt(0);
		this.sw[1] = (string) sws.GetElementAt(1);
		this.sw[2] = (string) sws.GetElementAt(2);
	}
	
	void OnMouseDown() 
	{
		nwm.SendInfoRequest(this.state);
	}
	
	public string getName()
	{
		return this.name;
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

}


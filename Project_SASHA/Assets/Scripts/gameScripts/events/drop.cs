using UnityEngine;
using System.Collections;

public class drop : MonoBehaviour {
	
	
	private NetworkManager networkManager = null;
	private string player;
	public GameObject gtw;
	public GameObject softwareAssigned;
	GameObject img;
	OTSprite imgSprite;

	imgRepo imgRepo;
	OTSprite sprite;
	
	// Use this for initialization
	void Awake() 
	{
		networkManager = GameObject.Find("referencePanel").GetComponent<NetworkManager>();
		
		sprite=GetComponent<OTSprite>();
		sprite.onReceiveDrop=onDrop;
		imgRepo=GameObject.Find("imagesRepository").GetComponent<imgRepo>();
		
		
		if (gameObject.transform.name=="slot1")	
		{
			img=GameObject.Find("imgSlot1");
		}
		if (gameObject.transform.name=="slot2")
		{
			img=GameObject.Find("imgSlot2");
		}
		if (gameObject.transform.name=="slot3")
		{
			img=GameObject.Find("imgSlot3");
		}
		imgSprite=img.GetComponent<OTSprite>();
		
	}
	
	
	void onDrop(OTObject sprite)
	{
		player = networkManager.getCurrentPlayer();
		string owner = gtw.GetComponent<Gateway>().getOwner();
		string gatewayName = gtw.GetComponent<Gateway>().getState();
		if (player==owner)
		{
			StartCoroutine(installSoftware (gatewayName));
		}
		else
		{
			Debug.Log (owner);
			Debug.Log (player);
			Debug.Log ("that gateway is not yours");
		}
		//handler sw sul player
	}
	
	
	// Update is called once per frame
	void Update(){}
	
	public void setGateway(GameObject gtw)
	{
		this.gtw = gtw;
	}
	
	private IEnumerator installSoftware(string gatewayName)
	{
		
		networkManager.installSoftware(imgRepo.current.name, gatewayName);
		yield return new WaitForSeconds(0.3f);
		networkManager.resetInstalledSuccess();
	}
}


using UnityEngine;
using System.Collections;

public class otMouseInput : MonoBehaviour {
	
	private GameObject thisGameObject;
	private GameObject pop;
	private OTSprite sprite;
	private referencePanel referencePanel;
	private hackEvent hackEvent;
	private neutralizeEvent neutralizeEvent;
	private GameObject lastSelectedGateway;
	private string owner;
	private string player;
	private NetworkManager networkManager = null;
	private Color prevColor;
	
	void Start()
	{
			sprite = GetComponent<OTSprite>();
			thisGameObject=gameObject;
			hackEvent = GameObject.Find("hackButton").GetComponent<hackEvent>();
			neutralizeEvent = GameObject.Find("neutralizeButton").GetComponent<neutralizeEvent>();
			networkManager = GameObject.Find("referencePanel").GetComponent<NetworkManager>();

			//sprite.tintColor=Color.gray;
			prevColor = sprite._tintColor;
			sprite.onMouseEnterOT=enter;
			sprite.onMouseExitOT=exit;
	    	
			pop = GameObject.Find("referencePanel");
			referencePanel = pop.GetComponent<referencePanel>();
		
			sprite.onInput = click;
			owner = GetComponent<Gateway>().getOwner();
			player = networkManager.getCurrentPlayer();
			
	}
	// Input handler

	
	public void enter(OTObject s)
	{
		GetComponent<OTSprite>().size=new Vector2(64f,64f);
		GetComponent<OTSprite>().tintColor=Color.blue;

	}
	
	public void exit(OTObject s)
	{
		GetComponent<OTSprite>().size=new Vector2(40f,40f);
		GetComponent<OTSprite>().tintColor=prevColor;

	}
	
	
	void click (OTObject sprite) {
		
	
		if(Input.GetMouseButtonDown(0))
		{
		
			//controlla che non sia lo stesso selezionato, se seleziono un altro gateway mostro le stat. se seleziono lo stesso disattivo il panel
			if (referencePanel.lastSelectedGateway!=gameObject)
			{
				if (owner==player){
					hackEvent.gatewayStart=gameObject;
					neutralizeEvent.gatewayStart=gameObject;					
				} 
				else
				{
					hackEvent.gatewayTarget=gameObject;
					neutralizeEvent.gatewayTarget=gameObject;
				}
				
				//starting particle for selected gateway
				gameObject.particleSystem.Play();
				
			}
			else
			{
				if (owner==player){
					hackEvent.gatewayStart=null;
					neutralizeEvent.gatewayStart=null;					
				} 
				else
				{
					hackEvent.gatewayTarget=null;
					neutralizeEvent.gatewayTarget=null;
				}
			}
			
			//disabling particle for not selected gateways
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Gateway"))
				if(g != hackEvent.gatewayStart && g != hackEvent.gatewayTarget)
					g.particleSystem.Stop();
			
			referencePanel.activateBottomPanel(gameObject);
		}
	}
	
	void Update()
	{
		owner = GetComponent<Gateway>().getOwner();	
	}
	
	public void setPrevColor(Color newColor)
	{
		this.prevColor = newColor;
	}
}

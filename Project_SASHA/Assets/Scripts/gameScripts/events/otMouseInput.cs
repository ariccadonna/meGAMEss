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
	
	private GameObject toolTip;
	private GameObject toolTipText1,toolTipText2;
	private Gateway gw;
	private moveToolTipOutside move;
	private Vector3 precPosition;
	
	
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
		
			gw=GetComponent<Gateway>();
			toolTip=GameObject.Find("toolTip");
			toolTipText1=GameObject.Find("toolTipText1");
			toolTipText2=GameObject.Find("toolTipText2");
		
			move=GameObject.Find("blackPanel").GetComponent<moveToolTipOutside>();
			precPosition=new Vector3(0,0,0);
			
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
		
		if (Input.GetMouseButtonUp(0))
		{
			if (gameObject.transform.position.x+5!=precPosition.x && gameObject.transform.position.y+5!=precPosition.y)
			{
				toolTip.transform.position=new Vector3(gameObject.transform.position.x+5,gameObject.transform.position.y+5,-200);
				precPosition=new Vector3(gameObject.transform.position.x+5, gameObject.transform.position.y+5, -200);
			}
			else
			{
				toolTip.transform.position=new Vector3(-130,800,-200);
				precPosition=new Vector3(0,0,0);
			}
		}
			
	
		if(Input.GetMouseButtonDown(0))
		{
			

			//controlla che non sia lo stesso selezionato, se seleziono un altro gateway mostro le stat. se seleziono lo stesso disattivo il panel
			if (referencePanel.lastSelectedGateway!=gameObject)
			{
				
				if (owner==player){
					hackEvent.gatewayStart=gameObject;
					neutralizeEvent.gatewayStart=gameObject;				
					

					toolTipText1.GetComponent<OTTextSprite>().text="Atk: " + gw.getAtk();
					toolTipText2.GetComponent<OTTextSprite>().text="Def: " + gw.getDef();
					toolTipText1.GetComponent<OTTextSprite>().tintColor=networkManager.getColor(player);
					toolTipText2.GetComponent<OTTextSprite>().tintColor=networkManager.getColor(player);
					
					
				} 
				else
				{
					hackEvent.gatewayTarget=gameObject;
					neutralizeEvent.gatewayTarget=gameObject;
					
					if (hackEvent.gatewayStart)
					{
					
						toolTipText1.GetComponent<OTTextSprite>().text="Atk: " + hackEvent.gatewayStart.GetComponent<Gateway>().getAtk();
						toolTipText2.GetComponent<OTTextSprite>().text="Def: " + hackEvent.gatewayTarget.GetComponent<Gateway>().getDef();
						toolTipText1.GetComponent<OTTextSprite>().tintColor=networkManager.getColor(hackEvent.gatewayStart.GetComponent<Gateway>().getOwner());//colore del giocatore
						toolTipText2.GetComponent<OTTextSprite>().tintColor=networkManager.getColor(hackEvent.gatewayTarget.GetComponent<Gateway>().getOwner());//colore del proprietario del gateway

					}
					else
					{
						
						toolTipText1.GetComponent<OTTextSprite>().text="Atk: " + hackEvent.gatewayTarget.GetComponent<Gateway>().getAtk();
						toolTipText2.GetComponent<OTTextSprite>().text="Def: " + hackEvent.gatewayTarget.GetComponent<Gateway>().getDef();
						toolTipText1.GetComponent<OTTextSprite>().tintColor=networkManager.getColor(owner);
						toolTipText2.GetComponent<OTTextSprite>().tintColor=networkManager.getColor(owner);

					}
					
					
					
					
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

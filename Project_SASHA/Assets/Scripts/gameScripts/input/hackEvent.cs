using UnityEngine;
using System.Collections;

public class hackEvent : MonoBehaviour {
	
	public GameObject gatewayStart;
	public GameObject gatewayTarget;
	OTScale9Sprite sprite;
	private NetworkManager nwm = null;
	private string target,start;
	GameObject ray;
 	public GameObject rayPrefab; 
 	float lunghezzaRay;
 	float inclinazioneRay;
 	private float xt,xs,yt,ys;
 	Quaternion rotation;
	
	// Use this for initialization
	void Start () {
		
		nwm=GameObject.Find("referencePanel").GetComponent<NetworkManager>();
		gatewayStart=null;
		gatewayTarget=null;
		sprite=GetComponent<OTScale9Sprite>();
		sprite.tintColor=Color.red;
		sprite.onInput=click;
		
		
	}
	
	void Update()
	{
		if (gatewayStart)
		{
			sprite.tintColor=Color.blue;
			
		}
		else
		{
			sprite.tintColor=Color.red;
		}
		
	}
	
	void click(OTObject sprite)
	{
		if (Input.GetMouseButtonDown(0))
		{
			if(!gatewayStart)
			{
				print ("first select the start gateway from those you own");
			}
			else
			{
				if(gatewayTarget)
				{
					target=gatewayTarget.GetComponent<Gateway>().getState();
					start=gatewayStart.GetComponent<Gateway>().getState();
					nwm.SendHackRequest(start,target);
					
					xs=gatewayStart.transform.position.x;
				    ys=gatewayStart.transform.position.y;
				    xt=gatewayTarget.transform.position.x;
				    yt=gatewayTarget.transform.position.y;
				     
				    ray=Instantiate(rayPrefab) as GameObject;
				    ray.transform.position=new Vector2((xt+xs)/2,(yt+ys)/2);
				    //ray.transform.position.x=(xt-xs);
				    //ray.transform.position.y=yt-ys;  
				    lunghezzaRay=Mathf.Sqrt(Mathf.Pow((xt-xs),2)+Mathf.Pow((yt-ys),2));
				    ray.transform.localScale=new Vector2(lunghezzaRay,1);
				    inclinazioneRay=Mathf.Atan((yt-ys)/(xt-xs))*Mathf.Rad2Deg;
				    rotation.eulerAngles=new Vector3(0,0,inclinazioneRay);
				    ray.transform.rotation=rotation;
					
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class drag : MonoBehaviour {
	
	OTSprite sprite;
	imgRepo imgRepo;
	Vector3 position;
	
	// Use this for initialization
	void Start () {
				
		sprite = GetComponent<OTSprite>();
		
		gameObject.name=sprite.image.name;
		
		sprite.onDragStart=start;
		sprite.onDragEnd=end;
		imgRepo=GameObject.Find("imagesRepository").GetComponent<imgRepo>();
		
		
		
	}
	
	
	void start(OTObject sprite)
	{
		imgRepo.current=GetComponent<OTSprite>().image;
		imgRepo.currentSw=gameObject;
		position=gameObject.transform.position;
	}
	
	
	
	
	void end(OTObject sprite)
	{
		sprite.transform.position=position;
		
	}
	

	
	
	
	// Update is called once per frame
	void Update () {
	
				
		
	}
}

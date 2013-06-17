using UnityEngine;
using System.Collections;

public class printStat : MonoBehaviour {
	
	public Gateway gtw;
	private imgRepo imgRepo;
	private OTSprite imgSlot1,imgSlot2,imgSlot3;
	private drop drop;
	private string player;
	GameObject stats;
	
	void Start()
	{
		
		imgRepo=GameObject.Find("imagesRepository").GetComponent<imgRepo>();
		imgSlot1=GameObject.Find("imgSlot1").GetComponent<OTSprite>();
		imgSlot2=GameObject.Find("imgSlot2").GetComponent<OTSprite>();
		imgSlot3=GameObject.Find("imgSlot3").GetComponent<OTSprite>();
	}
	
	
	public void stat(Gateway gtw){
		player = GameObject.Find("referencePanel").GetComponent<NetworkManager>().getCurrentPlayer();
		stats=GameObject.Find("spotStats");
		stats.GetComponent<OTTextSprite>().text="Name: " + gtw.getName() + " \nType: " + gtw.getType() + "\nAttack: " + gtw.getAtk() + " Defence: " + gtw.getDef();
		string owner = gtw.getOwner();
		
		
		Vector3 resize = new Vector3(0.5f,0.8f,1f);
		
		if (gtw.getSlot(0)!="" && owner.Equals(player))
		{
			imgSlot1.image=imgRepo.getTxt(gtw.getSlot(0));
			imgSlot1.transform.localScale = resize;
		}
		else
		{
			imgSlot1.image=imgRepo.getTxt("empty");
			imgSlot1.transform.localScale = resize;
		}
		
		if (gtw.getSlot(1)!="" && owner.Equals(player))
		{
			imgSlot2.image=imgRepo.getTxt(gtw.getSlot(1));
			imgSlot2.transform.localScale = resize;
		}
		else
		{
			imgSlot2.image=imgRepo.getTxt("empty");
			imgSlot2.transform.localScale = resize;
		}
		
		if (gtw.getSlot(2)!="" && owner.Equals(player))
		{
			imgSlot3.image=imgRepo.getTxt(gtw.getSlot(2));
			imgSlot3.transform.localScale = resize;
		}
		else
		{
			imgSlot3.image=imgRepo.getTxt("empty");
			imgSlot3.transform.localScale = resize;
		}
	}
}

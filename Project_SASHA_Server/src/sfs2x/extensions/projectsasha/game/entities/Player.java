package sfs2x.extensions.projectsasha.game.entities;

import sfs2x.extensions.projectsasha.game.GameConsts;


import com.smartfoxserver.v2.entities.User;
//import sfs2x.extensions.projectsasha.game.User;
import com.smartfoxserver.v2.entities.data.SFSObject;

// Player class representing an individual soldier in the world simulation
public class Player 
{
	private User sfsUser; // SFS user that corresponds to this player
	private String name;
	private int money;

	
	
	public Player(User sfsUser) 
	{
		this.sfsUser = sfsUser;
		this.name = sfsUser.getName();
		this.money = 0;
	}


	public SFSObject toSFSObject()
	{
		SFSObject playerData = new SFSObject();

		playerData.putInt("id", sfsUser.getId());
		playerData.putUtfString("name", this.name);
		playerData.putInt("money", this.money);
		
		return playerData;
	}
	
	public SFSObject toInfosSFSObject()
	{
		SFSObject playerData = new SFSObject();
		
		playerData.putInt("id", sfsUser.getId());
		playerData.putUtfString("name", this.name);
		
		return playerData;
	}
	
	public String getName()
	{
		String ownerName ="none";
		if(this!=null)
			ownerName = this.name;
		return ownerName;
	}
	
	public void addMoney(int value)
	{
			this.money += value;
	}
	
	public void removeMoney(int value)
	{
		this.money -= value;
	}
	
	public int getMoney()
	{
		return this.money;
	}
}

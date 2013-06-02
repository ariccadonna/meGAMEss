package sfs2x.extensions.projectsasha.game.entities;

import java.util.Iterator;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;


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
	
	public String getUserName()
	{
		return this.name;
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
	
	public int getConqueredGateway(GameWorld world)
	{
		Iterator<String> itr = world.gateways.keySet().iterator();
		Gateway currentGW;
		String str;
		int counter = 0;
		
		while (itr.hasNext()) 
		{
			str = itr.next();
			currentGW = world.gateways.get(str);
			
			if(currentGW.getOwner() == this)
				counter++;
		}
		
		return counter;
		
	}
	
	public int getConqueredGateway(GameWorld world, String type)
	{
		Iterator<String> itr = world.gateways.keySet().iterator();
		Gateway currentGW;
		String str;
		int counter = 0;
		
		while (itr.hasNext()) 
		{
			str = itr.next();
			currentGW = world.gateways.get(str);
			
			if(currentGW.getClass().getSimpleName() != type)
				continue;
			
			if(currentGW.getOwner() == this)
				counter++;
		}
		
		return counter;
	}
	
	@Override 
	public String toString(){
		String returnString = "/*****************************************\\\n";
		returnString += " * Name:\t "+this.name+"\n";
		returnString += " * Money:\t "+this.money+"\n";
		returnString += "\\*****************************************/";
		return returnString;
		
	}
}

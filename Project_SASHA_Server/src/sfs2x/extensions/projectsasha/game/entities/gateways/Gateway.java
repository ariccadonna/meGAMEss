package sfs2x.extensions.projectsasha.game.entities.gateways;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.ia.Trace;
import sfs2x.extensions.projectsasha.game.entities.software.Software;

public abstract class Gateway 
{	
	
	private static int lastGatewayID = 1000;
	
	private List<Trace> traces;
	private Software[] installedSoftware;
	private Gateway[] neighboors;
	private Player owner;
	private String name, state;
	private int id;
	private boolean has_BruteForcer;
	private boolean has_BruteFirewall;

	public Gateway(Player owner, String name, String state)
	{
		this.owner = owner;
		this.name = name;
		this.state = state;
		this.id = getNewID();
		this.traces = new ArrayList<Trace>();
		this.installedSoftware = new Software[GameConsts.MAX_SOFTWARE_INSTALLED];
		this.has_BruteForcer = this.hasSoftware(GameConsts.BRUTEFORCER);
		this.has_BruteFirewall = this.hasSoftware(GameConsts.FIREWALL);
	}	
	
	
	//GETTERS
	
	public int getID()
	{
		return id;
	}

	public Player getOwner()
	{
		return owner;
	}
	
	public void setOwner(Player p)
	{
		this.owner = p;
	}
	
	public Gateway[] getNeighboors()
	{
		return neighboors;
	}
	
	public List<Trace> getTraces()
	{
		return traces;
	}
	
	public String getName()
	{
		return name;
	}
	
	public String getState()
	{
		return state;
	}
	
	public int getDefenceLevel()
	{
		int dl = getBaseDefenceLevel();
		
		for(Software s : installedSoftware)
		{
			if(s != null)
				dl += s.getDefenceLevel();
		}
		return dl;
	}
	
	public int getAttackLevel()
	{
		int dl = getBaseAttackLevel();
		
		for(Software s : installedSoftware)
		{
			if(s != null)
				dl += s.getAttackLevel();
		}
		return dl;
	}
	
	public Software[] getInstalledSoftwares()
	{
		return this.installedSoftware;
	}
	
	
	public Software getInstalledSoftware(Software type)
	{
		for(Software s : installedSoftware)
		{
			if (s.getClass() == type.getClass())
				return s;
		}
		return null;
	}
	
	
	//SETTERS
	
	public void setNeighborhoods(Gateway[] neighboors)
	{
		this.neighboors = neighboors;
	}
	
	
	//ABSTRACT METHODS

	protected abstract int getBaseAttackLevel();
	protected abstract int getBaseDefenceLevel();
	public abstract int getPaymentAmount();
	public abstract void onGatewayConquered(Player newOwner, Player oldOwner);
	
	
	//STATIC METHODS
	
	private static synchronized int getNewID()
	{
		return lastGatewayID++;
	}
	
	
	public static void hack(Gateway from, Gateway to){
		
		/*********************/
		/** hack logic here **/
		/*********************/
		
		System.out.print(from.getOwner().getName()+" is trying to hack "+to.state+" from "+from.state+": ");
		int difference = to.getDefenceLevel()-from.getAttackLevel();
		if(difference < 0){
			to.setOwner(from.getOwner());
			System.out.println("SUCCESS");
		}else{
			System.out.println("FAIL");
		}
	}
	
	//NON STATIC METHODS
	public void installSoftware(Software newSoftware, Player p)
	{
		if(this.owner!=p)
		{
			System.out.println("[" + this.state + "]-> " + "cant install software (" + newSoftware.getName() + "): " + p.getName()+" is not my owner (" + this.getOwner().getName() + ")");
			return;
		}
		
		if(this.hasSoftware(newSoftware))
		{
			System.out.println("[" + this.state + "]-> " + "Can't install software " + newSoftware.getName() + ": already installed!");
			return;
		}
		
		int available_slot = this.getNextSoftwareSlotAvailable();
		
		newSoftware.setVersion(1);
		
		if(available_slot < GameConsts.MAX_SOFTWARE_INSTALLED)
		{
			System.out.println("["+this.state+"]-> Installing "+newSoftware.getName()+" V"+newSoftware.getVersion());
			newSoftware.setSlot(available_slot);
			installedSoftware[available_slot] = newSoftware;
			
		}else
			System.out.println("["+this.state+"]-> You dont have enough slot for "+newSoftware.getName());
		
	}
	
	public void uninstallSoftware(Software theSoftware, Player p)
	{
		if(this.owner!=p)
		{
			System.out.println("[" + this.state + "]-> " + "cant uninstall software (" + theSoftware.getName() + "): " + p.getName()+" is not my owner (" + this.getOwner().getName() + ")");
			return;
		}
		
		if(this.hasSoftware(theSoftware))
		{
			System.out.println("["+this.state+"]-> Uninstalling "+theSoftware.getName()+" V"+theSoftware.getVersion());
			this.getInstalledSoftware(theSoftware).setVersion(0);
			this.getInstalledSoftware(theSoftware);
			installedSoftware[this.getInstalledSoftware(theSoftware).getSlot()] = null;
		}
	}
	
	public boolean hasSoftware(Software type){
		for(Software s : installedSoftware)
		{
			if(s != null && type.getClass() == s.getClass())
				return true;
		}
		return false;
	}
	
	public int getNextSoftwareSlotAvailable(){
		int slot = 0;
		
		for(Software s : installedSoftware)
			if(s != null)
				slot++;
		
		return slot;
	}
	
	public void upgradeSoftware(Software type){
		if(this.hasSoftware(type))
			this.getInstalledSoftware(type).downgrade();
		else
			System.out.println("["+this.getState()+"]-> Software "+type.getName()+" is not installed"); 
	}
	
	public void downgradeSoftware(Software type){
		if(this.hasSoftware(type))
			this.getInstalledSoftware(type).downgrade();
		else
			System.out.println("["+this.getState()+"]-> Software "+type.getName()+" is not installed"); 
	}
	
	
	@Override
	public String toString(){
		String ownerName = this.owner == null?"NEUTRAL":this.owner.getName();
		String returnString = "/*****************************************\\\n";
		returnString += " * Owner:\t "+ownerName+"\n";
		returnString += " * Name:\t "+this.name+"\n";
		returnString += " * State:\t "+this.state+"\n";
		returnString += " * Id:\t\t "+this.id+"\n";
		returnString += " * Attak:\t "+this.getAttackLevel()+"\n";
		returnString += " * Defence:\t "+this.getDefenceLevel()+"\n";
		returnString += " * Software:\t "+Arrays.toString(this.getInstalledSoftwares())+"\n";
		returnString += "\\*****************************************/";
		return returnString;
	}
}

package sfs2x.extensions.projectsasha.game.entities.gateways;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Hashtable;
import java.util.List;
import java.util.Map;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.ia.Trace;
import sfs2x.extensions.projectsasha.game.entities.software.Software;
import sfs2x.extensions.projectsasha.game.entities.software.SoftwareFactory;

public abstract class Gateway 
{	
	
	private static int lastGatewayID = 1000;
	
	private List<Trace> traces;
	private Software[] installedSoftware;
	private Gateway[] neighboors;
	private Map<String,Integer> distanceFromAttackGateway = new Hashtable<String,Integer>();
	private Player owner;
	private String name, state;
	private int id;


	public Gateway(Player owner, String name, String state)
	{
		this.owner = owner;
		this.name = name;
		this.state = state;
		this.id = getNewID();
		this.traces = new ArrayList<Trace>();
		this.installedSoftware = new Software[GameConsts.MAX_SOFTWARE_INSTALLED];
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
	
	public int getWeightByRelevance(int attackRelevance)
	{
		float traceCount=0;
		
		for(Trace t : traces)
			if(t.relevance == attackRelevance)
				traceCount++;
		return (int)((1/(1+traceCount))*100);
	}
	
	public int getWeightByDistance(String playerName)
	{
		return distanceFromAttackGateway.get(playerName);
	}
	
	public void setWeightByDistance(String playerName, int weight)
	{
		distanceFromAttackGateway.put(playerName, weight);
	}
	
	public void resetWeightByDistance(String playerName)
	{
		setWeightByDistance(playerName, 0);
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
	
	public Software getInstalledSoftware(String type)
	{
		for(Software s : installedSoftware)
		{
			if (s.getType() == type)
				return s;
		}
		return null;
	}
	
	public Software getInstalledSoftware(int slot)
	{
		return installedSoftware[slot];
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
	
	public static boolean hack(Gateway from, Gateway to){
		boolean ret = false;
		/*********************/
		/** hack logic here **/
		/*********************/
		if(GameConsts.DEBUG)
			System.out.print(from.getOwner().getName()+" is trying to hack "+to.state+" from "+from.state+": ");
		int difference = to.getDefenceLevel()-from.getAttackLevel();
		if(difference < 0){
			to.setOwner(from.getOwner());
			if(GameConsts.DEBUG)
				System.out.println("SUCCESS");
			ret = true;
		}else{
			if(GameConsts.DEBUG)
				System.out.println("FAIL");
			ret = false;
		}
		return ret;
	}
	
	//NON STATIC METHODS
	public void installSoftware(String type, Player hacker)
	{
		Software newSoftware = SoftwareFactory.makeSoftware(type);
		if(this.owner!=hacker)
		{
			if(GameConsts.DEBUG)
				if(this.owner == null)
					System.out.println("[" + this.state + "]-> " + "cant install software (" + newSoftware.getName() + "): " + hacker.getName()+" is not my owner (NEUTRAL)");
				else
					System.out.println("[" + this.state + "]-> " + "cant install software (" + newSoftware.getName() + "): " + hacker.getName()+" is not my owner (" + this.getOwner().getName() + ")");
			return;
		}
		
		if(this.hasSoftware(newSoftware.getType()) && !newSoftware.isCumulative())
		{
			if(GameConsts.DEBUG)
				System.out.println("[" + this.state + "]-> " + "Can't install software " + newSoftware.getName() + ": already installed!");
			return;
		}
		
		int available_slot = this.getNextSoftwareSlotAvailable();
		newSoftware.setVersion(1);
		
		if(available_slot < GameConsts.MAX_SOFTWARE_INSTALLED)
		{
			if(GameConsts.DEBUG)
				System.out.println("["+this.state+"]-> Installing "+newSoftware.getName()+" V"+newSoftware.getVersion()+" in slot "+(available_slot+1));
			newSoftware.setSlot(available_slot);
			installedSoftware[available_slot] = newSoftware;
			
		}else
			if(GameConsts.DEBUG)
				System.out.println("["+this.state+"]-> You dont have enough slot for "+newSoftware.getName());		
	}
	
	public void uninstallSoftware(String type, Player p)
	{
		this.uninstallSoftware(type, p, 0);
	}
	
	public void uninstallSoftware(String type, Player hacker, int slot)
	{
		Software newSoftware = SoftwareFactory.makeSoftware(type);
		
		if(this.owner!=hacker)
		{
			if(GameConsts.DEBUG)
				if(this.owner == null)
					System.out.println("[" + this.state + "]-> " + "cant uninstall software (" + newSoftware.getName() + "): " + hacker.getName()+" is not my owner (NEUTRAL)");
				else
					System.out.println("[" + this.state + "]-> " + "cant uninstall software (" + newSoftware.getName() + "): " + hacker.getName()+" is not my owner (" + this.getOwner().getName() + ")");
			return;
		}
		
		
		if(this.hasSoftware(type) && !this.getInstalledSoftware(type).isCumulative()) //installed and not cumulative
		{
			Software theSoftware = this.getInstalledSoftware(type); 
			if(GameConsts.DEBUG)
				System.out.println("["+this.state+"]-> Uninstalling "+theSoftware.getName()+" V"+theSoftware.getVersion());
			theSoftware.setVersion(0);
			installedSoftware[theSoftware.getSlot()] = null;
		}
		else if(this.hasSoftware(type)) //installed and cumulative
		{
			Software theSoftware = this.getInstalledSoftware(type); 
			if(GameConsts.DEBUG)
				System.out.println("["+this.state+"]-> Uninstalling "+theSoftware.getName()+" V"+theSoftware.getVersion()+" in slot "+(slot+1));
			this.getInstalledSoftware(slot).setVersion(0);
			installedSoftware[slot] = null;
		}
	}
	
	public boolean hasSoftware(String type){
		for(Software s : installedSoftware)
			if(s != null && type == s.getType())
				return true;
		return false;
	}
	
	public int getNextSoftwareSlotAvailable(){
		int slot = 0;
		
		for(Software s : installedSoftware)
			if(s != null)
				slot++;
		
		return slot;
	}
	
	public void upgradeSoftware(int slot, Player hacker){
		Software sw = null;
		try
		{
			sw = this.getInstalledSoftware(slot);
			this.upgradeSoftware(sw.getType(), hacker, slot);
		}
		catch(NullPointerException e)
		{
			if(GameConsts.DEBUG)
				System.out.println("Slot "+slot+" is empty");
		}
		
	}
	
	public void upgradeSoftware(String type, Player hacker){
		
		Software newSoftware = SoftwareFactory.makeSoftware(type);
		
		if(this.owner!=hacker)
		{
			if(GameConsts.DEBUG)
				if(this.owner == null)
					System.out.println("[" + this.state + "]-> " + "cant upgrade software (" + newSoftware.getName() + "): " + hacker.getName()+" is not my owner (NEUTRAL)");
				else
					System.out.println("[" + this.state + "]-> " + "cant upgrade software (" + newSoftware.getName() + "): " + hacker.getName()+" is not my owner (" + this.getOwner().getName() + ")");
			return;
		}
		
		if(this.hasSoftware(type))
			this.getInstalledSoftware(type).upgrade();
		else
			if(GameConsts.DEBUG)
				System.out.println("["+this.getState()+"]-> Software " + newSoftware.getName() + " is not installed"); 
	}
	
	public void upgradeSoftware(String type, Player hacker,  int slot){
		
		Software newSoftware = SoftwareFactory.makeSoftware(type);
		
		if(this.owner!=hacker)
		{
			if(GameConsts.DEBUG)
				if(this.owner == null)
					System.out.println("[" + this.state + "]-> " + "cant upgrade software (" + newSoftware.getName() + "): " + hacker.getName()+" is not my owner (NEUTRAL)");
				else
					System.out.println("[" + this.state + "]-> " + "cant upgrade software (" + newSoftware.getName() + "): " + hacker.getName()+" is not my owner (" + this.getOwner().getName() + ")");
			return;
		}
		
		if(this.hasSoftware(type))
			this.getInstalledSoftware(slot).upgrade();
		else
			if(GameConsts.DEBUG)
				System.out.println("["+this.getState()+"]-> Software " + newSoftware.getName() + " is not installed"); 
	}
	
	public void downgradeSoftware(String type, Player hacker){
		
		Software newSoftware = SoftwareFactory.makeSoftware(type);
		
		if(this.owner!=hacker)
		{
			if(GameConsts.DEBUG)
				System.out.println("[" + this.state + "]-> " + "cant upgrade software (" + newSoftware.getName() + "): " + hacker.getName()+" is not my owner (" + this.getOwner().getName() + ")");
			return;
		}
		
		if(this.hasSoftware(type))
			this.getInstalledSoftware(type).downgrade();
		else
			if(GameConsts.DEBUG)
				System.out.println("["+this.getState()+"]-> Software " + newSoftware.getName() + " is not installed"); 
	}
	
	public void downgradeSoftware(String type, Player hacker, int slot){
		
		Software newSoftware = SoftwareFactory.makeSoftware(type);
		
		if(this.owner!=hacker)
		{
			if(GameConsts.DEBUG)
				System.out.println("[" + this.state + "]-> " + "cant downgrade software (" + newSoftware.getName() + "): " + hacker.getName()+" is not my owner (" + this.getOwner().getName() + ")");
			return;
		}
		
		if(this.hasSoftware(type))
			this.getInstalledSoftware(slot).downgrade();
		else
			if(GameConsts.DEBUG)
				System.out.println("["+this.getState()+"]-> Software " + newSoftware.getName() + " is not installed"); 
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

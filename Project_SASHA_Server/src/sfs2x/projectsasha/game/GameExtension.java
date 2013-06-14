package sfs2x.projectsasha.game;
import java.util.Hashtable;
import java.util.Map;

import sfs2x.projectsasha.game.ia.AIThread;
import sfs2x.projectsasha.game.entities.Objective;
import sfs2x.projectsasha.game.entities.GameWorld;
import sfs2x.projectsasha.game.entities.Player;
import sfs2x.projectsasha.game.entities.Region;
import sfs2x.projectsasha.game.entities.gateways.Gateway;
import sfs2x.projectsasha.game.handlers.*;

import com.smartfoxserver.v2.core.SFSEventType;
import com.smartfoxserver.v2.extensions.SFSExtension;

public class GameExtension extends SFSExtension
{
	private GameWorld world; // Reference to World simulation model
	private static Map<String,Player> players = new Hashtable<String,Player>();
	private Objective[] gameObjectives = new Objective[6];
	private Region[] regions;
	private Gateway policePosition;
	private long startTime;
	
	@Override
	public void init()
	{
		startTime = System.currentTimeMillis();
		
		WorldInit(); 		// World init
		
		trace("----- GAME EXTENSION INITIALIZED! -----");
		addRequestHandler("getWorldSetup", WorldSetupHandler.class);
		trace("WorldSetupHandler	=>		INITIALIZED");
		
		addRequestHandler("getNeighborhoods", NeightborhoodHandler.class);
		trace("NeighborhoodsHandler	=>		INITIALIZED");
		
		addRequestHandler("getObjectives", ObjectiveHandler.class);
		trace("ObjectiveSetupHandler=>		INITIALIZED");
		
		addRequestHandler("hack", HackEventHandler.class);	
		trace("HackHandler			=>		INITIALIZED");
		
		addRequestHandler("playerInfo", PlayerInfoHandler.class);
		trace("PlayerInfoHandler	=>		INITIALIZED");
		
		addRequestHandler("gatewayInfo", GatewayInfoHandler.class);
		trace("GatewayInfoHandler 	=>		INITIALIZED");
		
		addRequestHandler("sync", SyncHandler.class);
		trace("SyncHandler 			=>		INITIALIZED");
		
		addRequestHandler("policePosition", PolicePositionHandler.class);
		trace("PolicePositionHandler=>		INITIALIZED");
		
		addRequestHandler("install", InstallSoftwareHandler.class);
		trace("InstallSoftwareHandler=>		INITIALIZED");
		
		addRequestHandler("shopInfo", ShopInfoHandler.class);
		trace("ShopInfoHandler		=>		INITIALIZED");
		
		addRequestHandler("buy", BuyItemHandler.class);
		trace("BuyItemHandler		=>		INITIALIZED");
		
		addRequestHandler("endGame", EndGameHandler.class);
		trace("EndGameHandler		=>		INITIALIZED");
		
		
		addEventHandler(SFSEventType.USER_DISCONNECT, OnUserGoneHandler.class);
		addEventHandler(SFSEventType.USER_LEAVE_ROOM, OnUserGoneHandler.class);
		addEventHandler(SFSEventType.USER_LOGOUT, OnUserGoneHandler.class);
		trace("----- GAME EXTENSION INITIALIZED! -----");
		
		startPoliceThread();// Starting police thread
		produceMoney(); 	// Starting money thread

	}
	
	@SuppressWarnings("deprecation")
	public void destroy() 
	{
	
		super.destroy();
		trace("----- GAME EXTENSION STOPPED! -----");
		removeRequestHandler("getWorldSetup");
		trace("WorldSetupHandler	=>		STOPPED");
		
		removeRequestHandler("hack");
		trace("HackHandler			=>		STOPPED");
		
		removeRequestHandler("getNeighborhoods");
		trace("NeighborhoodsHandler	=>		STOPPED");
		
		removeRequestHandler("getObjective");
		trace("ObjectiveSetupHandler=>		STOPPED");
		
		removeRequestHandler("playerInfo");
		trace("PlayerInfoHandler	=>		STOPPED");
		
		removeRequestHandler("gatewayInfo");
		trace("GatewayInfoHandler 	=>		STOPPED");
		
		removeRequestHandler("policePosition");
		trace("PolicePositionHandler=>		STOPPED");
		
		removeRequestHandler("install");
		trace("InstallSoftwareHandler=>		STOPPED");
		
		removeRequestHandler("shopInfo");
		trace("ShopInfoHandler		=>		STOPPED");
		
		removeRequestHandler("buy");
		trace("BuyItemHandler		=>		STOPPED");
		
		removeRequestHandler("endGame");
		trace("EndGameHandler		=>		STOPPED");
		
		trace("----- GAME EXTENSION STOPPED! -----");
		
		world.ai.stop();
		world.money.stop();
		
	}
	
	private void WorldInit()
	{
		trace("----- WORLD INIT -----");
		this.world = new GameWorld(0);
		trace("World Created");
		gameObjectives[0] = new Objective(world,"Main Target", "Hack 17 gateways", 17, GameConsts.BASE_GATEWAY);
		gameObjectives[1] = new Objective(world,"Hack.Edu", "Hack 4 academic gateways", 4, GameConsts.EDU_GATEWAY);
		gameObjectives[2] = new Objective(world,"Cops & Hackers", "Hack 4 military gateways", 4, GameConsts.MIL_GATEWAY);
		gameObjectives[3] = new Objective(world,"Hacker Impossible", "Hack 4 financial gateways", 4, GameConsts.SCI_GATEWAY);
		gameObjectives[4] = new Objective(world,"Rating: Hacker", "Hack 4 scientific gateways", 4, GameConsts.BANK_GATEWAY);
		gameObjectives[5] = new Objective(world,"Hack In Law", "Hack 4 government gateways", 4, GameConsts.GOV_GATEWAY);
		trace("Objectives created");
		regions = world.regions;
		world.money = new Thread(new MoneyThread(world));
		trace("Money Thread insantiated");
		world.ai = new AIThread(world, this);
		trace("AI Thread insantiated");

		trace("----- WORLD INIT DONE-----");
	}
	
	private void produceMoney() {
		world.money.start();

	}
	
	private void startPoliceThread()
	{
		world.ai.start();
	}
	
	public Player getPlayer(String name) 
	{
		return players.get(name);
	}
	
	public void putPlayer(Player p)
	{
		players.put(p.getName(), p);
	}
	
	public GameWorld getWorld() 
	{
		return this.world;
	}
	
	public Region[] getRegions() 
	{
		return this.regions;
	}
	
	public Region getGatewayRegion(Gateway gw) 
	{

		for(Region r : regions)
			for(Gateway g : r.gateways)
				if(gw.getState() == g.getState())
					return r;
		return null;
	}
	
	public Objective[] getObjectives()
	{
		return this.gameObjectives;
	}

	public void setPolicePosition(Gateway g)
	{
		policePosition = g;
	}
	
	public Gateway getPolicePosition()
	{
		return this.policePosition;
	}

	public long getTime() {
		return System.currentTimeMillis() - this.startTime;
	}
}

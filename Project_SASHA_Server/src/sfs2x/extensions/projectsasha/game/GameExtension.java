package sfs2x.extensions.projectsasha.game;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Set;

import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.Region;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.ia.AIThread;
import sfs2x.extensions.projectsasha.game.objectives.Objective;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;
import sfs2x.extensions.projectsasha.game.utils.TimerHelper;

import com.smartfoxserver.v2.core.SFSEventType;
import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.SFSZone;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.extensions.SFSExtension;

public class GameExtension extends SFSExtension
{
	private GameWorld world; // Reference to World simulation model
	private static Thread moneyThread;
	private static Map<String,Player> players = new Hashtable<String,Player>();
	private Objective[] gameObjectives = new Objective[6];
	private Region[] regions;
	private static Thread ai;
	private static Gateway policePosition;
	private long startTime;
	
	@Override
	public void init()
	{
		startTime = System.currentTimeMillis();
		WorldInit();
		startPoliceThread();
		produceMoney();
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
		addRequestHandler("getTime", GetTimeHandler.class);
		trace("GetTimeHandler		=>		INITIALIZED");
		addRequestHandler("gatewayInfo", GatewayInfoHandler.class);
		trace("GatewayInfoHandler 	=>		INITIALIZED");
		addRequestHandler("sync", SyncHandler.class);
		trace("SyncHandler 			=>		INITIALIZED");
		addRequestHandler("policePosition", PolicePositionHandler.class);
		trace("PolicePositionHandler=>		INITIALIZED");
		trace("----- GAME EXTENSION STOPPED! -----");
		
		addEventHandler(SFSEventType.USER_DISCONNECT, OnUserGoneHandler.class);
		addEventHandler(SFSEventType.USER_LEAVE_ROOM, OnUserGoneHandler.class);
		addEventHandler(SFSEventType.USER_LOGOUT, OnUserGoneHandler.class);
		trace("----- GAME EXTENSION INITIALIZED! -----");
		

	}
	
	@Override
	public void destroy() 
	{
		ai.stop();
		moneyThread.stop();
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
		removeRequestHandler("GetTimeHandler");
		trace("GetTimeHandler		=>		STOPPED");
		removeRequestHandler("gatewayInfo");
		trace("GatewayInfoHandler 	=>		STOPPED");
		removeRequestHandler("policePosition");
		trace("PolicePositionHandler=>		STOPPED");
		trace("----- GAME EXTENSION STOPPED! -----");
	}
	
	private void WorldInit()
	{
		trace("----- WORLD INIT -----");
		world = new GameWorld(0);
		
		gameObjectives[0] = new Objective(world,"Main Target", "Hack 17 gateways", 17, GameConsts.BASE_GATEWAY);
		gameObjectives[1] = new Objective(world,"Hack.Edu", "Hack 4 academic gateways", 4, GameConsts.EDU_GATEWAY);
		gameObjectives[2] = new Objective(world,"Cops & Hackers", "Hack 4 military gateways", 4, GameConsts.MIL_GATEWAY);
		gameObjectives[3] = new Objective(world,"Hacker Impossible", "Hack 4 financial gateways", 4, GameConsts.SCI_GATEWAY);
		gameObjectives[4] = new Objective(world,"Rating: Hacker", "Hack 4 scientific gateways", 4, GameConsts.BANK_GATEWAY);
		gameObjectives[5] = new Objective(world,"Hack In Law", "Hack 4 government gateways", 4, GameConsts.GOV_GATEWAY);
	
		regions = world.regions;
		moneyThread = new Thread(new MoneyThread(world));
		ai = new AIThread(world, this);

		trace("----- WORLD INIT DONE-----");
	}
	
	private static void produceMoney() {
		moneyThread.start();

	}
	
	private static void startPoliceThread()
	{
		ai.start();
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
	
	public static Gateway getPolicePosition()
	{
		return policePosition;
	}

	public long getTime() {
		return System.currentTimeMillis() - this.startTime;
	}
}

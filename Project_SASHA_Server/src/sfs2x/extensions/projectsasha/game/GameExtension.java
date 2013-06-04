package sfs2x.extensions.projectsasha.game;
import java.util.Hashtable;
import java.util.Map;

import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;

import com.smartfoxserver.v2.core.SFSEventType;
import com.smartfoxserver.v2.extensions.SFSExtension;

public class GameExtension extends SFSExtension
{
	private GameWorld world; // Reference to World simulation model
	private static Thread moneyThread;
	private static Map<String,Player> players = new Hashtable<String,Player>();
	
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
		
	@Override
	public void init()
	{
		WorldInit();
		
		trace("----- GAME EXTENSION INITIALIZED! -----");
		addRequestHandler("getWorldSetup", WorldSetupHandler.class);
		trace("WorldSetupHandler	=>		INITIALIZED");
		addRequestHandler("getNeighborhoods", NeightborhoodHandler.class);
		addRequestHandler("hack", HackEventHandler.class);	
		trace("HackHandler			=>		INITIALIZED");
		addRequestHandler("spawnMe", SpawnMeHandler.class);
		trace("SpawnMeHandler		=>		INITIALIZED");
		addRequestHandler("getTime", GetTimeHandler.class);
		trace("GetTimeHandler		=>		INITIALIZED");
		addRequestHandler("gatewayInfo", GatewayInfoHandler.class);
		trace("GatewayInfoHandler 	=>		INITIALIZED");
		
		addEventHandler(SFSEventType.USER_DISCONNECT, OnUserGoneHandler.class);
		addEventHandler(SFSEventType.USER_LEAVE_ROOM, OnUserGoneHandler.class);
		addEventHandler(SFSEventType.USER_LOGOUT, OnUserGoneHandler.class);
		trace("----- GAME EXTENSION INITIALIZED! -----");
		

	}
	
	@Override
	public void destroy() 
	{
		super.destroy();
		trace("----- GAME EXTENSION STOPPED! -----");
		removeRequestHandler("getWorldSetup");
		trace("WorldSetupHandler	=>		STOPPED");
		removeRequestHandler("hack");
		trace("HackHandler			=>		STOPPED");
		removeRequestHandler("SpawnMeHandler");
		trace("SpawnMeHandler		=>		STOPPED");
		removeRequestHandler("GetTimeHandler");
		trace("GetTimeHandler		=>		STOPPED");
		removeRequestHandler("gatewayInfo");
		trace("GatewayInfoHandler 	=>		STOPPED");
		trace("----- GAME EXTENSION STOPPED! -----");
	}
	
	private void WorldInit(){
		trace("----- WORLD INIT -----");
		world = new GameWorld(0);
		moneyThread = new Thread(new MoneyThread(world));

		trace("----- WORLD INIT DONE-----");
	}
	
	private static void produceMoney() {
		moneyThread.start();
	}
	
	
}

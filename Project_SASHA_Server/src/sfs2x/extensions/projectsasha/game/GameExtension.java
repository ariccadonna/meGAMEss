package sfs2x.extensions.projectsasha.game;
import sfs2x.extensions.projectsasha.game.entities.GameWorld;

import com.smartfoxserver.v2.core.SFSEventType;
import com.smartfoxserver.v2.extensions.SFSExtension;

public class GameExtension extends SFSExtension
{
	private GameWorld world; // Reference to World simulation model
	
	public GameWorld getWorld() 
	{
		return this.world;
	}
		
	@Override
	public void init()
	{
		WorldInit();
		
		trace("----- GAME EXTENSION INITIALIZED! -----");
		addRequestHandler("hack", HackEventHandler.class);	
		trace("HackHandler              	=>     INITIALIZED");
		addRequestHandler("spawnMe", SpawnMeHandler.class);
		trace("SpawnMeHandler               =>     INITIALIZED");
		addRequestHandler("getTime", GetTimeHandler.class);
		trace("GetTimeHandler               =>     INITIALIZED");
		
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
		removeRequestHandler("hack");
		trace("HackHandler              	=>     STOPPED");
		removeRequestHandler("SpawnMeHandler");
		trace("SpawnMeHandler               =>     STOPPED");
		removeRequestHandler("GetTimeHandler");
		trace("GetTimeHandler               =>     STOPPED");
		trace("----- GAME EXTENSION STOPPED! -----");
	}
	
	private void WorldInit(){
		trace("----- WORLD INIT -----");
		world = new GameWorld(0);
		trace("----- WORLD INIT DONE-----");
	}
	
	
}

package sfs2x.extensions.projectsasha.game.utils;
import sfs2x.extensions.projectsasha.game.*;

import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;
import com.smartfoxserver.v2.extensions.BaseServerEventHandler;
import com.smartfoxserver.v2.extensions.SFSExtension;
import sfs2x.extensions.projectsasha.game.GameExtension;
import sfs2x.extensions.projectsasha.game.entities.GameWorld;


// Helper methods to easily get current room or zone and precache the link to ExtensionHelper
public class RoomHelper {

	public static Room getCurrentRoom(BaseClientRequestHandler handler) 
	{
		return handler.getParentExtension().getParentRoom();
	}

	public static Room getCurrentRoom(SFSExtension extension)
	{
		return extension.getParentRoom();
	}

	public static GameWorld getWorld(BaseClientRequestHandler handler) 
	{
		GameExtension ext = (GameExtension) handler.getParentExtension();
		return ext.getWorld();
	}

	public static GameWorld getWorld(BaseServerEventHandler handler)
	{
		GameExtension ext = (GameExtension) handler.getParentExtension();
		return ext.getWorld();
	}


}

package sfs2x.extensions.projectsasha.login;


import com.smartfoxserver.v2.core.SFSEventType;
import com.smartfoxserver.v2.extensions.SFSExtension;

public class LoginExtension extends SFSExtension
{
		
	@Override
	public void init()
	{
		addEventHandler(SFSEventType.USER_LOGIN, LoginEventHandler.class);		
		addEventHandler(SFSEventType.USER_LOGOUT, LogoutEventHandler.class);
		addEventHandler(SFSEventType.USER_DISCONNECT, DisconnectEventHandler.class);
		addEventHandler(SFSEventType.BUDDY_ADD, AddBuddyHandler.class);
		addRequestHandler("createGameLobby", CreateGameLobby.class);
		addRequestHandler("startGame", StartGameHandler.class);
		trace("----- LOGIN MANAGER INITIALIZED! -----");
	}
	
	@Override
	public void destroy() 
	{
		super.destroy();
		removeEventHandler(SFSEventType.USER_LOGIN);
		removeEventHandler(SFSEventType.USER_LOGOUT);
		removeEventHandler(SFSEventType.USER_DISCONNECT);
		removeEventHandler(SFSEventType.BUDDY_ADD);
		removeRequestHandler("createGameLobby");
		removeRequestHandler("startGame");
		trace("----- LOGIN MANAGER STOPPED! -----");
	}
}

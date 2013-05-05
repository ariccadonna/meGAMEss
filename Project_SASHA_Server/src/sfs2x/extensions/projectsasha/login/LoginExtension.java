package sfs2x.extensions.projectsasha.login;


import com.smartfoxserver.v2.core.SFSEventType;
import com.smartfoxserver.v2.extensions.SFSExtension;

public class LoginExtension extends SFSExtension
{
		
	@Override
	public void init()
	{
		addEventHandler(SFSEventType.USER_LOGIN, LoginEventHandler.class);		
		addEventHandler(SFSEventType.USER_LOGOUT, LogoutEventHander.class);
		//addRequestHandler("setOnline", SetPlayerOnline.class);
		trace("----- LOGIN MANAGER INITIALIZED! -----");
	}
	
	@Override
	public void destroy() 
	{
		super.destroy();
		removeEventHandler(SFSEventType.USER_LOGIN);
		removeEventHandler(SFSEventType.USER_LOGOUT);
		trace("----- LOGIN MANAGER STOPPED! -----");
	}
}

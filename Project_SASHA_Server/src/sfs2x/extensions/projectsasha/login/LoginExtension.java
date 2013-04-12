package sfs2x.extensions.projectsasha.login;


import com.smartfoxserver.v2.core.SFSEventType;
import com.smartfoxserver.v2.extensions.SFSExtension;

public class LoginExtension extends SFSExtension
{
		
	@Override
	public void init()
	{
		addEventHandler(SFSEventType.USER_LOGIN, LoginEventHandler.class);		
		trace("   LOGIN MANAGER INITIALIZED ! ");
	}
	
	@Override
	public void destroy() 
	{
		super.destroy();
		removeEventHandler(SFSEventType.USER_LOGIN);
		trace("LOGIN MANAGER STOPPED ! ");
	}
		
	
	
	
}

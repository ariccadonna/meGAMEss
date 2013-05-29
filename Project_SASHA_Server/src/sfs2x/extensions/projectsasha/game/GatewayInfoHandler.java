package sfs2x.extensions.projectsasha.game;

import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.entities.software.Software;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class GatewayInfoHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){
		
		GameWorld world = RoomHelper.getWorld(this);
		Player p = new Player(sender);
		ISFSObject reback = SFSObject.newInstance();
		
		Gateway selectedGateway = world.gateways.get(params.getUtfString("selctedGateway"));
		
		reback.putUtfString("name", selectedGateway.getName());
		reback.putUtfString("state", selectedGateway.getState());
		reback.putInt("attack",selectedGateway.getAttackLevel());
		reback.putInt("defence", selectedGateway.getDefenceLevel());
		reback.putUtfString("type", selectedGateway.getClass().getSimpleName());
		
		trace("Gateway info request from " + p.getUserName() + " for " + selectedGateway.getState());
		if(selectedGateway.getOwner()!=null && selectedGateway.getOwner().getName() == p.getName())
		{
			Software[] installedSw = selectedGateway.getInstalledSoftwares();
			reback.putUtfString("owner", selectedGateway.getOwner().getName());
			for(int i = 0; i<GameConsts.MAX_SOFTWARE_INSTALLED; i++)
			{
				if(installedSw[i]!=null)
					reback.putUtfString("slot"+i, installedSw[i].getName()+" V"+installedSw[i].getVersion());
				else
					reback.putUtfString("slot"+i, "Empty");
			}	
				
		}
		else
		{
			reback.putUtfString("owner", "NONE");
		}
		send("gatewayInfo", reback, sender);
	}
}

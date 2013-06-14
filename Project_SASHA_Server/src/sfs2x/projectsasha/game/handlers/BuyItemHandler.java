package sfs2x.projectsasha.game.handlers;

import sfs2x.projectsasha.game.utils.RoomHelper;
import sfs2x.projectsasha.game.GameConsts;
import sfs2x.projectsasha.game.entities.GameWorld;
import sfs2x.projectsasha.game.entities.Player;
import sfs2x.projectsasha.game.entities.software.Software;
import sfs2x.projectsasha.game.entities.software.SoftwareFactory;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class BuyItemHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){
		
		String sw = params.getUtfString("software");
		trace(sw);
		GameWorld world = RoomHelper.getWorld(this);
		Player p = RoomHelper.getPlayer(this, sender.getName());
		
		String swName = nameToConst(sw.substring(0,sw.length()-1));
		int swVersion = Integer.parseInt(sw.substring(sw.length()-1));
		Software s = SoftwareFactory.makeSoftware(swName,swVersion);
		int bonus = (s.getCost() * p.getConqueredGateway(world, GameConsts.EDU_GATEWAY))/100;
		
		if(p.getMoney()<(s.getCost()-bonus))
		{
			sendError("NOTENOUGHMONEY", sender);
			return;
		}
		
		if(p.isFree()==false)
		{
			sendError("INVENTORYFULL", sender);
			return;
		}
		
		p.addInventory(world, s);
		ISFSObject reback = SFSObject.newInstance();
		reback.putBool("success", true);
		send("buy", reback, sender);	
	}
	
	private String nameToConst(String name)
	{
		String ret = "";
		if(name.equals("Brutus"))
			ret = GameConsts.BRUTEFORCER;
		if(name.equals("FireWool"))
			ret = GameConsts.FIREWALL;
		if(name.equals("Prelude_Detection_System"))
			ret = GameConsts.IDS;
		if(name.equals("LCleaner"))
			ret = GameConsts.LOGCLEANER;
		if(name.equals("Morton_Antivirus"))
			ret = GameConsts.ANTIVIRUS;
		if(name.equals("Blaster"))
			ret = GameConsts.VIRUS;
		if(name.equals("Deep_Throat"))
			ret = GameConsts.DEEPTHROAT;
		if(name.equals("WireBass"))
			ret = GameConsts.SNIFFER;
		if(name.equals("John_The_Rapper"))
			ret = GameConsts.DICTIONARY;
		if(name.equals("Thor_Garlic_Proxy"))
			ret = GameConsts.PROXY;
		return ret;
	}
	
	private void sendError(String errorType, User sender)
	{
		ISFSObject reback = SFSObject.newInstance();
		reback.putBool("success", false);
		
		//FIXME switch on errors
		if(errorType == "NOTENOUGHMONEY")
		{
			trace("Player hasn't enough money to buy this item");
			reback.putUtfString("error", "NOTENOUGHMONEY");
		}
		
		if(errorType == "INVENTORYFULL")
		{
			trace("Inventory is full");
			reback.putUtfString("error", "INVENTORYFULL");
		}
		
		send("error", reback, sender);	
	}
}

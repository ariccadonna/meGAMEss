package sfs2x.extensions.projectsasha.game;


import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.List;

import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;

import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.core.SFSEventParam;
import com.smartfoxserver.v2.core.ISFSEvent;
import com.smartfoxserver.v2.db.IDBManager;
import com.smartfoxserver.v2.exceptions.SFSException;
import com.smartfoxserver.v2.exceptions.SFSLoginException;
import com.smartfoxserver.v2.exceptions.SFSErrorData;
import com.smartfoxserver.v2.exceptions.SFSErrorCode;
import com.smartfoxserver.v2.extensions.BaseServerEventHandler;

public class OnUserGoneHandler extends BaseServerEventHandler {

	 @Override
	   public void handleServerEvent(ISFSEvent event) throws SFSException {
	     
		List<Room> joinedRooms = (List<Room>) event.getParameter(SFSEventParam.JOINED_ROOMS);
		User user = (User) event.getParameter(SFSEventParam.USER);
		Player p = RoomHelper.getPlayer(this, user.getName());
		GameWorld world = RoomHelper.getWorld(this);
		
		for(Gateway g : p.getAllConqueredGateway(world))
				g.setOwner(null);
		
	      for(Room r:joinedRooms)
	      {
	    	  user.removeJoinedRoom(r);
	      }
	      
	      IDBManager dbManager = getParentExtension().getParentZone().getDBManager();
	      Connection connection = null;
	      PreparedStatement stmt = null;
	      
	      try {
	         connection = dbManager.getConnection();
	         stmt = connection.prepareStatement("UPDATE "+ GameConsts.USER_TABLE + " SET online=? WHERE username = ?",Statement.RETURN_GENERATED_KEYS, ResultSet.TYPE_SCROLL_INSENSITIVE);
	 		 stmt.setBoolean(1, false);
	         stmt.setString(2, user.getName());
	         trace("User "+user.getName()+" quit game");
	         stmt.execute();
	         
	         /*
	          * FIXME: ridistribuzione delle risorse
	          */
	         
	      }
	      
	      catch (SQLException e) {
	         SFSErrorData errData = new SFSErrorData(SFSErrorCode.GENERIC_ERROR);
	         errData.addParameter("SQL Error: " + e.getMessage() + " " + stmt);
	         throw new SFSLoginException("A SQL Error occurred: " + e.getMessage(), errData);
	      }
	      
	      finally {
	         if (stmt != null) {
	            try { stmt.close(); }
	            catch (SQLException e) { }
	         }
	         if (connection != null) {
	            try { connection.close(); }
	            catch (SQLException e) { }
	         }
	      }
	      
	   }
}
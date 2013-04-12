package sfs2x.extensions.projectsasha.login;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import com.smartfoxserver.bitswarm.sessions.ISession;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.core.SFSEventParam;
import com.smartfoxserver.v2.core.ISFSEvent;
import com.smartfoxserver.v2.db.IDBManager;
import com.smartfoxserver.v2.exceptions.SFSException;
import com.smartfoxserver.v2.exceptions.SFSLoginException;
import com.smartfoxserver.v2.exceptions.SFSErrorData;
import com.smartfoxserver.v2.exceptions.SFSErrorCode;
import com.smartfoxserver.v2.extensions.BaseServerEventHandler;

public class LoginEventHandler extends BaseServerEventHandler
{
   
   @Override
   public void handleServerEvent(ISFSEvent event) throws SFSException {
      String clientUsername = (String) event.getParameter(SFSEventParam.LOGIN_NAME);
      String clientHash = (String) event.getParameter(SFSEventParam.LOGIN_PASSWORD);
      ISession clientSession = (ISession)event.getParameter(SFSEventParam.SESSION);
      
      String dbUsername = null;
      String dbHash = null;
      
      if(clientHash == "") {
         SFSErrorData data = new SFSErrorData(SFSErrorCode.LOGIN_BAD_PASSWORD);
         throw new SFSLoginException("You are not logged in!", data);
      }
      
      IDBManager dbManager = getParentExtension().getParentZone().getDBManager();
      Connection connection = null;
      PreparedStatement stmt = null;
      try {
         connection = dbManager.getConnection();
         stmt = connection.prepareStatement("SELECT * FROM ? WHERE username = ?;",Statement.RETURN_GENERATED_KEYS, ResultSet.TYPE_SCROLL_INSENSITIVE);
         stmt.setString(1, LoginConsts.USER_TABLE);
         stmt.setString(2, clientUsername);;
         
         ResultSet result = stmt.executeQuery();
         
         if(!result.first()) {
            SFSErrorData errData = new SFSErrorData(SFSErrorCode.LOGIN_BAD_USERNAME);
            errData.addParameter(clientUsername);
            throw new SFSLoginException("Bad id for user: " + clientUsername, errData);
         }
         
         dbUsername = result.getString("username");
         dbHash = result.getString("password");

         boolean verify = getApi().checkSecurePassword(clientSession, dbHash, clientHash);
   
         if (!verify) {
            SFSErrorData data = new SFSErrorData(SFSErrorCode.LOGIN_BAD_PASSWORD);
            throw new SFSLoginException("Incorrect password.", data);
         } else {
            if(dbUsername.compareTo(clientUsername) != 0) {
               throw new SFSLoginException("Nice try...");
            }
         }
         
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
package sfs2x.extensions.projectsasha.game.utils;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Random;

import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.SFSExtension;


import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.GameExtension;
import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;


public class ServerBatch extends SFSExtension implements Runnable
{
	private ResultSet res;
	private int timeOut, count;
	private Connection dbConn;
	private Room game;


	
	public ServerBatch(Connection dbConn, Room game, int timeOut)
	{
		this.dbConn = dbConn;
		this.game = game;
		this.timeOut = timeOut;
	}
	
	public boolean populateServerList()
	{
		// Grab a connection from the DBManager connection pool
        try 
        {	
			res = dbConn.prepareStatement("SELECT * FROM world",
					Statement.NO_GENERATED_KEYS, ResultSet.TYPE_SCROLL_INSENSITIVE).executeQuery();
        	//Gateways setup
        	while(res.next())	
        	{
        	//	GameWorld.gateways..add(new Gateway());				
		       
        	}	
        	
			
				
			return true;
        } 
        catch (SQLException e)
        { 
        	return false;
		}
        	  
	}		
		
	@Override
	public void run()
	{
	}
	
	
	@Override
	public void init() {}	
}

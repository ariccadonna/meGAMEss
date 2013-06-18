package sfs2x.projectsasha.tests;

import java.util.List;
import java.util.Map;

import com.smartfoxserver.bitswarm.sessions.ISession;
import com.smartfoxserver.v2.buddylist.BuddyProperties;
import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.Zone;
import com.smartfoxserver.v2.entities.data.ISFSArray;
import com.smartfoxserver.v2.entities.variables.UserVariable;
import com.smartfoxserver.v2.exceptions.SFSVariableException;
import com.smartfoxserver.v2.util.IDisconnectionReason;

public class User implements com.smartfoxserver.v2.entities.User {
	private String name;
	
	public User(String name) 
	{
		this.name = name;
		
	}
	
	public void setName(String name){
		this.name = name;
	}
	
	public String getName(){
		return this.name;
	}
	
	public int getId(){
		return 0;
	}

	@Override
	public void addCreatedRoom(Room arg0) {
		 
		
	}

	@Override
	public void addJoinedRoom(Room arg0) {
		 
		
	}

	@Override
	public boolean containsProperty(Object arg0) {
		 
		return false;
	}

	@Override
	public boolean containsVariable(String arg0) {
		 
		return false;
	}

	@Override
	public void disconnect(IDisconnectionReason arg0) {
		 
		
	}

	@Override
	public int getBadWordsWarnings() {
		 
		return 0;
	}

	@Override
	public BuddyProperties getBuddyProperties() {
		 
		return null;
	}

	@Override
	public List<Room> getCreatedRooms() {
		 
		return null;
	}

	@Override
	public String getDump() {
		 
		return null;
	}

	@Override
	public int getFloodWarnings() {
		 
		return 0;
	}

	@Override
	public String getIpAddress() {
		 
		return null;
	}

	@Override
	public List<Room> getJoinedRooms() {
		 
		return null;
	}

	@Override
	public Room getLastJoinedRoom() {
		 
		return null;
	}

	@Override
	public long getLastRequestTime() {
		 
		return 0;
	}

	@Override
	public long getLoginTime() {
		 
		return 0;
	}

	@Override
	public int getMaxAllowedVariables() {
		 
		return 0;
	}

	@Override
	public int getOwnedRoomsCount() {
		 
		return 0;
	}

	@Override
	public int getPlayerId() {
		 
		return 0;
	}

	@Override
	public int getPlayerId(Room arg0) {
		 
		return 0;
	}

	@Override
	public Map<Room, Integer> getPlayerIds() {
		 
		return null;
	}

	@Override
	public short getPrivilegeId() {
		 
		return 0;
	}

	@Override
	public Object getProperty(Object arg0) {
		 
		return null;
	}

	@Override
	public int getReconnectionSeconds() {
		 
		return 0;
	}

	@Override
	public ISession getSession() {
		 
		return null;
	}

	@Override
	public List<String> getSubscribedGroups() {
		 
		return null;
	}

	@Override
	public ISFSArray getUserVariablesData() {
		 
		return null;
	}

	@Override
	public UserVariable getVariable(String arg0) {
		 
		return null;
	}

	@Override
	public List<UserVariable> getVariables() {
		 
		return null;
	}

	@Override
	public int getVariablesCount() {
		 
		return 0;
	}

	@Override
	public Zone getZone() {
		 
		return null;
	}

	@Override
	public boolean isBeingKicked() {
		 
		return false;
	}

	@Override
	public boolean isConnected() {
		 
		return false;
	}

	@Override
	public boolean isJoinedInRoom(Room arg0) {
		 
		return false;
	}

	@Override
	public boolean isJoining() {
		 
		return false;
	}

	@Override
	public boolean isLocal() {
		 
		return false;
	}

	@Override
	public boolean isNpc() {
		 
		return false;
	}

	@Override
	public boolean isPlayer() {
		 
		return false;
	}

	@Override
	public boolean isPlayer(Room arg0) {
		 
		return false;
	}

	@Override
	public boolean isSpectator() {
		 
		return false;
	}

	@Override
	public boolean isSpectator(Room arg0) {
		 
		return false;
	}

	@Override
	public boolean isSubscribedToGroup(String arg0) {
		 
		return false;
	}

	@Override
	public boolean isSuperUser() {
		 
		return false;
	}

	@Override
	public void removeCreatedRoom(Room arg0) {
		 
		
	}

	@Override
	public void removeJoinedRoom(Room arg0) {
		 
		
	}

	@Override
	public void removeProperty(Object arg0) {
		 
		
	}

	@Override
	public void removeVariable(String arg0) {
		 
		
	}

	@Override
	public void setBadWordsWarnings(int arg0) {
		 
		
	}

	@Override
	public void setBeingKicked(boolean arg0) {
		 
		
	}

	@Override
	public void setConnected(boolean arg0) {
		 
		
	}

	@Override
	public void setFloodWarnings(int arg0) {
		 
		
	}

	@Override
	public void setJoining(boolean arg0) {
		 
		
	}

	@Override
	public void setLastLoginTime(long arg0) {
		 
		
	}

	@Override
	public void setLastRequestTime(long arg0) {
		 
		
	}

	@Override
	public void setMaxAllowedVariables(int arg0) {
		 
		
	}

	@Override
	public void setPlayerId(int arg0, Room arg1) {
		 
		
	}

	@Override
	public void setPrivilegeId(short arg0) {
		 
		
	}

	@Override
	public void setProperty(Object arg0, Object arg1) {
		 
		
	}

	@Override
	public void setReconnectionSeconds(int arg0) {
		 
		
	}

	@Override
	public void setVariable(UserVariable arg0) throws SFSVariableException {
		 
		
	}

	@Override
	public void setVariables(List<UserVariable> arg0)
			throws SFSVariableException {
		 
		
	}

	@Override
	public void setZone(Zone arg0) {
		 
		
	}

	@Override
	public void subscribeGroup(String arg0) {
		 
		
	}

	@Override
	public ISFSArray toSFSArray() {
		 
		return null;
	}

	@Override
	public ISFSArray toSFSArray(Room arg0) {
		 
		return null;
	}

	@Override
	public void unsubscribeGroup(String arg0) {
		 
		
	}

	@Override
	public void updateLastRequestTime() {
		 
		
	}
}

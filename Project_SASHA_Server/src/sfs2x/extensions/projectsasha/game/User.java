package sfs2x.extensions.projectsasha.game;

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
		// TODO Auto-generated method stub
		
	}

	@Override
	public void addJoinedRoom(Room arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public boolean containsProperty(Object arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean containsVariable(String arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public void disconnect(IDisconnectionReason arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public int getBadWordsWarnings() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public BuddyProperties getBuddyProperties() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public List<Room> getCreatedRooms() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public String getDump() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public int getFloodWarnings() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public String getIpAddress() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public List<Room> getJoinedRooms() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public Room getLastJoinedRoom() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public long getLastRequestTime() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public long getLoginTime() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public int getMaxAllowedVariables() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public int getOwnedRoomsCount() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public int getPlayerId() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public int getPlayerId(Room arg0) {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public Map<Room, Integer> getPlayerIds() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public short getPrivilegeId() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public Object getProperty(Object arg0) {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public int getReconnectionSeconds() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public ISession getSession() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public List<String> getSubscribedGroups() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public ISFSArray getUserVariablesData() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public UserVariable getVariable(String arg0) {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public List<UserVariable> getVariables() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public int getVariablesCount() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public Zone getZone() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public boolean isBeingKicked() {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isConnected() {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isJoinedInRoom(Room arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isJoining() {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isLocal() {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isNpc() {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isPlayer() {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isPlayer(Room arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isSpectator() {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isSpectator(Room arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isSubscribedToGroup(String arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isSuperUser() {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public void removeCreatedRoom(Room arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void removeJoinedRoom(Room arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void removeProperty(Object arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void removeVariable(String arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setBadWordsWarnings(int arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setBeingKicked(boolean arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setConnected(boolean arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setFloodWarnings(int arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setJoining(boolean arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setLastLoginTime(long arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setLastRequestTime(long arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setMaxAllowedVariables(int arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setPlayerId(int arg0, Room arg1) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setPrivilegeId(short arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setProperty(Object arg0, Object arg1) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setReconnectionSeconds(int arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setVariable(UserVariable arg0) throws SFSVariableException {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setVariables(List<UserVariable> arg0)
			throws SFSVariableException {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setZone(Zone arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void subscribeGroup(String arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public ISFSArray toSFSArray() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public ISFSArray toSFSArray(Room arg0) {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public void unsubscribeGroup(String arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void updateLastRequestTime() {
		// TODO Auto-generated method stub
		
	}
}

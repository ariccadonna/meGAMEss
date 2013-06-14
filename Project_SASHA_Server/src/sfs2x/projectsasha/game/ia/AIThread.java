package sfs2x.projectsasha.game.ia;
import sfs2x.projectsasha.game.GameConsts;
import sfs2x.projectsasha.game.GameExtension;
import sfs2x.projectsasha.game.entities.GameWorld;
import sfs2x.projectsasha.game.entities.gateways.Gateway;

import com.smartfoxserver.v2.extensions.SFSExtension;

public class AIThread extends Thread 
{

	public Police police;
	public GameExtension gameExtension;
	
	public AIThread(GameWorld currentWorld, SFSExtension gameExtension)
	{
		this.gameExtension = (GameExtension) gameExtension;
		police = new Police(currentWorld);
	}
	
	public AIThread(GameWorld currentWorld)
	{
		police = new Police(currentWorld);
	}

	
	@Override
	public void run()
	{
		long startTime, finalTime;
		
		for(long tickCount = 0;;tickCount++)
		{
			 startTime = System.currentTimeMillis();
			
			 if(tickCount % GameConsts.POLICE_SLEEP_TIME == 0)
			 {
				 police.followNextTrace();
				 setCurrentGateway(police.getCurrentgateway());
			 }
			 
			 
			 finalTime = GameConsts.IA_THREAD_SLEEP_TIME -(System.currentTimeMillis()-startTime);
			 if (finalTime > 0)
			 {
				 try {Thread.sleep(finalTime);}
				 catch (InterruptedException e) {}
			 }
			 else
			 {
				 System.out.println("WARNING: IA thread can't keep up!");
			 }
		}
	}
	
	public void setCurrentGateway(Gateway g)
	{
		this.gameExtension.setPolicePosition(g);
	}
}

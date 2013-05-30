package sfs2x.extensions.projectsasha.game.ia;
import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.GameWorld;

public class AIThread extends Thread 
{
	private GameWorld currentWorld;
	
	public AIThread(GameWorld currentWorld)
	{
		this.currentWorld = currentWorld;
	}

	
	public Police police = new Police(this.currentWorld);
	
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
}

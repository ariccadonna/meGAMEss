package sfs2x.extensions.projectsasha.game.ia;
import sfs2x.extensions.projectsasha.game.GameConsts;

public class AIThread extends Thread 
{
	public Police police = new Police();
	
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

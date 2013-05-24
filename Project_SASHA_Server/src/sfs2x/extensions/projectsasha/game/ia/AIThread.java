package sfs2x.extensions.projectsasha.game.ia;

public class AIThread extends Thread 
{
	@Override
	public void run()
	{
		long startTime, finalTime;
		
		for(;;)
		{
			 startTime = System.currentTimeMillis();
			
			 //TODO: Insert here IA code
			 
			 finalTime = 50 -(System.currentTimeMillis()-startTime);
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

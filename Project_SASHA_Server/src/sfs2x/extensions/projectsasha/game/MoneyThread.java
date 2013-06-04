package sfs2x.extensions.projectsasha.game;

import java.util.Iterator;
import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.Region;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.GameConsts;

public class MoneyThread implements Runnable
{
	private GameWorld world;
	
	public MoneyThread(GameWorld world) {
        this.world = world;
    }
    
	@Override
	public void run()
	{
		while(true)
		{
			try 
			{
				Thread.sleep(GameConsts.MONEY_UPDATE_TIME);
				
				/* Money logic here */
				Iterator<String> iterator = world.gateways.keySet().iterator();
				Region[] regions = world.regions;
				boolean giveBonusMoney =  false;
				Player prevOwner = null;
				for(Region region : regions){
					int size = region.gateways.length;
					prevOwner = region.gateways[0].getOwner(); 
					if(prevOwner == null)
							continue;
					
					for(int i = 1; i < size-1; i++)
					{
						if(region.gateways[i].getOwner()!=prevOwner)
						{
							giveBonusMoney = false;
							break;
						}
						else
						{
							giveBonusMoney = true;
							prevOwner = region.gateways[i].getOwner();
						}
					}
					if(giveBonusMoney)
						prevOwner.addMoney(size*GameConsts.BONUS_MONEY_PER_REGION +
								 		  prevOwner.getConqueredGateway(world, GameConsts.BANK_GATEWAY) * 
								 		  GameConsts.BANK_BONUS_MULTIPLIER);
				}
				 while (iterator.hasNext()) 
			        {
			        	
			            String key = (String) iterator.next();
			            Gateway currentGateway = world.gateways.get(key);
			            
			            if(currentGateway.getOwner()!=null)
			            	currentGateway.getOwner().addMoney(currentGateway.getPaymentAmount());
			        }		 
		 	} 
			catch (InterruptedException e){e.printStackTrace();}
		}
	}
}

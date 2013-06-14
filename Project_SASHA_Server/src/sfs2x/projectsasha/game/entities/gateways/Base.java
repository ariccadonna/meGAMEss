package sfs2x.projectsasha.game.entities.gateways;

import sfs2x.projectsasha.game.GameConsts;
import sfs2x.projectsasha.game.entities.Player;

public class Base extends Gateway
{
	
	public Base(Player owner, String name, String state, int x, int y, float lat, float lon) 
	{
		super(owner, name, state, x, y, lat, lon);
	}

	
	//GETTERS
	
	@Override
	public int getPaymentAmount() 
	{
		return GameConsts.BASE_PAYMENT_AMOUNT;
	}

	@Override
	protected int getBaseAttackLevel()
	{
		return GameConsts.BASE_ATTACK_LEVEL;
	}

	@Override
	protected int getBaseDefenceLevel()
	{
		return GameConsts.BASE_DEFENCE_LEVEL;
	}
	
	@Override
	public void onGatewayConquered(Player newOwner, Player oldOwner)
	{
		
	}

}

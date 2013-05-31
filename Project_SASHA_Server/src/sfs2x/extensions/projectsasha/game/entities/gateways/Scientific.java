package sfs2x.extensions.projectsasha.game.entities.gateways;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.Player;

public class Scientific extends Gateway
{
	
	public Scientific(Player owner, String name, String state, int x, int y) 
	{
		super(owner, name, state, x, y);
	}

	
	//GETTERS
	
	@Override
	public int getPaymentAmount() 
	{
		return GameConsts.SCI_BASE_PAYMENT_AMOUNT;
	}

	@Override
	protected int getBaseAttackLevel()
	{
		return GameConsts.SCI_BASE_ATTACK_LEVEL;
	}

	@Override
	protected int getBaseDefenceLevel()
	{
		return GameConsts.SCI_BASE_DEFENCE_LEVEL;
	}
	
	@Override
	public void onGatewayConquered(Player newOwner, Player oldOwner)
	{
		
	}
}

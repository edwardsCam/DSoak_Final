package SharedObjectTester;

import static org.junit.Assert.*;

import java.net.UnknownHostException;

import org.junit.Test;

import SharedObject.GamePlayerInfo;
import SharedObject.PlayerInfo;
import SharedObject.PublicEndPoint;

public class GamePlayerInfoTester 
{
	@Test
	public void test_EveryThing() throws UnknownHostException
	{
		GamePlayerInfo gp1 = new GamePlayerInfo();
		assertEquals(0, gp1.GameId);
		assertNull(gp1.Player);
		assertNull(gp1.Status);
		
		PublicEndPoint ep1 = new PublicEndPoint("swcwin.serv.usu.edu:35420");
	
		PlayerInfo pInfo = new PlayerInfo();
		pInfo.EndPoint = ep1;
		pInfo.PlayerId = 100;
		pInfo.Status =  PlayerInfo.StateCode.ONLINE;
		
		gp1 = new GamePlayerInfo();
		gp1.GameId = 10;
		gp1.Player = pInfo;
		gp1.Status = GamePlayerInfo.StatusCode.WINNER;
		
		assertEquals(10, gp1.GameId);
		assertEquals(pInfo, gp1.Player);
		assertEquals(GamePlayerInfo.StatusCode.WINNER, gp1.Status);
		
	}

}

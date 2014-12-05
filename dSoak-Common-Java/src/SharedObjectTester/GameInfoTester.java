package SharedObjectTester;

import static org.junit.Assert.*;

import java.net.UnknownHostException;

import org.junit.Test;

import SharedObject.GameInfo;
import SharedObject.PublicEndPoint;

public class GameInfoTester  
{
	@Test
	public void test_EveryThing() throws UnknownHostException 
	{
		GameInfo g1 = new GameInfo();
		
		assertEquals(null, g1.FlightManagerEP);
		assertEquals(0, g1.GameId);
		assertEquals(0, g1.MaxPlayers);
		assertNull(g1.Status);
		
		PublicEndPoint ep1 = new PublicEndPoint("buzz.serv.usu.edu:20011");
		
		GameInfo g2 = new GameInfo();
		g2.FlightManagerEP = ep1;
		g2.FightManagerId = 2;
		g2.GameId = 10;
		g2.MaxPlayers = 5;
		g2.Status = GameInfo.StatusCode.AVAILABLE;
		g2.MaxThieves = 2;
		
		assertTrue(ep1.getHost().equals(g2.FlightManagerEP.getHost()));
		assertEquals(2, g2.FightManagerId);
		assertEquals(10, g2.GameId);
		assertEquals(5,g2.MaxPlayers);
		assertEquals(GameInfo.StatusCode.AVAILABLE, g2.Status);
		assertEquals(2, g2.MaxThieves);
	}
}

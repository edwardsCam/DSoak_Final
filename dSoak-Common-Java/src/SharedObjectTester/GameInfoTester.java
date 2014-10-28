package SharedObjectTester;

import static org.junit.Assert.*;

import java.io.Serializable;

import org.junit.Test;

import SharedObject.GameInfo;
import SharedObject.GameInfo.StatusCode;
import SharedObject.PublicEndPoint;

public class GameInfoTester  
{
	@Test
	public void test_EveryThing() 
	{
		GameInfo g1 = new GameInfo();
		
		assertEquals(null, g1.FlightManagerEP);
		assertEquals(0, g1.GameId);
		assertEquals(0, g1.MaxPlayers);
		assertNull(g1.Status);
		
		PublicEndPoint ep1 = new PublicEndPoint();
		ep1.Host("buzz.serv.usu.edu");
		ep1.Port(20011);
		
		GameInfo g2 = new GameInfo();
		g2.FlightManagerEP = ep1;
		g2.GameId = 10;
		g2.MaxPlayers = 5;
		g2.Status = GameInfo.StatusCode.AVAILABLE;
		
		assertTrue(ep1.Host().equals(g2.FlightManagerEP.Host()));
		assertEquals(10, g2.GameId);
		assertEquals(5,g2.MaxPlayers);
		assertEquals(GameInfo.StatusCode.AVAILABLE, g2.Status);
	}
}

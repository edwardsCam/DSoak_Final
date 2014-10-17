package SharedObjectTester;

import static org.junit.Assert.*;

import org.junit.Test;

import SharedObject.PlayerInfo;
import SharedObject.PlayerInfo.StateCode;
import SharedObject.PublicEndPoint;

public class PlayerInfoTester {

	@Test
	public void test_EveryThing()
	{
		PlayerInfo p1 = new PlayerInfo();
		assertEquals(0, p1.PlayerId);
		assertEquals(null, p1.EndPoint);
		assertEquals(null, p1.Status);
		
		
		PublicEndPoint ep1 = new PublicEndPoint();
		ep1.Host =  "swcwin.serv.usu.edu";
		ep1.Port = 35420;
		
		PlayerInfo p2 = new PlayerInfo();
		p2.EndPoint = ep1;
		p2.PlayerId = 101;
		p2.Status = StateCode.OffLine;
		
		assertEquals(101, p2.PlayerId);
		assertTrue(ep1.Host.equals(p2.EndPoint.Host));
		assertEquals(StateCode.OffLine, p2.Status);
	}
}
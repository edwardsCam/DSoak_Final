package SharedObjectTester;

import static org.junit.Assert.*;

import java.net.UnknownHostException;

import org.junit.Test;

import SharedObject.PlayerInfo;
import SharedObject.PlayerInfo.StateCode;
import SharedObject.PublicEndPoint;

public class PlayerInfoTester {

	@Test
	public void test_EveryThing() throws UnknownHostException
	{
		PlayerInfo p1 = new PlayerInfo();
		assertEquals(0, p1.PlayerId);
		assertEquals(null, p1.EndPoint);
		assertEquals(null, p1.Status);
		
		
		PublicEndPoint ep1 = new PublicEndPoint("swcwin.serv.usu.edu:35420");
		
		PlayerInfo p2 = new PlayerInfo();
		p2.EndPoint = ep1;
		p2.PlayerId = 101;
		p2.Status = StateCode.OFFLINE;
		
		assertEquals(101, p2.PlayerId);
		assertTrue(ep1.getHost().equals(p2.EndPoint.getHost()));
		assertEquals(StateCode.OFFLINE, p2.Status);
		
		PlayerInfo p3 = p2.Copy();
		assertEquals(p2.PlayerId, p3.PlayerId);
		assertTrue(ep1.getHost().equals(p3.EndPoint.getHost()));
		assertEquals(p2.Status, p3.Status);
	}
}
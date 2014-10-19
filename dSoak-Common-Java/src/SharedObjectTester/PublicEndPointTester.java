package SharedObjectTester;

import static org.junit.Assert.*;

import java.net.UnknownHostException;

import org.junit.Test;

import SharedObject.PublicEndPoint;

public class PublicEndPointTester {

	@Test
	public void test_EveryThing() throws UnknownHostException 
	{
		PublicEndPoint ep1 = new PublicEndPoint();
		
		assertNull(ep1.Host);
		assertEquals(0, ep1.Port);
		assertNotNull(ep1.Port);
		assertNull(ep1.Host);
		assertNull(ep1.GetIPEndPoint());
		
		PublicEndPoint ep2 = new PublicEndPoint();
		ep2.Host =  "swcwin.serv.usu.edu";
		ep2.Port = 35420;
		
		assertTrue(ep2.Host.equals("swcwin.serv.usu.edu"));
		assertEquals(35420, ep2.Port);
	}
}
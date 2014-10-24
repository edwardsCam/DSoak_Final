package SharedObjectTester;

import static org.junit.Assert.*;

import java.net.InetAddress;
import java.net.UnknownHostException;

import org.junit.Test;

import SharedObject.IPEndPoint;

public class IPEndPointTester 
{
	@Test
	public void test_EveryThing() throws UnknownHostException 
	{
		IPEndPoint ep1 = new IPEndPoint();
		ep1.Address = InetAddress.getByName("127.0.0.1");
		ep1.Port = 12345;
		
		assertTrue(ep1.Address.getHostAddress().equals("127.0.0.1"));
		assertTrue(ep1.Address.getHostName().equals("127.0.0.1"));
		assertEquals(12345, ep1.Port);
		
		IPEndPoint ep2 = new IPEndPoint();
		ep2.Address = InetAddress.getByName("swcwin.serv.usu.edu");
		ep2.Port = 12345;
		
		assertTrue(ep2.Address.getHostAddress().equals("129.123.41.13"));
		assertTrue(ep2.Address.getHostName().equals("swcwin.serv.usu.edu"));
		
		IPEndPoint ep3 = new IPEndPoint();
		ep3.Address = InetAddress.getByName("localhost");
		ep3.Port = 12345;
		
		assertTrue(ep3.Address.getHostAddress().equals("127.0.0.1"));
		assertTrue(ep3.Address.getHostName().equals("localhost"));
	}
}
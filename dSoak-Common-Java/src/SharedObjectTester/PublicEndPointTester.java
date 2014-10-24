package SharedObjectTester;

import static org.junit.Assert.*;

import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.UnknownHostException;

import org.junit.Test;

import SharedObject.IPEndPoint;
import SharedObject.PublicEndPoint;

public class PublicEndPointTester {

	@Test
	public void test_Constructor() throws UnknownHostException 
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
		
		PublicEndPoint ep3 = new PublicEndPoint();
		ep3 = ep2;
		assertTrue(ep2.GetIPEndPoint().Address.getHostName().equals(ep3.GetIPEndPoint().Address.getHostName()));
		assertTrue(ep2.GetIPEndPoint().Address.getHostAddress().equals(ep3.GetIPEndPoint().Address.getHostAddress()));
		assertEquals(ep2.GetIPEndPoint().Port, ep3.GetIPEndPoint().Port);
		
		PublicEndPoint ep4 = new PublicEndPoint();
		IPEndPoint ipep = new  IPEndPoint("swcwin.serv.usu.edu", 1234);
		ep4.SettIPEndPoint(ipep);
		
		assertTrue(ep4.Host.equals(ipep.Address.getHostAddress()));
		assertEquals(ep4.Port, ipep.Port);
	}
	
	@Test
	public void test_GetIPEndPoint_SetIPEndPoint() throws UnknownHostException 
	{
		InetAddress add = InetAddress.getByName("swcwin.serv.usu.edu");
		InetSocketAddress sockAdd = new InetSocketAddress(add, 12345);
		IPEndPoint ep = IPEndPoint.Create(sockAdd);
		PublicEndPoint ipEP = new PublicEndPoint();
		ipEP.SettIPEndPoint(ep);
		
		assertNotNull(ipEP);
		assertEquals(sockAdd.getPort(), ep.Port);
		assertTrue(sockAdd.getAddress().getHostName().equals(add.getHostName()));
	}
	
	public void test_LookupAddress() throws UnknownHostException 
	{
		PublicEndPoint ep1 = new PublicEndPoint();
		ep1.Host =  "swcwin.serv.usu.edu";
		ep1.Port = 35420;
		
		IPEndPoint ep2 = new IPEndPoint();
		ep2 = ep1.GetIPEndPoint();
		
		assertEquals(ep2.Port, ep1.Port);
		assertTrue(ep1.Host.equals(ep2.Address.getHostName()));
		assertEquals(ep2.Address, PublicEndPoint.LookupAddress("swcwin.serv.usu.edu"));
		assertTrue(ep2.Address.getHostAddress().equals(PublicEndPoint.LookupAddress("swcwin.serv.usu.edu")));
		assertTrue(ep2.Address.getHostName().equals(PublicEndPoint.LookupAddress("swcwin.serv.usu.edu")));
		
		System.out.println(PublicEndPoint.LookupAddress("swcwin.serv.usu.edu").toString());
		
	}
}
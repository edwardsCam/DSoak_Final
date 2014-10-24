package SharedObjectTester;

import static org.junit.Assert.*;

import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.UnknownHostException;

import org.junit.Test;

import SharedObject.IPEndPoint;

public class IPEndPointTester 
{
	@Test
	public void test_Constructor1() throws UnknownHostException 
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
	
	@Test
	public void test_Constructor2() throws UnknownHostException 
	{
		String str = "127.0.0.1";
		IPEndPoint ep1 = new IPEndPoint(str, 1234);
		assertNotNull(ep1);
		assertEquals(1234, ep1.Port);
		assertTrue(str.equals(ep1.Address.getHostName()));
	}
	
	@Test
	public void test_Constructor3() throws UnknownHostException 
	{
		long l = 1272152412;
		IPEndPoint ep1 = new IPEndPoint(l, 1234);
		assertNotNull(ep1);
		assertEquals(1234, ep1.Port);
		String str = Long.toString(1272152412);
		assertEquals(InetAddress.getByName(str), ep1.Address);
	}
	
	@Test
	public void test_Constructor4() throws UnknownHostException 
	{
		InetAddress add = InetAddress.getByName("localhost");
		InetSocketAddress sockAdd = new InetSocketAddress(add, 12345);
		IPEndPoint ep1 = new IPEndPoint(sockAdd);
		assertNotNull(ep1);
		assertEquals(12345, ep1.Port);
		assertEquals(InetAddress.getByName("localhost"), ep1.Address);
	}
	
	@Test
	public void test_Constructor5() throws UnknownHostException 
	{
		InetAddress add = InetAddress.getByName("localhost");
		IPEndPoint ep1 = new IPEndPoint(add, 12345);
		assertNotNull(ep1);
		assertEquals(12345, ep1.Port);
		assertEquals(InetAddress.getByName("localhost"), ep1.Address);
	}
	
	@Test
	public void test_Create() throws UnknownHostException 
	{
		InetAddress add = InetAddress.getByName("localhost");
		InetSocketAddress sockAdd = new InetSocketAddress(add, 12345);
		IPEndPoint ep1 = IPEndPoint.Create(sockAdd);
		assertNotNull(ep1);
		assertEquals(12345, ep1.Port);
		assertEquals(InetAddress.getByName("localhost"), ep1.Address);
	}
	
}
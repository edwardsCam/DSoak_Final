package SharedObject;

import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.UnknownHostException;

import com.google.gson.annotations.Expose;

public class IPEndPoint 
{
	@Expose public InetAddress Address;
	@Expose public int Port;
	public static final int MinPort = 0, MaxPort = 65535;
	
	public IPEndPoint() {
	
	}
	
	public IPEndPoint(String str, int port) throws UnknownHostException
	{
		this.Address = InetAddress.getByName(str);
		this.Port = port;
	}
	
	public IPEndPoint(long address, int port) throws UnknownHostException
	{
		String str = Long.toString(address);
		this.Address = InetAddress.getByName(str);
		this.Port = port;
	}
	
	public IPEndPoint(InetSocketAddress socketAddress)
	{
		this.Address = socketAddress.getAddress();
		this.Port = socketAddress.getPort();
	}
	
	public IPEndPoint(InetAddress address, int port) 
	{
		Address = address;
		Port = port;
	}
	
	public static IPEndPoint Create(InetSocketAddress socketAddress)
	{  
		IPEndPoint result = new IPEndPoint();
		result.Address = socketAddress.getAddress();
		result.Port = socketAddress.getPort();
		return result;
	}
	
	public String toString() {
		return Address.getHostAddress() + ":" + Port;
	}
}

package SharedObject;

import java.io.Serializable;
import java.net.Inet4Address;
import java.net.InetAddress;
import java.net.UnknownHostException;

public class PublicEndPoint implements Serializable
{
	private static final long serialVersionUID = 1L;
	public String Host;
	public int Port;
	
	public IPEndPoint GetIPEndPoint() throws UnknownHostException
    {
		   IPEndPoint result = null;
           if (Host != null)
               result = new IPEndPoint(Host, Port);
           return result;
    }
	
	public void SettIPEndPoint(IPEndPoint value)
    {
		if (value != null)
        {
			Host = value.Address.getHostAddress();
            Port = value.Port;
         }
    }
	
	public static InetAddress LookupAddress(String host) throws UnknownHostException
    {
		InetAddress result = null;
		InetAddress[] addressList = InetAddress.getAllByName(host);
        for (int i = 0; i < addressList.length && result == null; i++)
        {
        	if (addressList[i].getHostAddress().equals(host) && result instanceof Inet4Address)
                result = addressList[i];
        }
        return result;
    }
}
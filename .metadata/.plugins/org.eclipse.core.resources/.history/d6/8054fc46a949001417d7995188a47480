package SharedObject;

import java.net.Inet4Address;
import java.net.InetAddress;
import java.net.UnknownHostException;

public class PublicEndPoint 
{
	public String Host;
	public int Port;
	
	public IPEndPoint GetIPEndPoint() throws UnknownHostException
    {
        return new IPEndPoint(LookupAddress(Host), Port);
    }
	
	public void SettIPEndPoint(IPEndPoint value)
    {
		if (value != null)
        {
			Host = value.Address.toString();
            Port = value.Port;
         }
    }
	
	public static InetAddress LookupAddress(String host) throws UnknownHostException
    {
		// the first way
		//InetAddress result = InetAddress.getByName(host);
		//return result;
		//
		InetAddress result2 = null;
		InetAddress[] addressList = InetAddress.getAllByName(host);
        for (int i = 0; i < addressList.length && result2 == null; i++)
            if (addressList[i].getHostAddress().equals(host) && result2 instanceof Inet4Address)
                result2 = addressList[i];
        return result2;
    }
}
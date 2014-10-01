package SharedObject;

import java.io.Serializable;
import java.net.Inet4Address;
import java.net.InetAddress;
import java.net.UnknownHostException;

public class PublicEndPoint implements Serializable
{
	private static final long SerialVersionUID = -145514789630468800L;
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
		InetAddress result = null;
		InetAddress[] addressList = InetAddress.getAllByName(host);
        for (int i = 0; i < addressList.length && result == null; i++)
            if (addressList[i].getHostAddress().equals(host) && result instanceof Inet4Address)
                result = addressList[i];
        return result;
    }
}
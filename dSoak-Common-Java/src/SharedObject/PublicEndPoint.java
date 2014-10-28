package SharedObject;

import java.io.Serializable;
import java.net.InetAddress;
import java.net.UnknownHostException;

public class PublicEndPoint implements Serializable
{
	private static final long serialVersionUID = 1L;
	private InetAddress myIPAddress= null;
	private IPEndPoint myIPEndPoint = null;
	private String myHost;
	private int myPort;

	public PublicEndPoint() {	}
	
	public PublicEndPoint(String hostnameAndPort) 
	{
		HostAndPort(hostnameAndPort);
	}
	
	public String HostAndPort()
	{
		return ((myHost == null) ? ("0.0.0.0:" + myPort) : myHost.toString() + ":" + myPort);
	}
	
	public void HostAndPort(String value)
	{
		SetHostAndPortValue(value);
	}
	
	public String Host()
	{
		return myHost;
	}
	
	public void Host(String value)
	{
		myHost = value;
		myIPAddress = null;
		myIPEndPoint = null;
	}
	
	public int Port()
	{
		return myPort;
	}
	
	public void Port(int value)
	{
		myPort = value;
		myIPEndPoint = null;
	}
	
	private void SetHostAndPortValue(String str) 
	{
		if (str != null)
		{
			String[] tmp = str.split(":");
			if ((tmp.length == 2) && (tmp[0] != null))
			{
				Host(tmp[0]);
				Port(Integer.parseInt(tmp[1]));
			}
		}
	}

	public IPEndPoint IPEndPoint() throws UnknownHostException
    {
		if (myIPEndPoint == null) 
		{
			if  (myIPAddress == null)
			{
				if (Host() != null)
				{
					myIPAddress = LookupAddress(Host());
				}
				if  (myIPAddress != null)
				{
					myIPEndPoint =  new IPEndPoint(myIPAddress, Port());
				}
			}
		}
		return myIPEndPoint;
    }
	
	public void IPEndPoint(IPEndPoint value)
    {
		if (value != null)
        {
			Host(value.Address.getHostAddress());
            Port(value.Port);
            myIPAddress = value.Address;
            myIPEndPoint = value;
         }
    }
	
	public static InetAddress LookupAddress(String host) throws UnknownHostException
    {
		InetAddress result = null;
		InetAddress[] addressList = InetAddress.getAllByName(host);
		
        for (int i = 0; i < addressList.length && result == null; i++)
        {
        	String str1 = addressList[i].getHostName();
        	String str2 = addressList[i].getHostAddress();
        	if (str1.equals(host))
                result = InetAddress.getByName(str2);
        }
        return result;
    }
	
	@Override
	public String toString()
	{
		return Host() + ":" + Port();
	}
}
package SharedObject;

import java.io.Serializable;

public class PlayerInfo implements Serializable
{
	private static final long serialVersionUID = 1L;
	
    public int PlayerId;
    public PublicEndPoint EndPoint;
    public StateCode Status;
   
    public enum StateCode
   	{
   		UNKNOWN((short)0),
   		ONLINE((short)1), 
   		OFFLINE((short)2); 
   		
   		short value;
   		StateCode(short va) { this.value = va;	}
   		
   		public short getValue()
   		{
   			return value;
   		}
   	}
}

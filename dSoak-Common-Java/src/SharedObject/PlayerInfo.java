package SharedObject;

import java.io.Serializable;
import java.util.Date;

public class PlayerInfo implements Serializable
{
	private static final long serialVersionUID = 1L;
	
    public short PlayerId;
    public PublicEndPoint EndPoint;
    public StateCode Status;
    public Date AliveTimestamp;
    
    public PlayerInfo Copy()
    {
        PlayerInfo result = new PlayerInfo();
        result.CopyFrom(this);
        return result;
    }
    
    protected void CopyFrom(PlayerInfo orig)
    {
        PlayerId = orig.PlayerId;
        EndPoint = orig.EndPoint;
        Status = orig.Status;
    }
    
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

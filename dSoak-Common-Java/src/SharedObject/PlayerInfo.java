package SharedObject;

import java.util.Date;

import com.google.gson.annotations.Expose;

public class PlayerInfo 
{
	@Expose public short PlayerId; 
	@Expose public PublicEndPoint EndPoint; 
	@Expose public StateCode Status; 
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
   		
	   	public static StateCode setValue(short b)
	   	 {
	   		StateCode temp = null;
		    for (StateCode  t : StateCode.values()) 
		    {
		    	if (t.value == b) {
		    		temp = t;
		        }
		    }
		    return temp;
		  }
   	}
}

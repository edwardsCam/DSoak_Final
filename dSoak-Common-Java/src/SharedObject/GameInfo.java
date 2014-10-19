package SharedObject;

import java.io.Serializable;

public class GameInfo implements Serializable
{
	private static final long serialVersionUID = -8579515000505941289L;
	public short GameId;
    public PublicEndPoint FlightManagerEP;
    public StatusCode Status;
    public short MaxPlayers;
    
    public enum StatusCode
	{
		UNKNOWN((short)0),
		AVAILABLE((short)1), 
		INPROGRESS((short)2), 
		COMPLETE((short)3), 
		CANCLED((short)4); 
		
		short value;
		StatusCode(short va) { this.value = va;	}
		
		public short getValue()
		{
			return value;
		}
	}
}

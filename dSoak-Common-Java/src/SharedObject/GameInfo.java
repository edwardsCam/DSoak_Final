package SharedObject;

import java.io.Serializable;

public class GameInfo implements Serializable
{
	private static final long serialVersionUID = 1L;
	public short GameId;
    public PublicEndPoint FlightManagerEP;
    public StatusCode Status;
    public short MaxPlayers;
    
    public enum StatusCode
	{
		AVAILABLE, INPROGRESS, COMPLETE, CANCLED; 
	
		StatusCode() {	}
	}
}
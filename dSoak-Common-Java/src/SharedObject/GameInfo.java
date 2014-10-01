package SharedObject;

import java.io.Serializable;

public class GameInfo implements Serializable
{
	private static final long SerialVersionUID = -1455876467430468998L;
	public short GameId;
    public PublicEndPoint FlightManagerEP;
    public StatusCode Status;
    public short MaxPlayers;
    
    public enum StatusCode
	{
		AVAILABLE, INPROGRESS, COMPLETE, CANCLED; 
	
		StatusCode() {
		
		}
	}
}
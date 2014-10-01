package SharedObject;

import java.io.Serializable;

public class PlayerInfo implements Serializable
{
	private static final long SerialVersionUID = -1455333467430654800L; 
	public enum StateCode { OnLine, OffLine };
    public int PlayerId;
    public PublicEndPoint EndPoint;
    public StateCode Status;
}

package SharedObject;

import java.io.Serializable;

public class PlayerInfo implements Serializable
{
	private static final long serialVersionUID = 1L;
	public enum StateCode { OnLine, OffLine };
    public int PlayerId;
    public PublicEndPoint EndPoint;
    public StateCode Status;
}

package SharedObject;

public class PlayerInfo 
{
	 public enum StateCode { OnLine, OffLine };
     public int PlayerId;
     public PublicEndPoint EndPoint;
     public StateCode Status;
}

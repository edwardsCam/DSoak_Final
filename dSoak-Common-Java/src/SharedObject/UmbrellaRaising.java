package SharedObject;

import java.io.Serializable;
import java.util.Date;

public class UmbrellaRaising implements Serializable
{
	private static final long SerialVersionUID = -145532167430468998L; 
	public short GameId;
    public short PlayerId;
    public short UmbrellaId;
    public Date AtTime;
}

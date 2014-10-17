package SharedObject;

import java.io.Serializable;
import java.util.Date;

public class UmbrellaRaising implements Serializable
{
	private static final long serialVersionUID = 1L;
	public short GameId;
    public short PlayerId;
    public short UmbrellaId;
    public Date AtTime;
}

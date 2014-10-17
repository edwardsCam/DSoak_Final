package Messages;
import java.io.Serializable;

import SharedObject.*;

public class BalloonPurchased extends Message implements Serializable
{
	private static final long serialVersionUID = -7580640607652110571L;
	
	public Balloon Balloon;
}

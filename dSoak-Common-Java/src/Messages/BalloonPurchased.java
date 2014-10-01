package Messages;
import java.io.Serializable;

import SharedObject.*;

public class BalloonPurchased extends Message implements Serializable
{
	private static final long SerialVersionUID = -1455333437430468120L;
	public Balloon Balloon;
}

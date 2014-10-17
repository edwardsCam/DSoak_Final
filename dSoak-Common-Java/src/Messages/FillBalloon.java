package Messages;
import java.io.Serializable;

import SharedObject.*;

public class FillBalloon extends ResourceRequest implements Serializable
{
	private static final long serialVersionUID = -6539311549193170393L;
	
	public Balloon Balloon;
}

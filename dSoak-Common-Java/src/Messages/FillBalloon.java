package Messages;
import java.io.Serializable;

import SharedObject.*;

public class FillBalloon extends ResourceRequest implements Serializable
{
	private static final long SerialVersionUID = -1451233437430468800L;
	public Balloon Balloon;
}

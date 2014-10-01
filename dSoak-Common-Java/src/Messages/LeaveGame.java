package Messages;

import java.io.Serializable;

public class LeaveGame extends Message implements Serializable
{
	private static final long SerialVersionUID = -1455339437430468800L;
	public short GameId;
}

package Messages;

import java.io.Serializable;

public class LeaveGame extends Message implements Serializable
{
	private static final long serialVersionUID = 7360788984493439522L;
	
	public short GameId;
}

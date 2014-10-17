package Messages;

import java.io.Serializable;

import SharedObject.PlayerInfo;

public class JoinGame extends Message implements Serializable
{
	private static final long serialVersionUID = -6052191176204707748L;
	
	public short GameId;
	public PlayerInfo Player;
}

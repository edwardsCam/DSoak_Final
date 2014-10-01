package Messages;

import java.io.Serializable;

import SharedObject.PlayerInfo;

public class JoinGame extends Message implements Serializable
{
	private static final long SerialVersionUID = -1255333437430468800L;
	public short GameId;
	public PlayerInfo Player;
}

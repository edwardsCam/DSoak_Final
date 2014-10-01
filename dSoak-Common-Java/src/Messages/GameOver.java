package Messages;

import java.io.Serializable;

import SharedObject.PlayerInfo;

public class GameOver extends Message implements Serializable
{
	private static final long SerialVersionUID = -1455233437430468800L;
	public short GameId;
	public PlayerInfo Winner;
}

package Messages;

import java.io.Serializable;

import SharedObject.PlayerInfo;

public class GameOver extends Message implements Serializable
{
	private static final long serialVersionUID = 3733186951527175746L;
	
	public short GameId;
	public PlayerInfo Winner;
}

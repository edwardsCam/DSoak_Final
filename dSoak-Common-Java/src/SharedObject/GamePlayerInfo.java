package SharedObject;

import java.io.Serializable;

public class GamePlayerInfo implements Serializable
{
	private static final long serialVersionUID = 1951609679759293604L;
	
	public enum StatusCode { InGame, LeftGame, Winner };
	
	public short GameId; 
	public PlayerInfo Player;
	public StatusCode Status;
}

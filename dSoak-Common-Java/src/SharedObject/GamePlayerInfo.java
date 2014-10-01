package SharedObject;

import java.io.Serializable;

public class GamePlayerInfo implements Serializable
{
	private static final long SerialVersionUID = -1781333467430468998L; 
	public enum StatusCode { InGame, LeftGame, Winner };
	public short GameId; 
	public PlayerInfo Player;
	public StatusCode Status;
}

package Messages;

import java.io.Serializable;
import java.util.List;

import SharedObject.Penny;

public class GameJoined extends Message implements Serializable
{
	private static final long SerialVersionUID = -1455333437430469100L;
	public short GameId;
	public List<Penny> Pennies;
}

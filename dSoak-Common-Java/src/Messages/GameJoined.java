package Messages;

import java.io.Serializable;
import java.util.List;

import SharedObject.Penny;

public class GameJoined extends Message implements Serializable
{
	private static final long serialVersionUID = -7287080222819817227L;
	
	public short GameId;
    public short InitialLifePoints;
	public List<Penny> Pennies;
}

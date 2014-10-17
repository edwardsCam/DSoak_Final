package Messages;

import java.io.Serializable;
import java.util.List;

import SharedObject.*;

public class GameData extends Message implements Serializable
{
	private static final long serialVersionUID = 8209022363040598597L;
	
	public GameInfo Info;
	public List<PlayerInfo> Players;
}

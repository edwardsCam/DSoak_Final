package Messages;

import java.io.Serializable;
import java.util.List;

import SharedObject.*;

public class GameData extends Message implements Serializable
{
	private static final long SerialVersionUID = -1455326437430468800L;
	public GameInfo Info;
	public List<PlayerInfo> Players;
}

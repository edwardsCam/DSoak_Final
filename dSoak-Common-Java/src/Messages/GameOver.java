package Messages;


import SharedObject.PlayerInfo;

public class GameOver extends Message 
{
	public short GameId; 
	public PlayerInfo Winner;
}

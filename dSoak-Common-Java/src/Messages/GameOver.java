package Messages;


import com.google.gson.annotations.Expose;

import SharedObject.PlayerInfo;

public class GameOver extends Message 
{
	@Expose public short GameId; 
	@Expose public PlayerInfo Winner;
}

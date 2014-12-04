package Messages;

import com.google.gson.annotations.Expose;

import SharedObject.PlayerInfo;

public class JoinGame extends Message 
{
	@Expose public short GameId;
	@Expose public PlayerInfo Player;
}

package Messages;

import java.util.List;

import com.google.gson.annotations.Expose;

import SharedObject.Penny;

public class GameJoined extends Message 
{
	@Expose public short GameId;
	@Expose public short InitialLifePoints;
	@Expose public List<Penny> Pennies;
}

package Messages;

import com.google.gson.annotations.Expose;

public class LeaveGame extends Message 
{
	@Expose public short GameId;
}

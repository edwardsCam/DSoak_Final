package Messages;

import com.google.gson.annotations.Expose;

public abstract class StealingBase extends Message
{
	@Expose public short GameId;
	@Expose public short TargetProcessId;
	@Expose public short ThiefId;
}

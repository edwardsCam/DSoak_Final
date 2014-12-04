package Messages;

import com.google.gson.annotations.Expose;

public class Continue extends Message 
{
	@Expose public short MissingReplyDeqNr;
}

package Messages;

import java.io.Serializable;

public class Continue extends Message implements Serializable
{
	private static final long serialVersionUID = 4096127881936671018L;
	
	public short MissingReplyDeqNr;
}

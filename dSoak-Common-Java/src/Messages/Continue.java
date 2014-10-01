package Messages;

import java.io.Serializable;

public class Continue extends Message implements Serializable
{
	private static final long SerialVersionUID = -1455333437430461100L;
	public short MissingReplyDeqNr;
}

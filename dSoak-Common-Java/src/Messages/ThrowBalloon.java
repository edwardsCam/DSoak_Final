package Messages;

import java.io.Serializable;

import SharedObject.*;

public class ThrowBalloon extends Message implements Serializable
{
	private static final long serialVersionUID = 1119847867053235579L;
	 
	public short GameId;
    public Balloon Balloon;
    public short TargetPlayerId;
}

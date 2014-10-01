package Messages;

import java.io.Serializable;

import SharedObject.*;

public class ThrowBalloon extends Message implements Serializable
{
	private static final long SerialVersionUID = -1455223437430468800L; 
	public short GameId;
    public Balloon Balloon;
    public short TargetPlayerId;
}

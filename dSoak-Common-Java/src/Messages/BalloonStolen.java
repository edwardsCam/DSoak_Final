package Messages;

import java.io.Serializable;

import SharedObject.Balloon;

public class BalloonStolen extends StealingBase implements Serializable
{
	private static final long serialVersionUID = 7684966139890319072L;
	public Balloon StolenBalloon;
}

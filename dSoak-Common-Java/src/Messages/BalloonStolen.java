package Messages;

import com.google.gson.annotations.Expose;

import SharedObject.Balloon;

public class BalloonStolen extends StealingBase 
{
	@Expose public Balloon StolenBalloon;
}

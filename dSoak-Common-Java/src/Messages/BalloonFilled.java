package Messages;

import com.google.gson.annotations.Expose;

import SharedObject.Balloon;

public class BalloonFilled extends Message 
{
	@Expose public Balloon Balloon;
}

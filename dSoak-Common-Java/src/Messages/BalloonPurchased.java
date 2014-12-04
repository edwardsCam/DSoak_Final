package Messages;

import com.google.gson.annotations.Expose;
import SharedObject.*;

public class BalloonPurchased extends Message 
{
	@Expose public Balloon Balloon;
}

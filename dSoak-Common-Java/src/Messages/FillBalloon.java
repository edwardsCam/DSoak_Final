package Messages;

import com.google.gson.annotations.Expose;
import SharedObject.*;

public class FillBalloon extends ResourceRequest 
{
	@Expose public Balloon Balloon;
}

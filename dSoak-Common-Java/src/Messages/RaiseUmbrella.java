package Messages;

import com.google.gson.annotations.Expose;
import SharedObject.*;

public class RaiseUmbrella extends Message 
{
	@Expose public Umbrella Umbrella;
}

package Messages;

import com.google.gson.annotations.Expose;
import SharedObject.*;

public class UmbrellaPurchased extends Message 
{
	@Expose public Umbrella Umbrella;
}

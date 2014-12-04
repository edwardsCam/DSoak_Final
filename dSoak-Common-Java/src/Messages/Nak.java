package Messages;

import com.google.gson.annotations.Expose;

public class Nak extends Message 
{
	@Expose public String Error;
}

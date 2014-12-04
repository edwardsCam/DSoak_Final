package Messages;

import java.util.List;

import com.google.gson.annotations.Expose;

import SharedObject.Penny;

public class ResourceRequest extends Message 
{
	@Expose public List<Penny> Pennies;
}

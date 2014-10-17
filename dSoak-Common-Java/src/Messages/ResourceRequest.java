package Messages;

import java.io.Serializable;
import java.util.List;

import SharedObject.Penny;

public class ResourceRequest extends Message implements Serializable
{
	private static final long serialVersionUID = 5272578343088726328L;
	
	public List<Penny> Pennies;
}

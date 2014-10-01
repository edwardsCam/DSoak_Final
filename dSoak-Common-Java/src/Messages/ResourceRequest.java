package Messages;

import java.io.Serializable;
import java.util.List;

import SharedObject.Penny;

public class ResourceRequest extends Message implements Serializable
{
	private static final long SerialVersionUID = -1455333437770468800L;
	public List<Penny> Pennies;
}

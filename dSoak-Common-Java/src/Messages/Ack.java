package Messages;

import java.io.Serializable;

public class Ack extends Message implements Serializable
{
	private static final long SerialVersionUID = -145533343743320500L; 
	public int x;
	public String message;
}

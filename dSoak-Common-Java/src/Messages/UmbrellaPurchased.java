package Messages;

import java.io.Serializable;

import SharedObject.*;

public class UmbrellaPurchased extends Message implements Serializable
{
	private static final long serialVersionUID = -1092915128222526056L;

	public Umbrella Umbrella;
	
	public UmbrellaPurchased(Umbrella ump) {
		this.Umbrella = ump;
	}
}

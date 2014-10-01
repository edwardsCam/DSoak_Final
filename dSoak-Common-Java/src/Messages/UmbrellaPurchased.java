package Messages;

import java.io.Serializable;

import SharedObject.*;

public class UmbrellaPurchased extends Message implements Serializable
{
	private static final long SerialVersionUID = -1455333467430468800L;
	public Umbrella Umbrella;
	
	public UmbrellaPurchased(Umbrella ump) {
		this.Umbrella = ump;
	}
}

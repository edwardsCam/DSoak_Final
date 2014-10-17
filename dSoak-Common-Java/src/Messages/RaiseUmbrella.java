package Messages;
import java.io.Serializable;

import SharedObject.*;

public class RaiseUmbrella extends Message implements Serializable
{
	private static final long serialVersionUID = 5965853462602457661L;
	
	public Umbrella Umbrella;
}

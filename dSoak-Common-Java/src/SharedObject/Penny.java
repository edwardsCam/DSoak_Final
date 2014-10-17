package SharedObject;

import java.io.IOException;
import java.io.Serializable;
import java.security.NoSuchAlgorithmException;

public class Penny extends SharedResource implements Serializable
{
	private static final long serialVersionUID = 8616746732902575340L;
	
	public Penny() throws NoSuchAlgorithmException, IOException 
	{
		super();
	}

}

package SharedObject;

import java.io.IOException;
import java.io.Serializable;
import java.security.NoSuchAlgorithmException;

public class Umbrella extends SharedResource implements Serializable 
{
	private static final long serialVersionUID = 1L;
	 
	public Umbrella() throws NoSuchAlgorithmException, IOException{
		super();
	}
}

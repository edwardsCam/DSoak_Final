package SharedObject;

import java.io.IOException;
import java.io.Serializable;
import java.security.NoSuchAlgorithmException;

public class Umbrella extends SharedResource implements Serializable 
{
	private static final long SerialVersionUID = -1455223439430461110L; 
	int x;
	
	public Umbrella(int x) throws NoSuchAlgorithmException, IOException{
		super();
		this.x = x;
	}
	
	public int getX() {
		return x;
	}
}

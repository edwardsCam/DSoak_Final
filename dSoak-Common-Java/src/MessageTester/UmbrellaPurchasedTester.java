package MessageTester;

import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.security.NoSuchAlgorithmException;

import org.junit.Test;

import Messages.Message;
import Messages.UmbrellaPurchased;
import SharedObject.Umbrella;

public class UmbrellaPurchasedTester 
{
	@Test
	public void test_EveryThings() throws ClassNotFoundException, NoSuchAlgorithmException, IOException
	{
		Umbrella umbrella = new Umbrella();
		UmbrellaPurchased msg1 = new UmbrellaPurchased(umbrella);
	
		byte[] bytes = msg1.Encode();
		InputStream myInputStream = new ByteArrayInputStream(bytes);
		ObjectInputStream oin = new ObjectInputStream(myInputStream);
		String type = (String) oin.readObject();
		
		
		UmbrellaPurchased msg2 = (UmbrellaPurchased) Message.Decode(bytes);
		assertNotNull(msg1);
		assertNotNull(msg2);
		
	}
}

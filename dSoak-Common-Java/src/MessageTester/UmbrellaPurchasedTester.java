package MessageTester;

import static org.junit.Assert.*;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;

import org.junit.Test;

import Messages.Ack;
import Messages.Message;
import Messages.UmbrellaPurchased;
import SharedObject.Umbrella;

public class UmbrellaPurchasedTester {

	@Test
	public void test() throws ClassNotFoundException, NoSuchAlgorithmException, IOException {
		Umbrella umbrella = new Umbrella(10);
		UmbrellaPurchased msg1 = new UmbrellaPurchased(umbrella);
	
		byte[] bytes = msg1.Encode();
		
		UmbrellaPurchased msg2 = (UmbrellaPurchased) Message.Decode(bytes);
		assertEquals(msg1.Umbrella.getX(), msg2.Umbrella.getX());
		assertNotNull(msg1);
	}

}
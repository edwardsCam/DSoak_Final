package MessageTester;

import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;

import org.junit.Test;

import Messages.LeaveGame;
import Messages.Message;

public class LeaveGameTester {

	@Test
	public void test_EveryThings() throws ClassNotFoundException, IOException 
	{
		LeaveGame msg1 = new LeaveGame();
		msg1.GameId = 10;
		
		byte[] bytes = msg1.Encode();
		InputStream myInputStream = new ByteArrayInputStream(bytes);
		ObjectInputStream oin = new ObjectInputStream(myInputStream);
		String type = (String) oin.readObject();
		
		LeaveGame msg2 = (LeaveGame) Message.Decode(bytes);
		
		assertEquals(msg1.GameId, msg2.GameId);
	}
}

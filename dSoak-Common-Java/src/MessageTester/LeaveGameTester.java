package MessageTester;

import static org.junit.Assert.*;

import org.junit.Test;

import Messages.LeaveGame;
import Messages.Message;

public class LeaveGameTester {

	@Test
	public void test_EveryThings() throws ClassNotFoundException 
	{
		LeaveGame msg1 = new LeaveGame();
		msg1.GameId = 10;
		byte[] bytes = msg1.Encode();
		
		LeaveGame msg2 = (LeaveGame) Message.Decode(bytes);
		
		assertEquals(msg1.GameId, msg2.GameId);
	}
}

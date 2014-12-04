package MessageTester;

import static org.junit.Assert.*;

import java.io.IOException;

import org.junit.Test;

import Messages.LeaveGame;
import Messages.Message;
import SharedObject.MessageNumber;

public class LeaveGameTester {

	@Test
	public void test_EveryThings() throws ClassNotFoundException, IOException 
	{
		MessageNumber.LocalProcessId = 100;
		
		LeaveGame msg1 = new LeaveGame();
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
		
		LeaveGame msg2 = new LeaveGame();
		msg2.GameId = 123;
		assertNotNull(msg2.MessageNr);
		assertTrue(msg2.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
		assertEquals(msg1.MessageNr.ProcessId, msg2.MessageNr.ProcessId);
		assertEquals(msg1.ConvId.ProcessId, msg2.ConvId.ProcessId);
		assertEquals(msg1.ConvId.SeqNumber + 1, msg2.ConvId.SeqNumber);
		
		byte[] bytes = msg2.Encode();
		String str = new String(bytes);
		
		LeaveGame msg3 = (LeaveGame) Message.Decode(bytes);
		assertNotNull(msg3);
		assertTrue(msg3 instanceof LeaveGame);
		assertEquals(msg2.GameId, msg3.GameId);
		assertEquals(msg2.MessageNr.ProcessId, msg3.MessageNr.ProcessId);
		assertEquals(msg2.MessageNr.SeqNumber, msg3.MessageNr.SeqNumber);
		
		LeaveGame msg4 = (LeaveGame) msg3;
		assertTrue(msg4 instanceof LeaveGame);
		assertEquals(msg4.GameId, msg3.GameId);
		assertEquals(msg3.MessageNr, msg4.MessageNr);
		assertEquals(msg3.ConvId, msg4.ConvId);
	}
}

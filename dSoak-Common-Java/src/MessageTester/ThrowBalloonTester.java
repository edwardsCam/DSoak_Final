package MessageTester;

import static org.junit.Assert.*;
import java.io.IOException;
import java.security.NoSuchAlgorithmException;
import org.junit.Test;
import Messages.Message;
import Messages.ThrowBalloon;
import SharedObject.Balloon;
import SharedObject.MessageNumber;

public class ThrowBalloonTester {

	@Test
	public void test_EveryThings() throws NoSuchAlgorithmException, IOException, ClassNotFoundException 
	{
		MessageNumber.LocalProcessId = 100;
		
		ThrowBalloon msg1 = new ThrowBalloon();
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
		
		Balloon b = new Balloon();
		b.Id = 245;
		ThrowBalloon msg2 = new ThrowBalloon();
		msg2.GameId  =123;
		msg2.Balloon = b;
		msg2.TargetPlayerId = 352;
		
		assertNotNull(msg2.MessageNr);
		assertNotNull(msg2.Balloon);
		assertEquals(100, msg2.MessageNr.ProcessId);
		assertTrue(msg2.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
		assertEquals(msg1.MessageNr.ProcessId, msg2.MessageNr.ProcessId);
		assertEquals(msg1.ConvId.ProcessId, msg2.ConvId.ProcessId);
		assertEquals(msg1.ConvId.SeqNumber + 1, msg2.ConvId.SeqNumber);
		assertEquals(123, msg2.GameId);
		assertEquals(352, msg2.TargetPlayerId);
		
		byte[] bytes = msg2.Encode();
		String str = new String(bytes);
		
		Message msg3 = Message.Decode(bytes);
		assertNotNull(msg3);
		assertEquals(msg2.MessageNr.ProcessId, msg3.MessageNr.ProcessId);
		assertEquals(msg2.MessageNr.SeqNumber, msg3.MessageNr.SeqNumber);
	
		
		ThrowBalloon msg4 = (ThrowBalloon) msg3;
		assertTrue(msg4 instanceof ThrowBalloon);
		assertEquals(msg3.MessageNr, msg4.MessageNr);
		assertEquals(msg3.ConvId, msg4.ConvId);
		
		assertEquals(msg2.MessageNr.ProcessId, msg4.MessageNr.ProcessId);
		assertEquals(msg2.MessageNr.SeqNumber, msg4.MessageNr.SeqNumber);
		assertEquals(msg2.ConvId.ProcessId, msg4.ConvId.ProcessId);
		assertEquals(msg2.ConvId.SeqNumber, msg4.ConvId.SeqNumber);
		assertEquals(msg2.TargetPlayerId, msg4.TargetPlayerId);
		assertEquals(msg2.Balloon.Id, msg4.Balloon.Id);
		assertNotNull(msg4.Balloon);
		
	}

}

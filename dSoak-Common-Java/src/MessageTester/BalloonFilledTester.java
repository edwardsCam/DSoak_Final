package MessageTester;

import static org.junit.Assert.*;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;

import org.junit.Test;

import Messages.BalloonFilled;
import Messages.Message;
import SharedObject.Balloon;
import SharedObject.MessageNumber;

public class BalloonFilledTester {

	@Test
	public void test_EveryThing() throws NoSuchAlgorithmException, IOException, ClassNotFoundException
	{
		MessageNumber.LocalProcessId = 100;
		
		BalloonFilled msg1 = new BalloonFilled();
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
		
		Balloon b = new Balloon();
		
		BalloonFilled msg2 = new BalloonFilled();
		msg2.ConvId = msg1.ConvId;
		msg2.Balloon = b;
		
		assertNotNull(msg2.MessageNr);
		assertEquals(100, msg2.MessageNr.ProcessId);
		assertEquals(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
		assertEquals(msg1.ConvId, msg2.ConvId);
		assertNotNull(msg2.Balloon);
		assertSame(b, msg2.Balloon);
		
		byte[] bytes = msg2.Encode();
		
		Message msg3 = Message.Decode(bytes);
		
		assertTrue(msg3 instanceof BalloonFilled);
		
		BalloonFilled msg4 = (BalloonFilled) msg3;
		
		assertEquals(msg4.MessageNr, msg3.MessageNr);
		assertEquals(msg2.ConvId.ProcessId, msg4.ConvId.ProcessId);
		assertEquals(msg2.ConvId.SeqNumber, msg4.ConvId.SeqNumber);
		assertEquals(msg2.Balloon.Id, msg4.Balloon.Id);
	}
}
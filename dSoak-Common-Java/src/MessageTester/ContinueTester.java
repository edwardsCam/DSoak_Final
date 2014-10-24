package MessageTester;

import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;

import org.junit.Test;

import Messages.BuyUmbrella;
import Messages.Continue;
import Messages.Message;
import SharedObject.MessageNumber;

public class ContinueTester
{
	@Test
	public void test_EveryThing() throws IOException, ClassNotFoundException 
	{
		MessageNumber.LocalProcessId = 100;
		
		Continue msg1 = new Continue();
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
		
		Continue msg2 = new Continue();
		msg2.MissingReplyDeqNr = 3;
		
		assertNotNull(msg2.MessageNr);
		assertEquals(100, msg2.MessageNr.ProcessId);
		assertEquals(msg1.MessageNr.SeqNumber + 1 , msg2.MessageNr.SeqNumber);
		assertEquals(msg2.MessageNr, msg2.ConvId);
		assertEquals(3, msg2.MissingReplyDeqNr);
		
		byte[] bytes = msg2.Encode();
		
		InputStream myInputStream = new ByteArrayInputStream(bytes);
		ObjectInputStream oin = new ObjectInputStream(myInputStream);
		String type = (String) oin.readObject();
		
		assertTrue(type.equals("Continue:"));
		
		Message msg3 = Message.Decode(bytes);
		assertTrue(msg3 instanceof Continue);
		Continue msg4 = (Continue) msg3;
		
		assertEquals(msg2.MessageNr.ProcessId, msg4.MessageNr.ProcessId);
		assertEquals(msg2.MessageNr.SeqNumber, msg4.MessageNr.SeqNumber);
		assertEquals(msg2.ConvId.ProcessId, msg4.ConvId.ProcessId);
		assertEquals(msg2.ConvId.SeqNumber, msg4.ConvId.SeqNumber);
		assertEquals(msg2.MissingReplyDeqNr, msg4.MissingReplyDeqNr);
	}
}
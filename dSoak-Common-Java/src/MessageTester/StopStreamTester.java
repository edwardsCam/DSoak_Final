package MessageTester;

import static org.junit.Assert.*;

import java.io.IOException;

import org.junit.Test;

import Messages.Message;
import Messages.StopStream;
import SharedObject.MessageNumber;

public class StopStreamTester
{
	@Test
	public void test_EveryThing() throws ClassNotFoundException, IOException
	{
		MessageNumber.LocalProcessId = 100;
		
		StopStream msg1 = new StopStream();
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
		
		StopStream msg2 = new StopStream();
		msg2.ConvId = msg1.ConvId;
		assertNotNull(msg2.MessageNr);
		assertEquals(100, msg2.MessageNr.ProcessId);
		assertTrue(msg2.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
		assertEquals(msg1.MessageNr.ProcessId, msg2.MessageNr.ProcessId);
		assertEquals(msg1.ConvId.ProcessId, msg2.ConvId.ProcessId);
		assertEquals(msg1.ConvId.SeqNumber, msg2.ConvId.SeqNumber);
		
		byte[] bytes = msg2.Encode();
		String str = new String(bytes);
		
		Message msg3 = Message.Decode(bytes);
		assertNotNull(msg3);
		assertEquals(msg2.MessageNr.ProcessId, msg3.MessageNr.ProcessId);
		assertEquals(msg2.MessageNr.SeqNumber, msg3.MessageNr.SeqNumber);
	
		
		StopStream msg4 = (StopStream) msg3;
		assertTrue(msg4 instanceof StopStream);
		assertEquals(msg3.MessageNr, msg4.MessageNr);
		assertEquals(msg3.ConvId, msg4.ConvId);
		
		assertEquals(msg2.MessageNr.ProcessId, msg4.MessageNr.ProcessId);
		assertEquals(msg2.MessageNr.SeqNumber, msg4.MessageNr.SeqNumber);
		assertEquals(msg2.ConvId.ProcessId, msg4.ConvId.ProcessId);
		assertEquals(msg2.ConvId.SeqNumber, msg4.ConvId.SeqNumber);
	}

}

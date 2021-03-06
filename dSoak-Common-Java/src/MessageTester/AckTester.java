package MessageTester;

import java.io.IOException;

import org.junit.Test;

import Messages.Ack;
import Messages.Message;
import SharedObject.MessageNumber;
import static org.junit.Assert.*;

public class AckTester 
{
	@Test
	public void test_EveryThings() throws IOException, ClassNotFoundException 
	{
		MessageNumber.LocalProcessId = 100;
		
		Ack ack = new Ack();
		assertNotNull(ack);
		assertEquals(100, ack.MessageNr.ProcessId);
		assertTrue(ack.MessageNr.SeqNumber > 0);
		assertEquals(ack.MessageNr, ack.ConvId);
		
		Ack ack2 = new Ack();
		ack2.ConvId = ack.ConvId;
		assertNotNull(ack2.MessageNr);
		assertEquals(100, ack2.MessageNr.ProcessId);
		assertEquals(ack.MessageNr.SeqNumber+1, ack2.MessageNr.SeqNumber);
		assertEquals(ack.ConvId, ack2.ConvId);
		
		byte[] bytes = ack2.Encode();
		String str = new String(bytes);
		
		Message ack3 = (Ack) Message.Decode(str.getBytes());
		assertTrue(ack3 instanceof Ack);
		assertNotNull(ack3);
		Ack ack4 = (Ack) ack3;
		assertEquals(ack3.MessageNr, ack4.MessageNr);
		assertEquals(ack3.ConvId, ack4.ConvId);
	}
}
package MessageTester;

import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.security.NoSuchAlgorithmException;
import org.junit.Test;
import Messages.Message;
import Messages.RaiseUmbrella;
import SharedObject.MessageNumber;
import SharedObject.Umbrella;

public class RaiseUmbrellaTester 
{
	@Test
	public void test_EveryThing() throws ClassNotFoundException, IOException, NoSuchAlgorithmException 
	{
		MessageNumber.LocalProcessId = 100;
		
		RaiseUmbrella msg1 = new RaiseUmbrella();
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
		
		Umbrella u = new Umbrella();
		u.Id = 10;
		RaiseUmbrella msg2 = new RaiseUmbrella();
		msg2.Umbrella = u;
		assertNotNull(msg2.MessageNr);
		assertTrue(msg2.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
		assertEquals(msg1.MessageNr.ProcessId, msg2.MessageNr.ProcessId);
		assertEquals(msg1.ConvId.ProcessId, msg2.ConvId.ProcessId);
		assertEquals(msg1.ConvId.SeqNumber + 1, msg2.ConvId.SeqNumber);
		assertEquals(msg2.Umbrella.Id, u.Id);
		
		byte[] bytes = msg2.Encode();
		InputStream myInputStream = new ByteArrayInputStream(bytes);
		ObjectInputStream oin = new ObjectInputStream(myInputStream);
		String type = (String) oin.readObject();
		assertTrue(type.equals("RaiseUmbrella:"));
		
		Message msg3 = Message.Decode(bytes);
		assertNotNull(msg3);
		assertEquals(msg2.MessageNr.ProcessId, msg3.MessageNr.ProcessId);
		assertEquals(msg2.MessageNr.SeqNumber, msg3.MessageNr.SeqNumber);
	
		RaiseUmbrella msg4 = (RaiseUmbrella) msg3;
		assertTrue(msg4 instanceof RaiseUmbrella);
		assertEquals(msg3.MessageNr, msg4.MessageNr);
		assertEquals(msg3.ConvId, msg4.ConvId);
		
		assertEquals(msg2.MessageNr.ProcessId, msg4.MessageNr.ProcessId);
		assertEquals(msg2.MessageNr.SeqNumber, msg4.MessageNr.SeqNumber);
		assertEquals(msg2.ConvId.ProcessId, msg4.ConvId.ProcessId);
		assertEquals(msg2.ConvId.SeqNumber, msg4.ConvId.SeqNumber);
		assertEquals(msg2.Umbrella.Id, msg4.Umbrella.Id);
	}

}

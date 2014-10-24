package MessageTester;

import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.util.ArrayList;

import org.junit.Test;

import Messages.FillBalloon;
import Messages.Hit;
import Messages.Message;
import SharedObject.MessageNumber;
import SharedObject.Penny;

public class HitTester 
{
	@Test
	public void test_EveryThing() throws ClassNotFoundException, IOException 
	{
		MessageNumber.LocalProcessId = 100;
		
		Hit msg1 = new Hit();
		
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
	
		Hit msg2 = new Hit();
		msg2.UnitsOfWater = 3;
		
		assertNotNull(msg2.MessageNr);
		assertEquals(100, msg2.MessageNr.ProcessId);
		assertEquals(msg1.MessageNr.SeqNumber + 1 , msg2.MessageNr.SeqNumber);
		assertEquals(msg2.MessageNr, msg2.ConvId);
		assertEquals(3, msg2.UnitsOfWater);
		
		byte[] bytes = msg2.Encode();
		
		InputStream myInputStream = new ByteArrayInputStream(bytes);
		ObjectInputStream oin = new ObjectInputStream(myInputStream);
		String type = (String) oin.readObject();
		
		assertTrue(type.equals("Hit:"));
		
		Message msg3 = Message.Decode(bytes);
		assertTrue(msg3 instanceof Hit);
		Hit msg4 = (Hit) msg3;
		
		assertEquals(msg2.MessageNr.ProcessId, msg4.MessageNr.ProcessId);
		assertEquals(msg2.MessageNr.SeqNumber, msg4.MessageNr.SeqNumber);
		assertEquals(msg2.ConvId.ProcessId, msg3.ConvId.ProcessId);
		assertEquals(msg2.ConvId.SeqNumber, msg3.ConvId.SeqNumber);
		assertEquals(msg2.UnitsOfWater, msg4.UnitsOfWater);
	}

}

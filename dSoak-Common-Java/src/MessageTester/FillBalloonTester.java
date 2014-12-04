package MessageTester;

import static org.junit.Assert.*;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;

import org.junit.Test;

import Messages.FillBalloon;
import Messages.Message;
import SharedObject.MessageNumber;
import SharedObject.Penny;

public class FillBalloonTester 
{
	@Test
	public void test_EveryThing() throws NoSuchAlgorithmException, IOException, ClassNotFoundException 
	{
		MessageNumber.LocalProcessId = 100;
		
		FillBalloon msg1 = new FillBalloon();
		
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
	
		ArrayList<Penny> pennies = new  ArrayList<Penny>();
		pennies.add(new Penny());
		pennies.add(new Penny());
		pennies.add(new Penny());
		
		FillBalloon msg2 = new FillBalloon();
		msg2.Pennies = pennies;
		
		assertNotNull(msg2.MessageNr);
		assertEquals(100, msg2.MessageNr.ProcessId);
		assertEquals(msg1.MessageNr.SeqNumber + 1 , msg2.MessageNr.SeqNumber);
		assertEquals(msg2.MessageNr, msg2.ConvId);
		assertNotNull(msg2.Pennies);
		assertSame(pennies, msg2.Pennies);
		
		byte[] bytes = msg2.Encode();
		String str = new String(bytes);
		
		
		Message msg3 = Message.Decode(bytes);
		assertTrue(msg3 instanceof FillBalloon);
		FillBalloon msg4 = (FillBalloon) msg3;
		
		assertEquals(msg3.MessageNr, msg4.MessageNr);
		assertEquals(msg3.ConvId, msg3.ConvId);
		assertEquals(msg2.Pennies.size(), msg4.Pennies.size());
	}

}

package MessageTester;

import static org.junit.Assert.*;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;

import org.junit.Test;

import Messages.BuyBalloon;
import Messages.Message;
import SharedObject.MessageNumber;
import SharedObject.Penny;

public class BuyBalloonTester {

	@Test
	public void test_EveryThing() throws NoSuchAlgorithmException, IOException, ClassNotFoundException
	{
		MessageNumber.LocalProcessId = 100;
		
		BuyBalloon msg1 = new BuyBalloon();
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
		
		ArrayList<Penny> pennies = new  ArrayList<Penny>();
		pennies.add(new Penny());
		pennies.add(new Penny());
		pennies.add(new Penny());
		
		BuyBalloon msg2 = new BuyBalloon();
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
		assertTrue(msg3 instanceof BuyBalloon);
		BuyBalloon msg4 = (BuyBalloon) msg3;
		
		assertEquals(msg3.MessageNr, msg4.MessageNr);
		assertEquals(msg3.ConvId, msg3.ConvId);
		assertEquals(msg2.Pennies.size(), msg4.Pennies.size());
	}
}
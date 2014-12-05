package MessageTester;

import static org.junit.Assert.*;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;

import org.junit.Test;

import Messages.BalloonStolen;
import Messages.Message;
import SharedObject.Balloon;
import SharedObject.MessageNumber;

public class BalloonStolenTester
{
	@Test
	public void test_Everything() throws NoSuchAlgorithmException, IOException, ClassNotFoundException 
	{
		MessageNumber.LocalProcessId = 100;
		
		BalloonStolen msg1 = new BalloonStolen();
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
		assertEquals(0, msg1.GameId);
		assertEquals(0, msg1.TargetProcessId);
		assertEquals(0, msg1.ThiefId);
		assertNull(msg1.StolenBalloon);
		
		Balloon b = new Balloon();
        BalloonStolen msg2 = new BalloonStolen();
        msg2.GameId = 11;
        msg2.TargetProcessId = 12;
        msg2.ThiefId = 13;
        msg2.StolenBalloon = b;
        
        assertNotNull(msg2.MessageNr);
        assertEquals(100, msg2.MessageNr.ProcessId);
        assertEquals(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
        assertEquals(msg2.MessageNr, msg2.ConvId);
        assertEquals(11, msg2.GameId);
        assertEquals(12, msg2.TargetProcessId);
        assertEquals(13, msg2.ThiefId);
        assertSame(b, msg2.StolenBalloon);

        byte[] bytes = msg2.Encode();
        String str = new String(bytes);
        
	    Message msg3 = Message.Decode(bytes);
        assertTrue(msg3 instanceof BalloonStolen);
        BalloonStolen msg4 = (BalloonStolen) msg3;
        assertEquals(msg2.MessageNr.ProcessId, msg4.MessageNr.ProcessId);
        assertEquals(msg2.MessageNr.SeqNumber, msg4.MessageNr.SeqNumber);
        assertEquals(msg2.ConvId.ProcessId, msg4.ConvId.ProcessId);
        assertEquals(msg2.ConvId.SeqNumber, msg4.ConvId.SeqNumber);
        assertEquals(msg2.GameId, msg4.GameId);
        assertEquals(msg2.TargetProcessId, msg4.TargetProcessId);
        assertEquals(msg2.ThiefId, msg4.ThiefId);
        assertEquals(b.Id, msg4.StolenBalloon.Id);
	}	

}

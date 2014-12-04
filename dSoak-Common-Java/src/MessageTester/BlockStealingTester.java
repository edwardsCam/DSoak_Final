package MessageTester;

import static org.junit.Assert.*;

import java.io.IOException;

import org.junit.Test;

import Messages.BlockStealing;
import Messages.Message;
import SharedObject.MessageNumber;

public class BlockStealingTester 
{
	@Test
	public void test_Everything() throws IOException, ClassNotFoundException
	{
		 MessageNumber.LocalProcessId = 100;
		 
		 BlockStealing msg1 = new BlockStealing();
		 assertNotNull(msg1.MessageNr);
         assertEquals(100, msg1.MessageNr.ProcessId);
         assertTrue(msg1.MessageNr.SeqNumber > 0);
         assertEquals(msg1.MessageNr, msg1.ConvId);
         assertEquals(0, msg1.GameId);
         assertEquals(0, msg1.TargetProcessId);
         assertEquals(0, msg1.ThiefId);

         BlockStealing msg2 = new BlockStealing();
         msg2.GameId = 11;
         msg2.TargetProcessId = 12;
         msg2.ThiefId = 13;
         
         assertNotNull(msg2.MessageNr);
         assertEquals(100, msg2.MessageNr.ProcessId);
         assertEquals(msg1.MessageNr.SeqNumber + 1, msg2.MessageNr.SeqNumber);
         assertEquals(msg2.MessageNr, msg2.ConvId);
         assertEquals(11, msg2.GameId);
         assertEquals(12, msg2.TargetProcessId);
         assertEquals(13, msg2.ThiefId);

         byte[] bytes = msg2.Encode();
         String str = new String(bytes);
         
 		 Message msg3 = Message.Decode(bytes);
 		 assertTrue(msg3 instanceof BlockStealing);
         
 		 BlockStealing msg4 = (BlockStealing) msg3;
         
 		 assertEquals(msg2.MessageNr.ProcessId, msg4.MessageNr.ProcessId);
 		 assertEquals(msg2.MessageNr.SeqNumber, msg4.MessageNr.SeqNumber);
 		 assertEquals(msg2.ConvId.ProcessId, msg4.ConvId.ProcessId);
 		assertEquals(msg2.ConvId.SeqNumber, msg4.ConvId.SeqNumber);
 		 assertEquals(msg2.GameId, msg4.GameId);
 		 assertEquals(msg2.TargetProcessId, msg4.TargetProcessId);
 		 assertEquals(msg2.ThiefId, msg4.ThiefId);
 		 
	}

}

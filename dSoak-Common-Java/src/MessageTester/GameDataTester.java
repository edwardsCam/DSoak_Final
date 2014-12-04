package MessageTester;

import static org.junit.Assert.*;
import java.io.IOException;
import java.util.ArrayList;
import org.junit.Test;
import Messages.GameData;
import Messages.Message;
import SharedObject.GameInfo;
import SharedObject.MessageNumber;
import SharedObject.ProcessData;
import SharedObject.ProcessData.PossibleProcessType;

public class GameDataTester
{
	@Test
	public void test_EveryThing() throws ClassNotFoundException, IOException
	{
		MessageNumber.LocalProcessId = 100;
		
		GameData msg1 = new GameData();
		
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
	
		GameInfo gameInfo = new GameInfo();
		gameInfo.GameId = 102;
		gameInfo.Status = GameInfo.StatusCode.NOTINITIALIZED;
		
		ArrayList<ProcessData> processes = new  ArrayList<ProcessData>();
		ProcessData process1 = new ProcessData();
		process1.GameId = 102;
		process1.ProcessId = 10;
		process1.ProcessType = PossibleProcessType.Player;
		
		ProcessData process2 = new ProcessData();
		process2.GameId = 102;
		process2.ProcessId = 20;
		process2.ProcessType = PossibleProcessType.Player;
		
		ProcessData process3 = new ProcessData();
		process3.GameId = 102;
		process3.ProcessId = 30;
		process3.ProcessType = PossibleProcessType.Thief;
		
		
		processes.add(process1);
		processes.add(process2);
		processes.add(process3);
		
		GameData msg2 = new GameData();
		msg2.Info = gameInfo;
		msg2.Processes  = processes;
			
		assertNotNull(msg2.MessageNr);
		assertEquals(100, msg2.MessageNr.ProcessId);
		assertEquals(msg1.MessageNr.SeqNumber + 1 , msg2.MessageNr.SeqNumber);
		assertEquals(msg2.MessageNr, msg2.ConvId);
		assertNotNull(msg2.Processes);
		assertSame(processes, msg2.Processes);
		
		byte[] bytes = msg2.Encode();
		String str = new String(bytes);
		
		Message msg3 = Message.Decode(bytes);
		assertTrue(msg3 instanceof GameData);
		GameData msg4 = (GameData) msg3;
		
		assertEquals(msg2.MessageNr.ProcessId, msg4.MessageNr.ProcessId);
		assertEquals(msg2.MessageNr.SeqNumber, msg4.MessageNr.SeqNumber);
		assertEquals(msg2.ConvId.ProcessId, msg4.ConvId.ProcessId);
		assertEquals(msg2.ConvId.SeqNumber, msg4.ConvId.SeqNumber);
		assertNotNull(msg4.Info);
		assertEquals(msg2.Info.GameId, msg4.Info.GameId);
		assertEquals(msg2.Info.Status.getValue(), msg4.Info.Status.getValue());
		assertNotNull(msg4.Processes);
		assertEquals(msg2.Processes.size(), msg4.Processes.size());
	}

}

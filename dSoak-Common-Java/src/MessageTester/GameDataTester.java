package MessageTester;

import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.util.ArrayList;

import org.junit.Test;

import Messages.FillBalloon;
import Messages.GameData;
import Messages.Message;
import SharedObject.GameInfo;
import SharedObject.GameInfo.StatusCode;
import SharedObject.MessageNumber;
import SharedObject.Penny;
import SharedObject.PlayerInfo;

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
		gameInfo.Status = GameInfo.StatusCode.AVAILABLE;
		
		ArrayList<PlayerInfo> players = new  ArrayList<PlayerInfo>();
		PlayerInfo plyerInfo1 = new PlayerInfo();
		plyerInfo1.PlayerId = 10 ;
		
		PlayerInfo plyerInfo2 = new PlayerInfo();
		plyerInfo2.PlayerId = 20 ;
		
		PlayerInfo plyerInfo3 = new PlayerInfo();
		plyerInfo3.PlayerId = 30 ;
		
		players.add(plyerInfo1);
		players.add(plyerInfo2);
		players.add(plyerInfo3);
		
		GameData msg2 = new GameData();
		msg2.Info = gameInfo;
		msg2.Players = players;
			
		assertNotNull(msg2.MessageNr);
		assertEquals(100, msg2.MessageNr.ProcessId);
		assertEquals(msg1.MessageNr.SeqNumber + 1 , msg2.MessageNr.SeqNumber);
		assertEquals(msg2.MessageNr, msg2.ConvId);
		assertNotNull(msg2.Players);
		assertSame(players, msg2.Players);
		
		byte[] bytes = msg2.Encode();
		
		InputStream myInputStream = new ByteArrayInputStream(bytes);
		ObjectInputStream oin = new ObjectInputStream(myInputStream);
		String type = (String) oin.readObject();
		
		assertTrue(type.equals("GameData:"));
		
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
		assertNotNull(msg4.Players);
		assertEquals(msg2.Players.size(), msg4.Players.size());
	}

}

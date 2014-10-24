package MessageTester;

import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;

import org.junit.Test;

import Messages.JoinGame;
import Messages.Message;
import SharedObject.MessageNumber;
import SharedObject.PlayerInfo;
import SharedObject.PublicEndPoint;

public class JoinGameTester {

	@Test
	public void test_Everythings() throws ClassNotFoundException, IOException 
	{
		MessageNumber.LocalProcessId = 100;
		
		JoinGame msg1 = new JoinGame();
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
		
		PublicEndPoint ep = new PublicEndPoint();
		ep.Host = "127.0.0.1";
		ep.Port = 123456;
		PlayerInfo playerInfo = new PlayerInfo();
		playerInfo.PlayerId = 10;
		playerInfo.Status = PlayerInfo.StateCode.ONLINE;
		playerInfo.EndPoint = ep;
		
		JoinGame msg2 = new JoinGame();
		msg2.GameId = 123;
		msg2.Player = playerInfo;
		assertNotNull(msg2);
		assertNotNull(msg2.MessageNr);
		assertEquals(100, msg2.MessageNr.ProcessId);
		assertEquals(msg1.MessageNr.SeqNumber + 1 , msg2.MessageNr.SeqNumber);
		assertEquals(msg2.MessageNr, msg2.ConvId);
		assertEquals(123, msg2.GameId);
		assertNotNull(msg2.Player);
		assertSame(playerInfo, msg2.Player);
		assertTrue(msg2.Player.EndPoint.Host.equals( "127.0.0.1"));
		assertEquals(123456, msg2.Player.EndPoint.Port);
		
		byte[] bytes = msg2.Encode();
		InputStream myInputStream = new ByteArrayInputStream(bytes);
		ObjectInputStream oin = new ObjectInputStream(myInputStream);
		String type = (String) oin.readObject();
		
		assertTrue(type.equals("JoinGame:"));
		JoinGame msg3 = (JoinGame) Message.Decode(bytes);
		assertNotNull(msg3);
		assertTrue(msg3 instanceof JoinGame);
		assertEquals(msg2.GameId, msg3.GameId);
		assertTrue(msg2.Player.EndPoint.Host.equals(msg3.Player.EndPoint.Host));
		assertEquals(msg2.Player.EndPoint.Port, msg3.Player.EndPoint.Port);
		assertEquals(msg2.Player.Status.getValue(), msg3.Player.Status.getValue());
		assertEquals(msg2.Player.PlayerId, msg3.Player.PlayerId);
		
		JoinGame msg4 = (JoinGame) msg3;
		assertEquals(msg3.GameId, msg4.GameId);
		assertTrue(msg3.Player.EndPoint.Host.equals(msg4.Player.EndPoint.Host));
		assertEquals(msg3.Player.EndPoint.Port, msg4.Player.EndPoint.Port);
		assertEquals(msg3.Player.Status.getValue(), msg4.Player.Status.getValue());
		assertEquals(msg3.Player.PlayerId, msg4.Player.PlayerId);
		
		assertEquals(msg2.GameId, msg4.GameId);
		assertTrue(msg2.Player.EndPoint.Host.equals(msg4.Player.EndPoint.Host));
		assertEquals(msg2.Player.EndPoint.Port, msg4.Player.EndPoint.Port);
		assertEquals(msg2.Player.Status.getValue(), msg4.Player.Status.getValue());
		assertEquals(msg2.Player.PlayerId, msg4.Player.PlayerId);
	}
}
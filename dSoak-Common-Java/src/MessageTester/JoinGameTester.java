package MessageTester;

import static org.junit.Assert.*;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

import org.junit.Test;

import Messages.JoinGame;
import Messages.Message;
import SharedObject.MessageNumber;
import SharedObject.PlayerInfo;
import SharedObject.PublicEndPoint;

public class JoinGameTester
{
	@Test
	public void test_Everythings() throws ClassNotFoundException, IOException 
	{
		MessageNumber.LocalProcessId = 100;
		
		JoinGame msg1 = new JoinGame();
		assertNotNull(msg1.MessageNr);
		assertEquals(100, msg1.MessageNr.ProcessId);
		assertTrue(msg1.MessageNr.SeqNumber > 0);
		assertEquals(msg1.MessageNr, msg1.ConvId);
		
		PublicEndPoint ep = new PublicEndPoint("127.0.0.1:123456");
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
		assertTrue(msg2.Player.EndPoint.getHost().equals( "127.0.0.1"));
		assertEquals(123456, msg2.Player.EndPoint.getPort());
		
		byte[] bytes = msg2.Encode();
		String str = new String(bytes);
		
		JoinGame msg3 = (JoinGame) Message.Decode(bytes);
		assertNotNull(msg3);
		assertTrue(msg3 instanceof JoinGame);
		assertEquals(msg2.GameId, msg3.GameId);
		assertTrue(msg2.Player.EndPoint.HostAndPort.equals(msg3.Player.EndPoint.HostAndPort));
		assertEquals(msg2.Player.Status.getValue(), msg3.Player.Status.getValue());
		assertEquals(msg2.Player.PlayerId, msg3.Player.PlayerId);
		
		JoinGame msg4 = (JoinGame) msg3;
		assertEquals(msg3.GameId, msg4.GameId);
		assertTrue(msg3.Player.EndPoint.HostAndPort.equals(msg4.Player.EndPoint.HostAndPort));
		assertEquals(msg3.Player.Status.getValue(), msg4.Player.Status.getValue());
		assertEquals(msg3.Player.PlayerId, msg4.Player.PlayerId);
		
		assertEquals(msg2.GameId, msg4.GameId);
		assertTrue(msg2.Player.EndPoint.HostAndPort.equals(msg4.Player.EndPoint.HostAndPort));
		assertEquals(msg2.Player.Status.getValue(), msg4.Player.Status.getValue());
		assertEquals(msg2.Player.PlayerId, msg4.Player.PlayerId);
	}
	
	@Test
	public void test_Compatabilitys() throws ClassNotFoundException, IOException 
	{
		MessageNumber.LocalProcessId = 100;
		
		JoinGame msg2 = new JoinGame();
		PlayerInfo playerInfo = new PlayerInfo();
		playerInfo.PlayerId = 10;
		playerInfo.Status = PlayerInfo.StateCode.ONLINE;
		msg2.GameId = 123;
		msg2.Player = playerInfo;
		
		byte[] bytes = msg2.Encode();
		String str = new String(bytes);
		
		Message msg = Message.Decode(bytes);
		JoinGame join = (JoinGame) msg;
		assertEquals(msg.ConvId.ProcessId, msg2.ConvId.ProcessId);
		assertEquals(msg.ConvId.SeqNumber, msg2.ConvId.SeqNumber);
		assertEquals(msg.MessageNr.ProcessId, msg2.MessageNr.ProcessId);
		assertEquals(msg.MessageNr.SeqNumber, msg2.MessageNr.SeqNumber);
		assertEquals(join.ConvId.ProcessId, msg2.ConvId.ProcessId);
		assertEquals(join.ConvId.SeqNumber, msg2.ConvId.SeqNumber);
		assertEquals(join.MessageNr.ProcessId, msg2.MessageNr.ProcessId);
		assertEquals(join.MessageNr.SeqNumber, msg2.MessageNr.SeqNumber);
		assertEquals(join.GameId, msg2.GameId);
		assertEquals(join.Player.PlayerId, msg2.Player.PlayerId);
		assertEquals(join.Player.Status.getValue(), msg2.Player.Status.getValue());
	}
	
}
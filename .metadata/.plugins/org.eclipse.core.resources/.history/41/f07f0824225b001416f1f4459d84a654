package MessageTester;

import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;

import org.junit.Test;

import Messages.JoinGame;
import Messages.Message;
import SharedObject.PlayerInfo;
import SharedObject.PublicEndPoint;

public class JoinGameTester {

	@Test
	public void test_Everythings() throws ClassNotFoundException, IOException 
	{
		JoinGame msg1 = new JoinGame();
		msg1.GameId = 10;
		PlayerInfo info = new PlayerInfo();
		PublicEndPoint ep = new PublicEndPoint();
		ep.Host = "127.0.0.1";
		ep.Port = 123456;
		info.EndPoint = ep;
		msg1.Player = info;
		
		byte[] bytes = msg1.Encode();
		InputStream myInputStream = new ByteArrayInputStream(bytes);
		ObjectInputStream oin = new ObjectInputStream(myInputStream);
		String type = (String) oin.readObject();
		
		JoinGame msg2 = (JoinGame) Message.Decode(bytes);
		
		assertNotNull(msg2);
		assertEquals(msg1.GameId, msg2.GameId);
		assertTrue(msg1.Player.EndPoint.Host.equals(msg2.Player.EndPoint.Host));
		assertEquals(msg1.Player.EndPoint.Port, msg2.Player.EndPoint.Port);
	}
}
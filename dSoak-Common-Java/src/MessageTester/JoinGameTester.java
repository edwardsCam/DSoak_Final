package MessageTester;

import static org.junit.Assert.*;

import org.junit.Test;

import Messages.JoinGame;
import Messages.Message;
import SharedObject.PlayerInfo;
import SharedObject.PublicEndPoint;

public class JoinGameTester {

	@Test
	public void test_Everythings() throws ClassNotFoundException 
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
		
		JoinGame msg2 = (JoinGame) Message.Decode(bytes);
		
		assertNotNull(msg2);
		assertEquals(msg1.GameId, msg2.GameId);
		assertTrue(msg1.Player.EndPoint.Host.equals(msg2.Player.EndPoint.Host));
		assertEquals(msg1.Player.EndPoint.Port, msg2.Player.EndPoint.Port);
	}
}
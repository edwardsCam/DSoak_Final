package MessageTester;

import static org.junit.Assert.*;

import org.junit.Test;

import Messages.GameOver;
import Messages.Message;
import SharedObject.MessageNumber;
import SharedObject.PlayerInfo;
import SharedObject.PublicEndPoint;

public class GameOverTester {

	@Test
	public void test_EveryThings() throws ClassNotFoundException
	{
		GameOver gameOver = new GameOver();
		MessageNumber msgN =  MessageNumber.Create();
		gameOver.MessageNr = msgN;
		gameOver.ConvId = gameOver.MessageNr;
		
		PublicEndPoint ep = new PublicEndPoint();
		ep.Host = "127.0.0.1";
		ep.Port = 1010;
		PlayerInfo winer = new PlayerInfo();
		winer.EndPoint = ep;
		gameOver.GameId  = (short)10;
		gameOver.Winner = winer;
		
		byte[] bytes = gameOver.Encode();
		
		GameOver msg = (GameOver)Message.Decode(bytes);
		
		assertEquals((short)10, msg.GameId);
		assertEquals(1010, msg.Winner.EndPoint.Port);
		assertTrue(msg.Winner.EndPoint.Host.equals("127.0.0.1"));
		assertEquals(msgN.ProcessId, msg.MessageNr.ProcessId);
		assertEquals(msgN.SeqNumber, msg.MessageNr.SeqNumber);
		assertEquals(winer.PlayerId, msg.Winner.PlayerId);
	}
}
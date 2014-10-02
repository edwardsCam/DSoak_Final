package MessageTester;

import static org.junit.Assert.*;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;

import org.junit.Test;

import Messages.Message;
import Messages.ThrowBalloon;
import SharedObject.Balloon;

public class ThrowBalloonTester {

	@Test
	public void test_EveryThings() throws NoSuchAlgorithmException, IOException, ClassNotFoundException 
	{
		ThrowBalloon throwBallon = new ThrowBalloon();
		throwBallon.GameId = 10;
		Balloon ballon = new Balloon();
		ballon.Id = 20;
		throwBallon.Balloon = ballon;
		throwBallon.TargetPlayerId = 12;
		
		byte[] bytes = throwBallon.Encode();
		
		ThrowBalloon throwBallon2 =(ThrowBalloon) Message.Decode(bytes);
		
		assertEquals(throwBallon.GameId, throwBallon2.GameId);
		assertEquals(throwBallon.Balloon.Id, throwBallon2.Balloon.Id);
		
		
		
	}

}
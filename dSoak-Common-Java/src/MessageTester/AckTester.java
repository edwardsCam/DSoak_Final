package MessageTester;

import java.io.IOException;
import org.junit.Test;
import Messages.Ack;
import Messages.Message;
import static org.junit.Assert.*;
import org.junit.Test;

public class AckTester {

	@Test
	public void test_EveryThings() throws IOException, ClassNotFoundException 
	{
		Ack ack = new Ack();
		ack.x = 10;
		ack.message = "ack message tester";
		byte[] bytes = ack.Encode();
		
		Ack ack2 = (Ack) Message.Decode(bytes);
		assertEquals(ack.x, ack2.x);
		assertTrue(ack.message.equals(ack2.message));
	}
}
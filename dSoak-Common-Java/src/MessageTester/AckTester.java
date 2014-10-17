package MessageTester;

import java.io.IOException;
import org.junit.Test;
import Messages.Ack;
import Messages.Message;
import static org.junit.Assert.*;

public class AckTester 
{
	@Test
	public void test_EveryThings() throws IOException, ClassNotFoundException 
	{
		Ack ack = new Ack();
		assertNotNull(ack);
		
		byte[] bytes = ack.Encode();
		
		Ack ack2 = (Ack) Message.Decode(bytes);
		assertNotNull(ack2);
	}
}
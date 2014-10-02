package MessageTester;

import static org.junit.Assert.*;

import org.junit.Test;

import Messages.Message;
import Messages.Nak;

public class NakTester {

	@Test
	public void test_EveryThings() throws ClassNotFoundException {
		Nak nak = new Nak();
		nak.Error = "Error: test error message...";
		
		byte[] bytes = nak.Encode();
		
		Nak nak2 = (Nak) Message.Decode(bytes);
		assertTrue(nak.Error.equals(nak2.Error));
	}

}
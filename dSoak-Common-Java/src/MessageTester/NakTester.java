package MessageTester;

import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;

import org.junit.Test;

import Messages.Message;
import Messages.Nak;

public class NakTester {

	@Test
	public void test_EveryThings() throws ClassNotFoundException, IOException {
		Nak nak = new Nak();
		nak.Error = "Error: test error message...";
		
		byte[] bytes = nak.Encode();
		InputStream myInputStream = new ByteArrayInputStream(bytes);
		ObjectInputStream oin = new ObjectInputStream(myInputStream);
		String type = (String) oin.readObject();
		
		Nak nak2 = (Nak) Message.Decode(bytes);
		assertTrue(nak.Error.equals(nak2.Error));
	}

}

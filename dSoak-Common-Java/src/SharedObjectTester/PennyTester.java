package SharedObjectTester;

import static org.junit.Assert.*;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;

import org.junit.Test;

import SharedObject.Penny;

public class PennyTester {

	@Test
	public void test_EveryThing() throws NoSuchAlgorithmException, IOException 
	{
		Penny penny = new Penny();
		
		assertTrue(penny.Id > 0);
		assertTrue(penny.IsValid());
		penny.Id++;
		
		//assertFalse(penny.IsValid());
	}

}

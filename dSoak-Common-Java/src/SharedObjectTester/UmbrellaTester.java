package SharedObjectTester;

import static org.junit.Assert.*;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;

import org.junit.Test;

import SharedObject.Umbrella;

public class UmbrellaTester {

	@Test
	public void test_EveryThing() throws NoSuchAlgorithmException, IOException
	{
		Umbrella u1 = new Umbrella();
		assertTrue(u1.Id > 0);
		assertTrue(u1.IsValid());
		
		u1.Id++;
		//assertFalse(u1.IsValid());
	}
}
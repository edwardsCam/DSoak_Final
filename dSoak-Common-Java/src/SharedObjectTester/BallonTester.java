package SharedObjectTester;

import static org.junit.Assert.*;
import java.io.IOException;
import java.security.NoSuchAlgorithmException;
import org.junit.Test;
import SharedObject.Balloon;

public class BallonTester 
{
	@Test
	public void test_EveryThing() throws NoSuchAlgorithmException, IOException 
	{
		Balloon b1 = new Balloon();
		assertTrue(b1.Id>0);
		assertTrue(b1.IsValid());
		assertEquals(0,  b1.UnitOfWater);
		
		b1.UnitOfWater = 1;
		assertEquals(1, b1.UnitOfWater);
	}

}

package SharedObjectTester;

import static org.junit.Assert.*;

import java.util.Date;

import org.junit.Test;

import SharedObject.UmbrellaRaising;

public class UmbrellaRaisingTester {

	@Test
	public void test_EveryThing() 
	{
		UmbrellaRaising r1 = new UmbrellaRaising();
		
		assertEquals(0, r1.GameId);
		assertEquals(0, r1.PlayerId);
		assertEquals(0, r1.UmbrellaId);
		assertNull(r1.AtTime);
		
		Date date = new Date();
		
		UmbrellaRaising r2 = new UmbrellaRaising();
		r2.GameId = 1;
		r2.PlayerId = 2;
		r2.UmbrellaId = 3;
		r2.AtTime = date;
		
		assertEquals(1, r2.GameId);
		assertEquals(2, r2.PlayerId);
		assertEquals(3, r2.UmbrellaId);
		assertEquals(date, r2.AtTime);
	}
}
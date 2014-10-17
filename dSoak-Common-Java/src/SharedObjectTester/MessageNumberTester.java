package SharedObjectTester;

import static org.junit.Assert.*;

import org.junit.Test;

import SharedObject.MessageNumber;

public class MessageNumberTester {

	@Test
	public void test_EveryThing()
	{
		MessageNumber.LocalProcessId =100;
		
		MessageNumber mn1 = MessageNumber.Create();
		assertEquals(100, mn1.ProcessId);
		assertTrue(mn1.SeqNumber > 0);
		
		MessageNumber mn2 = MessageNumber.Create();
		assertEquals(100, mn1.ProcessId);
		assertEquals(mn1.SeqNumber + 1,  mn2.SeqNumber);
		
		 MessageNumber mn3 = new MessageNumber();
		 assertEquals(0, mn3.ProcessId);
		 assertEquals(0, mn3.SeqNumber);
		 
		 mn3.ProcessId = mn2.ProcessId;
		 mn3.SeqNumber = mn2.SeqNumber;
		 
		 assertTrue(mn2.Equals(mn3));
		 assertTrue(MessageNumber.operatorLessThankOrEqual(mn1, mn3));
		 assertTrue(MessageNumber.operatorLessThan(mn1, mn3));
		 assertTrue(MessageNumber.operatorNotEqual(mn3, mn1));
		 assertTrue(MessageNumber.operatorGreaterThanOrEqual(mn3, mn1));
		 assertTrue(MessageNumber.operatorGreaterThan(mn3, mn1));
	}
}
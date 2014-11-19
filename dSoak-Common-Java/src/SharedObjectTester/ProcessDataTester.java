package SharedObjectTester;

import static org.junit.Assert.*;

import org.junit.Test;

import SharedObject.ProcessData;

public class ProcessDataTester
{
	@Test
	public void test_Everything() 
	{
		 ProcessData data = new ProcessData();
		 
		 assertEquals(0, data.GameId);
		 assertEquals(0, data.ProcessId);
		 assertEquals(null, data.ProcessType);
		 assertEquals(0, data.LifePoints);
		 assertEquals(0, data.HitPoints);
		 assertEquals(0, data.NumberOfPennies);
		 assertEquals(0, data.NumberOfUnfilledBalloon);
		 assertEquals(0, data.NumberOfFilledBalloon);
		 assertEquals(0, data.NumberOfUnraisedUmbrellas);
		 assertFalse(data.HasUmbrellaRaised);

         data = new ProcessData();
         data.GameId = 10;
         data.ProcessId = 11;
         data.ProcessType = ProcessData.PossibleProcessType.Thief;
         data.LifePoints = 12;
         data.HitPoints = 13;
         data.NumberOfPennies = 14;
         data.NumberOfUnfilledBalloon = 15;
         data.NumberOfFilledBalloon = 16;
         data.NumberOfUnraisedUmbrellas = 17;
         data.HasUmbrellaRaised = true;
         

         assertEquals(10, data.GameId);
         assertEquals(11, data.ProcessId);
         assertEquals(ProcessData.PossibleProcessType.Thief, data.ProcessType);
         assertEquals(12, data.LifePoints);
         assertEquals(13, data.HitPoints);
         assertEquals(14, data.NumberOfPennies);
         assertEquals(15, data.NumberOfUnfilledBalloon);
         assertEquals(16, data.NumberOfFilledBalloon);
         assertEquals(17, data.NumberOfUnraisedUmbrellas);
         assertTrue(data.HasUmbrellaRaised);
	}

}
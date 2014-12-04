package SharedObject;

import com.google.gson.annotations.Expose;

public class ProcessData 
{
	@Expose public short GameId;
	@Expose public short ProcessId;
	@Expose public PossibleProcessType ProcessType; 
	@Expose public short LifePoints; 
	@Expose public short HitPoints; 
	@Expose public short NumberOfPennies; 
	@Expose public short NumberOfUnfilledBalloon; 
	@Expose public short NumberOfFilledBalloon; 
	@Expose public short NumberOfUnraisedUmbrellas; 
	@Expose public boolean HasUmbrellaRaised; 
    
    public enum PossibleProcessType
	{ 
		Unknown((short)0),
		Player((short)1), 
		BalloonStore((short)2), 
		WaterServer((short)3), 
		UmbrellaSupplier((short)4), 
		Thief((short)5); 
	
	
		short value;
		PossibleProcessType(short va) { this.value = va; }
		
		public short getValue()
		{
			return value;
		}
	};
}

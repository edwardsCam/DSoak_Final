package SharedObject;

import java.io.Serializable;

public class ProcessData implements Serializable
{
	private static final long serialVersionUID = 1821207611587414708L;

	public short GameId;
    
    public short ProcessId;
    
    public PossibleProcessType ProcessType; 
    
    public short LifePoints; 
    
    public short HitPoints; 
    
    public short NumberOfPennies; 
    
    public short NumberOfUnfilledBalloon; 
    
    public short NumberOfFilledBalloon; 
    
    public short NumberOfUnraisedUmbrellas; 
    
    public boolean HasUmbrellaRaised; 
    
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

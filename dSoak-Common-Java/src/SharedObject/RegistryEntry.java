package SharedObject;

import java.io.Serializable;
import java.util.Date;

public class RegistryEntry implements Serializable
{
	private static final long serialVersionUID = -1263614345475764865L;
	
	public enum ProcessType { GameManager, Player, BalloonStore, WaterServer, UmbrellaManager, Thief };
    public PublicEndPoint Ep;
    public String Label; 
    public short ProcessId; 
    public ProcessType Type;
    public Date AliveTimestamp; 
}

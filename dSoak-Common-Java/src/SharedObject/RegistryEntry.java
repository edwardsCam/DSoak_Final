package SharedObject;

import java.util.Date;

import com.google.gson.annotations.Expose;

public class RegistryEntry 
{
	public enum ProcessType { GameManager, Player, BalloonStore, WaterServer, UmbrellaManager, Thief };
	@Expose public PublicEndPoint Ep;
	@Expose public String Label; 
	@Expose public short ProcessId; 
	@Expose public ProcessType Type;
	@Expose public Date AliveTimestamp; 
}

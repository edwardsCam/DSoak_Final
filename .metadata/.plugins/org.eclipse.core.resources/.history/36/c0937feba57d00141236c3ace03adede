package SharedObject;

import java.util.Date;

import SharedObject.PlayerInfo.StateCode;

import com.google.gson.annotations.Expose;

public class GameInfo 
{
	public short GameId;
	public String Label;
	public short FightManagerId;
	public PublicEndPoint FlightManagerEP;
	public StatusCode Status;
	public short MaxPlayers;
	public Date AliveTimeStamp;
	public short MaxThieves;

    public enum StatusCode
	{
		NOTINITIALIZED((short) 0),
		AVAILABLE((short) 1), 
		INPROGRESS((short) 2), 
		COMPLETE((short) 3), 
		CANCLED((short) 4); 
		
		short value;
		StatusCode(short va) { this.value = va;	}
		
		public short getValue()
		{
			return value;
		}
		
		public static StatusCode setValue(short b)
	   	{
			StatusCode temp = null;
		    for (StatusCode  t : StatusCode.values()) 
		    {
		    	if (t.value == b) {
		    		temp = t;
		        }
		    }
		    return temp;
		}
	}
}
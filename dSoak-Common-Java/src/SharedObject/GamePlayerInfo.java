package SharedObject;

import com.google.gson.annotations.Expose;

public class GamePlayerInfo 
{
	@Expose public short GameId; 
	@Expose public PlayerInfo Player;
	@Expose public StatusCode Status;
	
	
	public enum StatusCode
	{ 
		UNKNOWN((short)0),
		INGAME((short)1),
		LEFTGAME((short)2),
		WINNER((short)3);
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
	};
}

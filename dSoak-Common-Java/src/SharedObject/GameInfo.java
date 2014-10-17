package SharedObject;

import java.io.Serializable;

public class GameInfo implements Serializable
{
	private static final long serialVersionUID = -8579515000505941289L;
	public short GameId;
    public PublicEndPoint FlightManagerEP;
    public StatusCode Status;
    public short MaxPlayers;
    
    public enum StatusCode
	{
		UNKNOWN((short)0),
		AVAILABLE((short)1), 
		INPROGRESS((short)2), 
		COMPLETE((short)3), 
		CANCLED((short)4); 
		
		short value;
		StatusCode(short va) { this.value = va;	}
		
		public short getValue()
		{
			return value;
		}
	}
}
/*
public static short getStringValueFromInt(int i) {

    for (DISTRIBUTABLE_CLASS_IDS status : DISTRIBUTABLE_CLASS_IDS.values()) {
        if (status.getValue() == i) {
            return (short) status.getValue();
        }

    }
    throw new IllegalArgumentException("the given number doesn't match any Status.");
}

public static DISTRIBUTABLE_CLASS_IDS fromInt(int i) {
    DISTRIBUTABLE_CLASS_IDS temp = null;
    for (DISTRIBUTABLE_CLASS_IDS dt : DISTRIBUTABLE_CLASS_IDS.values()) {
        if (dt.value == i) {
            temp = dt;
        }
    }
    return temp;
}

public static DISTRIBUTABLE_CLASS_IDS fromByte(short b) {
    DISTRIBUTABLE_CLASS_IDS temp = null;
    for (DISTRIBUTABLE_CLASS_IDS t : DISTRIBUTABLE_CLASS_IDS.values()) {
        if (t.value == b) {
            temp = t;
        }
    }
    return temp;
}*/
package SharedObject;

import java.io.Serializable;

public class MessageNumber implements Comparable, Serializable
{
	private static final long SerialVersionUID = -1455333467491068998L;
	private static short nextSeqNumber = 1;             
    public static short LocalProcessId;
    public short ProcessId;
    public short SeqNumber;
 
    protected MessageNumber() { }
    
    public static MessageNumber Create() 
    {
        MessageNumber result = new MessageNumber();
        result.ProcessId = LocalProcessId;
        result.SeqNumber = GetNextSeqNumber();
        return result;
    }

    public static MessageNumber Create(short processId, short seqNumber) {
        MessageNumber result = new MessageNumber();
        result.ProcessId = processId;
        result.SeqNumber = seqNumber;
        return result;
    }


    public String toString() {
        return ProcessId + "." + SeqNumber;
    }

    public boolean Equals(Object obj) {
        boolean tag = false;
        int result = Compare(this, (MessageNumber) obj);

        if (result > 0) {
            tag = false;
        } else if (result < 0) {
            tag = false;
        } else if (result == 0) {
            tag = true;
        }
        return tag;
    }

    public int GetHashCode() {
        return super.hashCode();
    }

    public static int Compare(MessageNumber a, MessageNumber b) {
        int result = 0;

        if (!(a == b)) {
            if (((Object) a == null) && ((Object) b != null)) {
                result = -1;
            } else if (((Object) a != null) && ((Object) b == null)) {
                result = 1;
            } else {
                if (a.ProcessId < b.ProcessId) {
                    result = -1;
                } else if (a.ProcessId > b.ProcessId) {
                    result = 1;
                } else if (a.SeqNumber < b.SeqNumber) {
                    result = -1;
                } else if (a.SeqNumber > b.SeqNumber) {
                    result = 1;
                }
            }
        }
        return result;
    }

    public static boolean operatorEqual(MessageNumber a, MessageNumber b) {
        return (Compare(a, b) == 0);
    }

    public static boolean operatorNotEqual(MessageNumber a, MessageNumber b) {
        return (Compare(a, b) != 0);
    }

    public static boolean operatorLessThan(MessageNumber a, MessageNumber b) {
        return (Compare(a, b) < 0);
    }

    public static boolean operatorGreaterThan(MessageNumber a, MessageNumber b) {
        return (Compare(a, b) > 0);
    }

    public static boolean operatorLessThankOrEqual(MessageNumber a, MessageNumber b) {
        return (Compare(a, b) <= 0);
    }

    public static boolean operatorGreaterThanOrEqual(MessageNumber a, MessageNumber b) {
        return (Compare(a, b) >= 0);
    }

    public int CompareTo(Object obj) {
        return Compare(this, (MessageNumber) obj);
    }

    public static MessageNumber getEmpty() {
        return new MessageNumber();
    }

    public static short getLocalProcessId() {
        return LocalProcessId;
    }

    public static void setLocalProcessId(short localProcessId) {
        LocalProcessId = localProcessId;
    }

    public short getProcessId() {
        return ProcessId;
    }

    public void setProcessId(short processId) {
        ProcessId = processId;
    }

    public short getSeqNumber() {
        return SeqNumber;
    }

    public void setSeqNumber(short seqNumber) {
        SeqNumber = seqNumber;
    }

    private static short GetNextSeqNumber()
    {
        if (nextSeqNumber == Short.MAX_VALUE) 
           nextSeqNumber = 1;
        return nextSeqNumber++;
    }

	public int compareTo(Object obj) {
		return Compare(this, (MessageNumber) obj);
	}
}
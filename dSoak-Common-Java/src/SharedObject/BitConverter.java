package SharedObject;

import java.io.Serializable;
import java.lang.Exception;

// This class is because Java has no bitConverter class.
// http://www.nextgenupdate.com/forums/computer-programming/394645-java-bitconverter-c.html
public class BitConverter implements Serializable 
{

	private static final long serialVersionUID = 559539043095715455L;
	public static final boolean IsLittleEndian = false;

    public static byte[] getBytes(boolean x) {
        return new byte[]{
            (byte) (x ? 1 : 0)
        };
    }

    public static byte[] getBytes(char c) {
        return new byte[]{
            (byte) (c & 0xff),
            (byte) (c >> 8 & 0xff)};
    }

    public static byte[] getBytes(double x) {
        return getBytes(
                Double.doubleToRawLongBits(x));
    }

    public static byte[] getBytes(short x) {
//        	return new byte[]{(byte)(x & 0x00FF),(byte)((x & 0xFF00)>>8)};
        return new byte[]{
            (byte) (x >>> 8),
            (byte) x
        };
    }

    public static byte[] getBytes(int x) {
        return new byte[]{
            (byte) (x >>> 24),
            (byte) (x >>> 16),
            (byte) (x >>> 8),
            (byte) x
        };
    }

    public static byte[] getBytes(long x) {
        return new byte[]{
            (byte) (x >>> 56),
            (byte) (x >>> 48),
            (byte) (x >>> 40),
            (byte) (x >>> 32),
            (byte) (x >>> 24),
            (byte) (x >>> 16),
            (byte) (x >>> 8),
            (byte) x
        };
    }

    public static byte[] getBytes(float x) {
        return getBytes(
                Float.floatToRawIntBits(x));
    }

    public static byte[] getBytes(String x) {
        return x.getBytes();
    }

    public static long doubleToInt64Bits(double x) {
        return Double.doubleToRawLongBits(x);
    }

    public static double int64BitsToDouble(long x) {
        return (double) x;
    }

    public boolean toBoolean(byte[] bytes, int index) throws Exception {
        if (bytes.length != 1) {
            throw new Exception("The length of the byte array must be at least 1 byte long.");
        }
        return bytes[index] != 0;
    }

    public static char toChar(byte[] bytes, int index) throws Exception {
        if (bytes.length != 2) {
            throw new Exception("The length of the byte array must be at least 2 bytes long.");
        }
        return (char) ((0xff & bytes[index]) << 8
                | (0xff & bytes[index + 1]) << 0);
    }

    public static double toDouble(byte[] bytes, int index) throws Exception {
        if (bytes.length != 8) {
            throw new Exception("The length of the byte array must be at least 8 bytes long.");
        }
        return Double.longBitsToDouble(
                toInt64(bytes, index));
    }

    public static short toInt16(byte[] bytes, int index) throws Exception {
        if (bytes.length != 8)
        		;//throw new Exception("The length of the byte array must be at least 8 bytes long.");
        return (short) ((0xff & bytes[index]) << 8
                | (0xff & bytes[index + 1]) << 0);
    }

    public static int toInt32(byte[] bytes, int index) throws Exception {
        if (bytes.length != 4) {
            throw new Exception("The length of the byte array must be at least 4 bytes long.");
        }
        return (int) ((int) (0xff & bytes[index]) << 56
                | (int) (0xff & bytes[index + 1]) << 48
                | (int) (0xff & bytes[index + 2]) << 40
                | (int) (0xff & bytes[index + 3]) << 32);
    }

    public static long toInt64(byte[] bytes, int index) throws Exception {
        if (bytes.length != 8) {
            throw new Exception("The length of the byte array must be at least 8 bytes long.");
        }
        return (long) ((long) (0xff & bytes[index]) << 56
                | (long) (0xff & bytes[index + 1]) << 48
                | (long) (0xff & bytes[index + 2]) << 40
                | (long) (0xff & bytes[index + 3]) << 32
                | (long) (0xff & bytes[index + 4]) << 24
                | (long) (0xff & bytes[index + 5]) << 16
                | (long) (0xff & bytes[index + 6]) << 8
                | (long) (0xff & bytes[index + 7]) << 0);
    }

    public static float toSingle(byte[] bytes, int index) throws Exception {
        if (bytes.length != 4) {
            throw new Exception("The length of the byte array must be at least 4 bytes long.");
        }
        return Float.intBitsToFloat(
                toInt32(bytes, index));
    }

    public static String toString(byte[] bytes) throws Exception {
        if (bytes == null) {
            throw new Exception("The byte array must have at least 1 byte.");
        }
        return new String(bytes);
    }
    public static byte[] toByteArray(double d) {
	    long l = Double.doubleToRawLongBits(d);
	    return new byte[] {
	        (byte)((l >> 56) & 0xff),
	        (byte)((l >> 48) & 0xff),
	        (byte)((l >> 40) & 0xff),
	        (byte)((l >> 32) & 0xff),
	        (byte)((l >> 24) & 0xff),
	        (byte)((l >> 16) & 0xff),
	        (byte)((l >> 8) & 0xff),
	        (byte)((l >> 0) & 0xff),
	    };
	}
}

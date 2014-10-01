package Messages;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInput;
import java.io.ObjectInputStream;
import java.io.ObjectOutput;
import java.io.ObjectOutputStream;
import java.io.OutputStream;
import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;

public class Message implements Serializable
{
	private static final long SerialVersionUID = -1455333437430468800L;
	private byte[] bytes = null;
	
	public byte[] Encode()
	{
		ByteArrayOutputStream bos = new ByteArrayOutputStream();
		ObjectOutput out = null;
		try
		{
			out = new ObjectOutputStream(bos);
			out.writeObject(this);
			bytes = bos.toByteArray();
		}
		catch (IOException ex) {}
		return bytes;
	}
	
	public static Message Decode(byte[] bytes) throws ClassNotFoundException
    {
		Message msg = null;
		ByteArrayInputStream bis = new ByteArrayInputStream(bytes);
		ObjectInput in = null;
		try
		{
			in = new ObjectInputStream(bis);
			msg = (Message) in.readObject();
		}
		catch (IOException e) {}
		
		return msg;
    }
}
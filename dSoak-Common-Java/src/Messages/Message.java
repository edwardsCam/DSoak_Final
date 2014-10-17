package Messages;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.ObjectInput;
import java.io.ObjectInputStream;
import java.io.ObjectOutput;
import java.io.ObjectOutputStream;
import java.io.Serializable;
import SharedObject.MessageNumber;

public class Message implements Serializable
{
	private static final long serialVersionUID = 2731655107048353376L;
	
	private byte[] bytes = null;
	public MessageNumber MessageNr;
	public MessageNumber ConvId;
	
	public Message() 
	{
		MessageNr =MessageNumber.Create();
		ConvId = MessageNr;
	}
	
	public byte[] Encode()
	{
		ByteArrayOutputStream bos = new ByteArrayOutputStream();
		ObjectOutput out = null;
		try
		{
			out = new ObjectOutputStream(bos);
			out.writeObject(this);
			bytes = bos.toByteArray();
			out.close();
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
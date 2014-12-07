package SharedObject;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.security.NoSuchAlgorithmException;

import com.google.gson.annotations.Expose;

public class Balloon extends SharedResource
{
	@Expose public short UnitOfWater;
	
	public Balloon() throws NoSuchAlgorithmException, IOException {
		super();
	}
	
	@Override
	protected void AddOwnDataToStream(ByteArrayOutputStream mStream) throws IOException 
	{
		byte[] tmp = BitConverter.getBytes(UnitOfWater);
		mStream.write(tmp, 0, tmp.length);
		super.AddOwnDataToStream(mStream);
	}
}

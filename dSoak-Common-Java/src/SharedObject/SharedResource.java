package SharedObject;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.ObjectOutputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Random;

public class SharedResource
{
	  private static short nextId = 0;
      private static byte[] nounce;
      private static Random randomizer;
      private static MessageDigest digest;
      
      public short Id;
      public byte[] DigitalSignature;
      
      public SharedResource() throws NoSuchAlgorithmException, IOException
      {
    	  randomizer = new Random();
    	  Id = GetNextId();
          Sign();
      }
      
      private static void Initialize() throws NoSuchAlgorithmException
      {
    	  digest = MessageDigest.getInstance("MD5");
          randomizer.nextInt();
          nounce = BitConverter.getBytes(randomizer.nextInt());
      }
      
      private static short GetNextId()
      {
          if (nextId == Short.MAX_VALUE)
              nextId = 0;
          return ++nextId;
      }
      
      protected void Sign() throws IOException, NoSuchAlgorithmException
      {
          if (digest == null)
              Initialize();
          DigitalSignature = ComputeDigitalSignature(new ByteArrayOutputStream());
      }
      
      protected byte[] ComputeDigitalSignature(ByteArrayOutputStream mStream) throws IOException
      {
          AddOwnDataToStream(mStream);
          mStream.reset();

          byte[] result = digest.digest();
          return result;
      }
      
      protected void AddOwnDataToStream(ByteArrayOutputStream mStream) throws IOException
      {
          byte[] idBytes = BitConverter.getBytes(Id);
          mStream.write(idBytes, 0, idBytes.length);
          if (nounce != null)
        	  mStream.write(nounce, 0, nounce.length);
      }
}
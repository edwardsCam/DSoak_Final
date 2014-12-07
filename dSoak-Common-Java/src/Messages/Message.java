package Messages;
// https://sites.google.com/site/gson/streaming
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.nio.ByteBuffer;
import java.util.HashMap;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.annotations.Expose;
import com.google.gson.stream.JsonReader;

import Deserializer.BalloonDesrializer;
import Deserializer.BalloonFilledDeserializer;
import Deserializer.GameInfoDeserializer;
import Deserializer.GameOverDeserializer;
import Deserializer.PlayerInfoDeserializer;
import Deserializer.ProcessDataDeserializer;
import Deserializer.PublicEndPointDeserializer;
import Deserializer.SharedResourceDeserializer;
import Deserializer.UmbrellaDesrializer;
import Serializers.BalloonFilledSerializer;
import Serializers.BalloonSerializer;
import Serializers.GameInfoSerializer;
import Serializers.GameOverSerializer;
import Serializers.PennySerializer;
import Serializers.PlayerInfoSerializer;
import Serializers.ProcessDataSerializer;
import Serializers.PublicEndPointSerializer;
import Serializers.SharedResourceSerializer;
import Serializers.UmbrellaPurchasedSerializer;
import Serializers.UmbrellaSerializer;
import SharedObject.Balloon;
import SharedObject.GameInfo;
import SharedObject.GamePlayerInfo;
import SharedObject.MessageNumber;
import SharedObject.Penny;
import SharedObject.PlayerInfo;
import SharedObject.ProcessData;
import SharedObject.PublicEndPoint;
import SharedObject.RegistryEntry;
import SharedObject.SharedResource;
import SharedObject.Umbrella;
import SharedObject.UmbrellaRaising;

public class Message 
{
	@Expose public MessageNumber MessageNr;
	@Expose public MessageNumber ConvId;
	private static boolean hasBeenInitialized = false;
	private static HashMap<String, Class<?>> classType = null;
	
	public Message() 
	{
		MessageNr = MessageNumber.Create();
		ConvId = MessageNr;
	}
	
	public byte[] Encode() throws IOException 
	{
		ByteArrayOutputStream byteArrayOutStream = new ByteArrayOutputStream();
		OutputStreamWriter outstreamWriter = new OutputStreamWriter(byteArrayOutStream, "US-ASCII"); 
	
		String type = this.getClass().getSimpleName() + ":";
		outstreamWriter.write(type);
		outstreamWriter.flush();
		final GsonBuilder gsonBuilder = new GsonBuilder();
	
		gsonBuilder.registerTypeAdapter(PublicEndPoint.class, new PublicEndPointSerializer());
		gsonBuilder.registerTypeAdapter(PlayerInfo.class, new PlayerInfoSerializer());
		gsonBuilder.registerTypeAdapter(GameOver.class, new GameOverSerializer());
		gsonBuilder.registerTypeAdapter(GameInfo.class, new GameInfoSerializer());
		gsonBuilder.registerTypeAdapter(ProcessData.class, new ProcessDataSerializer());
		gsonBuilder.registerTypeAdapter(SharedResource.class, new SharedResourceSerializer());
		gsonBuilder.registerTypeAdapter(Umbrella.class, new UmbrellaSerializer());
		gsonBuilder.registerTypeAdapter(Balloon.class, new BalloonSerializer());
		gsonBuilder.registerTypeAdapter(Penny.class, new PennySerializer());
		
		Gson gson = gsonBuilder.excludeFieldsWithoutExposeAnnotation().serializeNulls().create(); 
		gson.toJson(this, this.getClass(), outstreamWriter);
		outstreamWriter.flush();
		return byteArrayOutStream.toByteArray();
	}
	
	public static Message Decode(byte[] bytes) throws ClassNotFoundException, IOException
    {
	    Message message = null ;
		ByteBuffer stream = ByteBuffer.wrap(bytes);
		String typeName = ParseTypeName(stream);
		Class<?> classType = LookupClassType(typeName);
		ByteArrayInputStream input = new ByteArrayInputStream(bytes, typeName.length() + 1, bytes.length- typeName.length() -1);
		JsonReader reader = new JsonReader(new InputStreamReader(input, "US-ASCII"));
		final GsonBuilder gsonBuilder = new GsonBuilder();
		
		gsonBuilder.registerTypeAdapter(PublicEndPoint.class, new PublicEndPointDeserializer());
		gsonBuilder.registerTypeAdapter(PlayerInfo.class, new PlayerInfoDeserializer());
		gsonBuilder.registerTypeAdapter(GameOver.class, new GameOverDeserializer());
		gsonBuilder.registerTypeAdapter(GameInfo.class, new GameInfoDeserializer());
		gsonBuilder.registerTypeAdapter(ProcessData.class, new ProcessDataDeserializer());
		gsonBuilder.registerTypeAdapter(SharedResource.class, new SharedResourceDeserializer());
		
		Gson gson = gsonBuilder.create();
		
	   	message = (Message) gson.fromJson(reader, classType);
	    return message;
    }
	
	private static String ParseTypeName(ByteBuffer buff) throws IOException, ClassNotFoundException
	{
		String result = "";
		byte[] bytes = new byte[buff.capacity() - buff.position()];
		int index;
        for (index = 0; index < (buff.capacity() - buff.position()); index++)
        {
        	bytes[index] = (byte)buff.get();
            if (bytes[index] == (int)':')
            	  break;
          }
          if (index > 0)
          {
        	  result = new String(bytes, 0, index);
          }
          return result;
 	}
	
	 private static Class<?> LookupClassType(String typeName)
     {
         if (!hasBeenInitialized)
             Initialize();

         return classType.get(typeName);
     }
	 
	 private static void Initialize()
     {
         classType = new HashMap<String, Class<?>>();
         
         classType.put("Ack", Ack.class);
         classType.put("AliveQuery", AliveQuery.class);
         classType.put("BalloonFilled", BalloonFilled.class);
         classType.put("BalloonPurchased", BalloonPurchased.class);
         classType.put("BalloonStolen", BalloonStolen.class);
         classType.put("BlockStealing", BlockStealing.class);
         classType.put("BuyBalloon", BuyBalloon.class);
         classType.put("BuyUmbrella", BuyUmbrella.class);
         classType.put("Continue", Continue.class);
         classType.put("FillBalloon", FillBalloon.class);
         classType.put("GameData", GameData.class);
         classType.put("GameJoined", GameJoined.class);
         classType.put("GameOver", GameOver.class);
         classType.put("GiveUpABalloon", GiveUpABalloon.class);
         classType.put("Hit", Hit.class);
         classType.put("JoinGame", JoinGame.class); 
         classType.put("LeaveGame", LeaveGame.class);
         classType.put("Nak", Nak.class); 
         classType.put("ProcessSummary", ProcessSummary.class);
         classType.put("RaiseUmbrella", RaiseUmbrella.class);
         classType.put("ResourceRequest", ResourceRequest.class);
         classType.put("SetupStream", SetupStream.class);
         classType.put("Shutdown", Shutdown.class);
         classType.put("Stealing", Stealing.class);
         classType.put("StealingBase", StealingBase.class);
         classType.put("StealingBlocked", StealingBlocked.class);
         classType.put("StopStream", StopStream.class);
         classType.put("UmbrellaPurchased", UmbrellaPurchased.class);
         classType.put("ThrowBalloon", ThrowBalloon.class);
         
         //classType.put("Balloon", Balloon.class);
         classType.put("GameInfo", GameInfo.class);
         //classType.put("GamePlayerInfo", GamePlayerInfo.class);
        // classType.put("MessageNumber", MessageNumber.class);
         //classType.put("Penny", Penny.class);
         classType.put("PlayerInfo", PlayerInfo.class);
         classType.put("PublicEndPoint", PublicEndPoint.class);
         //classType.put("RegistryEntry", RegistryEntry.class);
         //classType.put("SharedResource", SharedResource.class);
         //classType.put("Umbrella", Umbrella.class);
         //classType.put("UmbrellaRaising", UmbrellaRaising.class);
    }
}
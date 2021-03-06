package Deserializer;

import java.lang.reflect.Type;
import SharedObject.SharedResource;
import com.google.gson.JsonArray;
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParseException;

public class SharedResourceDeserializer implements JsonDeserializer<SharedResource>
{
	public SharedResource deserialize(JsonElement jsonElement, Type type,	JsonDeserializationContext context) throws JsonParseException
	{
		final JsonObject jsonObject = jsonElement.getAsJsonObject();
		final JsonElement jsonId = jsonObject.get("Id");
		final short id = jsonId.getAsShort();
		
		final JsonArray jsonDigitalSignature = jsonObject.get("DigitalSignature").getAsJsonArray();
		final byte[] sig = new byte[jsonDigitalSignature.size()];
		
		for(int i =0; i<sig.length; i++)
		{
			final JsonElement jsonsig = jsonDigitalSignature.get(i);
			sig[i] = jsonsig.getAsByte();
		}
		
		SharedResource sharedResources = new SharedResource();
		sharedResources.Id = id;
		sharedResources.DigitalSignature = sig;
		
		return sharedResources;
	}

}

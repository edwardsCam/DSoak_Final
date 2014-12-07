package Serializers;

import java.lang.reflect.Type;

import SharedObject.SharedResource;

import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonPrimitive;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

public class SharedResourceSerializer implements JsonSerializer<SharedResource>
{
	public JsonElement serialize(final SharedResource sharedResources, Type type, JsonSerializationContext context) 
	{
		final JsonObject jsonObject = new JsonObject();
		jsonObject.addProperty("Id", sharedResources.Id);
		
		final JsonArray jsonDigitalSignature = new JsonArray();
		
		byte[] byteArray = sharedResources.DigitalSignature;
		for (int i = 0; i < byteArray.length; i++)
			byteArray[i] = (byte)convertToUnsigned(byteArray[i]);
	
		for (final byte sig: byteArray)
		{
			final JsonPrimitive jsonSig = new JsonPrimitive(sig);
			jsonDigitalSignature.add(jsonSig);
		}
		
		jsonObject.add("DigitalSignature", jsonDigitalSignature);
		return jsonObject;
	}
	
	private int convertToUnsigned(byte b)
	{
		int temp;
		if (b < 0)
			temp = 256 - b;
		else
			temp = b;
		return temp;
	}

}

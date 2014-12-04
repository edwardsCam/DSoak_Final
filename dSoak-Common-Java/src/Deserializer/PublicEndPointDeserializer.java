package Deserializer;

import java.lang.reflect.Type;
import SharedObject.PublicEndPoint;
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParseException;

public class PublicEndPointDeserializer implements JsonDeserializer<PublicEndPoint>
{

	public PublicEndPoint deserialize(JsonElement jsonElement, Type type, JsonDeserializationContext context) throws JsonParseException 
	{
		final JsonObject jsonObject = jsonElement.getAsJsonObject();
		final JsonElement jsonHostandPort = jsonObject.get("HostAndPort");
		final String  hostandport = jsonHostandPort.getAsString();

		PublicEndPoint publicEP = new PublicEndPoint();
		publicEP.setHostAndPort(hostandport);
		
		return publicEP;
	}

}

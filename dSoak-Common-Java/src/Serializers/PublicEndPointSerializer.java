package Serializers;

import java.lang.reflect.Type;
import SharedObject.PublicEndPoint;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

public class PublicEndPointSerializer  implements JsonSerializer<PublicEndPoint> 
{
	public JsonElement serialize(PublicEndPoint publicEP, Type arg1, JsonSerializationContext arg2) 
	{
		String hostandport = "";
		String address = "";
		final JsonObject jsonObject = new JsonObject();
		
		String temp = publicEP.getHost();
		
		if(temp.indexOf('/') >= 0)	
		{
			address = temp.substring(temp.indexOf('/') + 1, temp.length());
		}
		else
		{
			address = temp;
		}
		hostandport = address + ":" + publicEP.getPort();
		jsonObject.addProperty("HostAndPort", hostandport);
		
		return jsonObject;
	}

}
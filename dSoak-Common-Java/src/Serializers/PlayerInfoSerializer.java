package Serializers;


import java.lang.reflect.Type;

import SharedObject.PlayerInfo;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

public class PlayerInfoSerializer implements JsonSerializer<PlayerInfo> 
{
	public JsonElement serialize(PlayerInfo playerInfo, Type type, JsonSerializationContext context) 
	{
		final JsonObject jsonObject = new JsonObject();

		jsonObject.addProperty("PlayerId", playerInfo.PlayerId);
		jsonObject.addProperty("Status", playerInfo.Status.getValue());
		
		final JsonElement pubendpoint = context.serialize(playerInfo.EndPoint);
	
		jsonObject.add("EndPoint", pubendpoint);
		
		return jsonObject;
	}
}
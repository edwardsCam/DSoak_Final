package Serializers;

import java.lang.reflect.Type;

import SharedObject.GameInfo;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

public class GameInfoSerializer implements JsonSerializer<GameInfo> 
{
	public JsonElement serialize(GameInfo gameInfo, Type type, JsonSerializationContext context) 
	{
		final JsonObject jsonObject = new JsonObject();

		jsonObject.addProperty("GameId", gameInfo.GameId);
		jsonObject.addProperty("Label", gameInfo.Label);
		jsonObject.addProperty("FightManagerId", gameInfo.FightManagerId);
		
		final JsonElement flightManagerEP = context.serialize(gameInfo.FlightManagerEP);
		jsonObject.add("FlightManagerEP", flightManagerEP);
		
		jsonObject.addProperty("Status", gameInfo.Status.getValue());
		jsonObject.addProperty("MaxPlayers", gameInfo.MaxPlayers);
		jsonObject.addProperty("MaxThieves", gameInfo.MaxThieves);
		
		return jsonObject;
	}
}
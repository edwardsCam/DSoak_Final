package Deserializer;

import java.lang.reflect.Type;
import SharedObject.GameInfo;
import SharedObject.PublicEndPoint;
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParseException;

public class GameInfoDeserializer  implements JsonDeserializer<GameInfo>
{
	public GameInfo deserialize(JsonElement jsonElement, Type type, JsonDeserializationContext context) throws JsonParseException 
	{
		final JsonObject jsonObject = jsonElement.getAsJsonObject();
		final JsonElement jsonGameId = jsonObject.get("GameId");
		final short gameId = jsonGameId.getAsShort();
		
		final JsonElement jsonLabel = jsonObject.get("Label");
		final String label = jsonLabel.getAsString();
		
		final JsonElement jsonFightManagerId = jsonObject.get("FightManagerId");
		final short fightManagerId = jsonFightManagerId.getAsShort();
		
		PublicEndPoint  fmEP = context.deserialize(jsonObject.get("FlightManagerEP"), PublicEndPoint.class);
		
		final JsonElement status = jsonObject.get("Status");
		final short statusS = status.getAsShort();
		
		final JsonElement maxPlayer = jsonObject.get("MaxPlayers"); 
		final short maxP = maxPlayer.getAsShort();
		
		
		final JsonElement maxTheives = jsonObject.get("MaxThieves"); 
		final short maxT = maxTheives.getAsShort();
		
		GameInfo gameInfo = new GameInfo();
		gameInfo.GameId = gameId;
		gameInfo.Label = label;
		gameInfo.FightManagerId = fightManagerId;
		gameInfo.FlightManagerEP = fmEP;
		gameInfo.Status = GameInfo.StatusCode.setValue(statusS);
		gameInfo.MaxPlayers = maxP;
		gameInfo.MaxThieves = maxT;
		
		return gameInfo;
	}
}
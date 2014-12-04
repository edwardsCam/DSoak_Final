package Deserializer;

import java.lang.reflect.Type;
import Messages.GameOver;
import SharedObject.PlayerInfo;
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParseException;

public class GameOverDeserializer implements JsonDeserializer<GameOver>
{
	public GameOver deserialize(JsonElement jsonElement, Type type, JsonDeserializationContext context) throws JsonParseException 
	{
		final JsonObject jsonObject = jsonElement.getAsJsonObject();
		final JsonElement jsonGameId = jsonObject.get("GameId");
		
		final short gameId = jsonGameId.getAsShort();
		PlayerInfo winner = context.deserialize(jsonObject.get("Winner"), PlayerInfo.class);
		
		GameOver gameOver = new GameOver();
		gameOver.GameId = gameId;
		gameOver.Winner = winner;
		return gameOver;
	}

}

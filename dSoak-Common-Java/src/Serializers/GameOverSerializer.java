package Serializers;

import java.lang.reflect.Type;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

import Messages.GameOver;


public class GameOverSerializer implements JsonSerializer<GameOver>
{
	public JsonElement serialize(GameOver gameover, Type type,	JsonSerializationContext context)
	{
		final JsonObject jsonObject = new JsonObject();

		jsonObject.addProperty("GameId", gameover.GameId);
		final JsonElement winner = context.serialize(gameover.Winner);
		jsonObject.add("Winner", winner);
		
		return jsonObject;
	}
}

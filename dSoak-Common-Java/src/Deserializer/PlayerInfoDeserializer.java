package Deserializer;

import java.lang.reflect.Type;

import SharedObject.PlayerInfo;
import SharedObject.PublicEndPoint;

import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParseException;

public class PlayerInfoDeserializer implements JsonDeserializer<PlayerInfo>
{

	public PlayerInfo deserialize(JsonElement jsonElement, Type type,	JsonDeserializationContext context) throws JsonParseException 
	{
		final JsonObject jsonObject = jsonElement.getAsJsonObject();
		final JsonElement jsonPlayerId = jsonObject.get("PlayerId");
		
		final short PlayerId = jsonPlayerId.getAsShort();
		final short Status = jsonObject.get("Status").getAsShort();

		PublicEndPoint publicEp = context.deserialize(jsonObject.get("EndPoint"), PublicEndPoint.class);
		
		final PlayerInfo playerInfo = new PlayerInfo();
		playerInfo.PlayerId = PlayerId;
		playerInfo.Status = PlayerInfo.StateCode.setValue(Status);
		playerInfo.EndPoint = publicEp;
		
		return playerInfo;
	}

}

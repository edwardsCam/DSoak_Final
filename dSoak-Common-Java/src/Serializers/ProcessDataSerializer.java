package Serializers;

import java.lang.reflect.Type;

import SharedObject.ProcessData;
import SharedObject.ProcessData.PossibleProcessType;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;
import com.google.gson.annotations.Expose;

public class ProcessDataSerializer implements JsonSerializer<ProcessData> 
{
	public JsonElement serialize(ProcessData processData, Type type, JsonSerializationContext context) 
	{
		final JsonObject jsonObject = new JsonObject();

		jsonObject.addProperty("GameId", processData.GameId);
		jsonObject.addProperty("ProcessId", processData.ProcessId);
		jsonObject.addProperty("ProcessType", processData.ProcessType.getValue());
		jsonObject.addProperty("LifePoints", processData.LifePoints);
		jsonObject.addProperty("HitPoints", processData.HitPoints);
		jsonObject.addProperty("NumberOfPennies", processData.NumberOfPennies);
		jsonObject.addProperty("NumberOfUnfilledBalloon", processData.NumberOfUnfilledBalloon);
		
		jsonObject.addProperty("NumberOfFilledBalloon", processData.NumberOfFilledBalloon);
		jsonObject.addProperty("NumberOfUnraisedUmbrellas", processData.NumberOfUnraisedUmbrellas);
		jsonObject.addProperty("HasUmbrellaRaised", processData.HasUmbrellaRaised);

		return jsonObject;
	}

}
/*
@Expose public short GameId;
@Expose public short ProcessId;
@Expose public PossibleProcessType ProcessType; 
@Expose public short ; 
@Expose public short ; 
@Expose public short ; 
@Expose public short ; 
@Expose public short ; 
@Expose public short ; 
@Expose public boolean ; */
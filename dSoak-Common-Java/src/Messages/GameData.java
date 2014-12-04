package Messages;

import java.util.List;

import com.google.gson.annotations.Expose;

import SharedObject.*;

public class GameData extends Message 
{
	@Expose public GameInfo Info;
	@Expose public List<ProcessData> Processes;
}

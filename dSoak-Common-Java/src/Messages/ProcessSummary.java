package Messages;

import com.google.gson.annotations.Expose;
import SharedObject.ProcessData;

public class ProcessSummary extends Message 
{
	@Expose public ProcessData Data;
}

package Messages;

import java.io.Serializable;

import SharedObject.ProcessData;

public class ProcessSummary extends Message implements Serializable
{
	private static final long serialVersionUID = -133431157696154433L;
	public ProcessData Data;
}

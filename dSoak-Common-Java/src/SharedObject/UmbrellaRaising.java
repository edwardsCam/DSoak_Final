package SharedObject;

import java.util.Date;
import com.google.gson.annotations.Expose;

public class UmbrellaRaising 
{
	@Expose public short GameId;
	@Expose public short PlayerId;
	@Expose public short UmbrellaId;
	@Expose public Date AtTime;
}

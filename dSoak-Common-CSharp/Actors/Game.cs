using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

/*
 * Contains the state for the current game.
 * */

namespace Actors
{
	public class Game
	{

		#region Private Properties

		private Knapsack r;
		private string message;
		private string label;
		private bool raisedUmbrella;
		private bool active;
		private short lp;
		private short id;
		private short maxPlayers;
		//private DateTime timestamp;
		private SharedObjects.PublicEndPoint fightManagerEP;
		private SharedObjects.GameInfo.StatusCode status;

		#endregion

		#region Constructor and destructor

		public Game()
		{
			r = new Knapsack();
			raisedUmbrella = false;
			active = false;
			fightManagerEP = null;
			status = SharedObjects.GameInfo.StatusCode.NotInitialized;
		}

		public Game(_Registrar.GameInfo g)
		{
			if (g != null)
			{
				r = new Knapsack();
				raisedUmbrella = false;
				active = false;
				id = g.GameId;
				label = g.Label;
				status = (SharedObjects.GameInfo.StatusCode)g.Status;
				maxPlayers = g.MaxPlayers;
				fightManagerEP = new SharedObjects.PublicEndPoint(g.FightManagerEP.HostAndPort);
				//timestamp = g.AliveTimestamp;
			}
			else
				new Game();
		}

		public void clear()
		{
			r.clear();
			status = SharedObjects.GameInfo.StatusCode.NotInitialized;
			fightManagerEP = null;
		}

		#endregion

		#region Public Methods

		public void setInitialLP(short p)
		{
			lp = p;
		}

		public short getLP()
		{
			return lp;
		}

		#region Resource Methods

		public void raiseUmbrella()
		{
			raisedUmbrella = true;
		}

		public void lowerUmbrella()
		{
			raisedUmbrella = false;
		}

		public int umbrellaState()
		{
			if (r.umbrella == null)
				return 0;
			else
				if (!raisedUmbrella)
					return 1;
				else
					return 2;
		}

		public int pennyCount()
		{
			return r.pennies.Count();
		}

		public bool hasUmbrella()
		{
			return r.umbrella != null;
		}

		public bool umbrellaIsRaised()
		{
			return raisedUmbrella;
		}

		public string balloonStatus()
		{
			if (r.balloon == null)
				return "NO BALLOON";
			else
				return "" + r.balloon.UnitsOfWater;
		}

		#endregion

		#region Accessors

		public bool isActive()
		{
			return active;
		}

		public List<SharedObjects.Penny> getPennyList()
		{
			return r.pennies;
		}

		public SharedObjects.Balloon getBalloon()
		{
			return r.balloon;
		}

		public SharedObjects.Umbrella getUmbrella()
		{
			return r.umbrella;
		}

		public SharedObjects.PublicEndPoint getFightManagerEP()
		{
			return fightManagerEP;
		}

		public SharedObjects.GameInfo.StatusCode getStatus()
		{
			return status;
		}

		public short getID()
		{
			return id;
		}

		public short getMaxPlayers()
		{
			return maxPlayers;
		}

		public string getLabel()
		{
			return label;
		}

		public string error_message()
		{
			if (message == "")
				return "ERROR: NO COMMUNICATION WITH SERVER";
			else
				return message;
		}

		#endregion

		#region Mutators

		public void activate()
		{
			active = true;
		}

		public void setMessage(string m)
		{
			message = m;
		}

		public void setPennyList(List<SharedObjects.Penny> l)
		{
			r.pennies = l;
		}

		public void setBalloon(SharedObjects.Balloon b)
		{
			r.balloon = b;
		}

		public void setUmbrella(SharedObjects.Umbrella u)
		{
			r.umbrella = u;
		}

		public void setID(short i)
		{
			id = i;
		}

		#endregion

		#endregion

	}
}

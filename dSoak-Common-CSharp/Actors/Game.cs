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
		private string label;
		private bool raisedUmbrella;
		private bool active;
		private short lp;
		private short id;
		private short maxPlayers;
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

		public Knapsack getResource()
		{
			return r;
		}

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

		public int pennyCount()
		{
			return r.pennies.Count();
		}

		public bool hasUmbrellas()
		{
			return r.umbrellas.Count() > 0;
		}

		public bool umbrellaIsRaised()
		{
			return raisedUmbrella;
		}

		#endregion

		#region Accessors

		public bool isActive()
		{
			return active;
		}

		public bool hasBalloons()
		{
			return r.balloons.Count() > 0;
		}

		public List<SharedObjects.Penny> getPennyList()
		{
			return r.pennies;
		}

		public List<SharedObjects.Balloon> getBalloonList()
		{
			return r.balloons;
		}

		public List<SharedObjects.Umbrella> getUmbrellaList()
		{
			return r.umbrellas;
		}

		public SharedObjects.Balloon getBalloonToThrow()
		{
			foreach (SharedObjects.Balloon b in r.balloons.ToList())			
				if (b.IsValid && b.UnitsOfWater > 0)			
					return b;
			return null;
		}

		public SharedObjects.Balloon getBalloonToFill()
		{
			foreach (SharedObjects.Balloon b in r.balloons.ToList())
				if (b.IsValid && b.UnitsOfWater == 0)
					return b;
			return null;
		}

		public SharedObjects.Umbrella getUmbrellaToRaise()
		{
			foreach (SharedObjects.Umbrella u in r.umbrellas.ToList())
				if (u.IsValid)
					return u;
			return null;
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

		#endregion

		#region Mutators

		public void activate()
		{
			active = true;
		}

		public void setPennyList(List<SharedObjects.Penny> l)
		{
			r.pennies = l;
		}

		public void addBalloon(SharedObjects.Balloon b1)
		{
			foreach (SharedObjects.Balloon b2 in r.balloons.ToList())
			{
				if (b2.Id == b1.Id)
				{
					r.balloons.Remove(b2);
					break;
				}
			}
			r.balloons.Add(b1);
		}

		public void addUmbrella(SharedObjects.Umbrella u)
		{
			r.umbrellas.Add(u);
		}

		public void setID(short i)
		{
			id = i;
		}

		#endregion

		#endregion

	}
}

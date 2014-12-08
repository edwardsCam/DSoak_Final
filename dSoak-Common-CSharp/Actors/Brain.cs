using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Net;

namespace Actors
{
	public class Brain
	{

		#region Private Properties

		private Thread _t;
		private Communicator com;
		private Game active_game;
		private stat status;

		#endregion

		#region Enum

		public enum stat { Uninitialized, Searching, Joining, InGame, Error };

		#endregion

		#region Constructor and Destructor

		public Brain()
		{
			status = stat.Uninitialized;
			if (_t == null)
			{
				_t = new Thread(new ThreadStart(brainLoop));
				_t.Start();
			}
		}

		public void clear()
		{
			if (com != null)
				com.clear();
			if (active_game != null)
				active_game.clear();
			_t.Abort();
			_t = null;
		}

		#endregion

		#region Private Methods

		#region THREAD LOOP

		private void brainLoop()
		{
			short joinAttempts = 0;
			while (true)
			{
				if (status != stat.Uninitialized)
					com.isAlive();
				switch (status)
				{
					case stat.Uninitialized: //uninitialized
						initialize();
						status = stat.Searching;
						break;
					case stat.Searching: //searching for games
						if (joinAttempts++ > 10)
						{
							status = stat.Error;
							joinAttempts = 0;
						}
						if (chooseGame())
						{
							status = stat.Joining;
							joinAttempts = 0;
						}

						break;
					case stat.Joining: //joining game
						if (joinAttempts++ > 10)
						{
							status = stat.Error;
							joinAttempts = 0;
						}
						if (join(active_game))
						{
							status = stat.InGame;
							joinAttempts = 0;
						}
						break;
					case stat.InGame: //ingame
						gameLoop();
						break;
					case stat.Error:
						break;
				}
			}
		}

		#endregion

		#region Setup Methods

		private void initialize()
		{
			com = new Communicator();
			com.setLocalEP(true);
			com.setProcessID();
		}

		private bool chooseGame()
		{
			_Registrar.GameInfo[] games = com.GetGamesList();
			if (games.Count() > 0)
			{
				if (games.Count() > 1)
					games.Reverse();
				foreach (_Registrar.GameInfo i in games)
					if (i.Status == _Registrar.GameInfoStatusCode.Available)
					{
						active_game = new Game(i);
						return true;
					}
			}
			return false;
		}

		private bool join(Game g)
		{
			if (g != null)
			{
				SharedObjects.PlayerInfo player = new SharedObjects.PlayerInfo();
				player.EndPoint = com.setLocalEP(false);
				player.PlayerId = com.getProcessID();
				player.Status = SharedObjects.PlayerInfo.StateCode.OnLine;
				player.AliveTimestamp = DateTime.Now;

				Messages.JoinGame msg = new Messages.JoinGame();
				msg.Player = player;
				msg.GameId = g.getID();

				com.send(new Envelope(msg), true);
				if (com.receive(true))
				{
					active_game.setPennyList(com.returnPennies());
					active_game.activate();
					return true;
				}
			}
			return false;
		}

		#endregion

		#region In-Game Loop

		private void gameLoop()
		{
			while (active_game.isActive())
			{
				com.isAlive();
				active_game.raiseUmbrella();
				//todo
			}
		}

		#endregion

		#endregion

		#region Public Methods

		public void quitGame()
		{
			Messages.LeaveGame msg = new Messages.LeaveGame();
			msg.GameId = active_game.getID();

			com.send(new Envelope(msg), false);
			if (com.receive(false))
				active_game.setMessage(com.returnMessage());
		}

		#region Resource Methods

		public void umbrellaAction()
		{
			if (active_game.hasUmbrella())
			{
				if (!active_game.umbrellaIsRaised())
				{
					Messages.RaiseUmbrella msg = new Messages.RaiseUmbrella();
					msg.Umbrella = active_game.getUmbrella();

					com.send(new Envelope(msg), false);
					if (com.receive(false))
						active_game.setMessage(com.returnMessage());
				}
			}
			else //no umbrella
			{
				Messages.BuyUmbrella msg = new Messages.BuyUmbrella();
				msg.Pennies = active_game.getPennyList();

				com.send(new Envelope(msg), false);
				if (com.receive(false))
					active_game.setUmbrella(com.returnUmbrella());
			}
		}

		public void fillBalloon()
		{
			if (active_game.getBalloon() != null)
			{
				Messages.FillBalloon msg = new Messages.FillBalloon();
				msg.Pennies = active_game.getPennyList();

				com.send(new Envelope(msg), false);
				if (com.receive(false))
					active_game.setBalloon(com.returnBalloon());
			}
		}

		public bool buyBalloon()
		{
			if (active_game.getBalloon() == null)
			{
				Messages.BuyBalloon msg = new Messages.BuyBalloon();
				msg.Pennies = active_game.getPennyList();

				com.send(new Envelope(msg), false);
				if (com.receive(false))
					active_game.setBalloon(com.returnBalloon());
				return true;
			}
			return false;
		}

		public bool throwBalloon(string target_str)
		{
			if (active_game.getBalloon() != null)
			{
				Messages.ThrowBalloon msg = new Messages.ThrowBalloon();
				msg.Balloon = active_game.getBalloon();
				msg.GameId = active_game.getID();
				msg.TargetPlayerId = Convert.ToInt16(target_str);

				com.send(new Envelope(msg), false);
				if (com.receive(false))
				{
					active_game.setMessage(com.returnMessage());
					return true;
				}
				return false;
			}
			else
				return false;
		}

		#endregion

		#region Status Accessors

		public stat getStatus()
		{
			return status;
		}

		public bool isUninitialized()
		{
			return status == stat.Uninitialized;
		}

		public bool isSearching()
		{
			return status == stat.Searching;
		}

		public bool isJoining()
		{
			return status == stat.Joining;
		}

		public bool isInGame()
		{
			return status == stat.InGame;
		}

		public bool isInError()
		{
			return status == stat.Error;
		}

		#endregion

		#region Unit Test Helpers

		public bool hasComms()
		{
			return com != null;
		}

		public bool canGetGames()
		{
			_Registrar.GameInfo[] games = com.GetGamesList();
			return games.Count() > 0;
		}

		public bool hasGame()
		{
			return active_game != null;
		}

		public bool gameIsActive()
		{
			return hasGame() && active_game.isActive();
		}

		public bool hasThread()
		{
			return _t != null;
		}

		#endregion

		#endregion

	}
}

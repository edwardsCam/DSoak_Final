using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActorsTesting
{
	[TestClass]
	public class GameTest
	{
		[TestMethod]
		public void Game_TestEverything()
		{
			Actors.Game game = new Actors.Game();

			Assert.IsFalse(game.isActive());
			Assert.IsFalse(game.hasUmbrellas());
			Assert.IsFalse(game.umbrellaIsRaised());
			Assert.AreEqual(game.pennyCount(), 0);

			game.clear();

			Assert.AreEqual(game.getStatus(), SharedObjects.GameInfo.StatusCode.NotInitialized);

			Actors._Registrar.GameInfo info = new Actors._Registrar.GameInfo();

			#region Building a game from a GameInfo

			#region Properties
			Actors._Registrar.GameInfoStatusCode test_status = Actors._Registrar.GameInfoStatusCode.InProgress;
			Actors._Registrar.PublicEndPoint test_ep = new Actors._Registrar.PublicEndPoint();
			test_ep.HostAndPort = "123.456.789.012:12345";
			short test_id = 5;
			short test_maxPlayers = 32;
			string test_label = "theLabel";
			#endregion

			info.FightManagerEP = test_ep;
			info.GameId = test_id;
			info.Label = test_label;
			info.Status = test_status;
			info.MaxPlayers = test_maxPlayers;
			#endregion

			game = new Actors.Game(info);

			Assert.AreEqual((Actors._Registrar.GameInfoStatusCode)game.getStatus(), test_status);
			Assert.AreEqual(game.getID(), test_id);
			Assert.AreEqual(game.getLabel(), test_label);
			Assert.AreEqual(game.getMaxPlayers(), test_maxPlayers);
			Assert.AreEqual(game.getFightManagerEP().HostAndPort, test_ep.HostAndPort);
		}
	}
}

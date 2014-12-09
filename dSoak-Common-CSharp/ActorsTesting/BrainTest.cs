using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActorsTesting
{
	[TestClass]
	public class BrainTest
	{
		[TestMethod]
		public void Brain_TestEverything()
		{
			Actors.Brain brain = new Actors.Brain();

			Assert.IsTrue(brain.isUninitialized());
			Assert.IsFalse(brain.isSearching());

			Assert.IsFalse(brain.hasComms());
			while (brain.isUninitialized()) ;              //wait for brain to finish initializing
			Assert.IsTrue(brain.hasComms());
			Assert.IsFalse(brain.hasGame());

			while (brain.isSearching()) ;                  //wait to finish searching for games
			Assert.IsTrue(brain.canGetGames());
			Assert.IsTrue(brain.hasGame());

			while (brain.isJoining()) ;                    //join game
			//Assert.IsTrue(brain.gameIsActive());

			while (!brain.isInError()) ;                  //at this stage, brain will error
			Assert.AreEqual(brain.getStatus(), Actors.Brain.stat.Error);

			Assert.IsTrue(brain.hasThread());
			brain.clear();
			Assert.IsFalse(brain.hasThread());
		}
	}
}

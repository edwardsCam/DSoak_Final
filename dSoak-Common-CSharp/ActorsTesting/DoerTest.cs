using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActorsTesting
{
	[TestClass]
	public class DoerTest
	{
		[TestMethod]
		public void Doer_TestEverything()
		{
			Actors.Doer doer = new Actors.Doer(false);
			Assert.IsFalse(doer.hasThread());   //initialized without threads
			Assert.IsFalse(doer.hasConversation());

			Actors.Listener listener = new Actors.Listener();
			Actors.Envelope env = new Actors.Envelope(new Messages.RaiseUmbrella());
			env.setDirection(2);
			listener.addPending(env);

			Assert.IsTrue(doer.hasConversation());
			doer.readRequest();

			doer.clear();
			listener.clear();
		}
	}
}

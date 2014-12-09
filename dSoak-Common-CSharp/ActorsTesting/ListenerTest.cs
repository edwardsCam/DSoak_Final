using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActorsTesting
{
	[TestClass]
	public class ListenerTest
	{
		[TestMethod]
		public void Listener_TestEverything()
		{
			Actors.Listener listener = new Actors.Listener();
			Actors.Envelope envelope = new Actors.Envelope();

			Assert.IsFalse(listener.hasConversation());
			SharedObjects.MessageNumber num = new SharedObjects.MessageNumber();
			envelope.setConvID(num);
			listener.addPending(envelope);
			Assert.IsTrue(listener.hasConversation(num));

			Assert.IsTrue(listener.hasThread());

			listener.clear();
			Assert.IsFalse(listener.hasConversation(num));
			Assert.IsFalse(listener.hasConversation());
			Assert.IsFalse(listener.hasThread());
		}
	}
}

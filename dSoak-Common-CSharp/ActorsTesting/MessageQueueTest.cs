using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActorsTesting
{
	[TestClass]
	public class MessageQueueTest
	{
		[TestMethod]
		public void MessageQueue_TestEverything()
		{

			Actors.MessageQueue q = new Actors.MessageQueue();
			Assert.AreEqual(q.size(), 0);

			Actors.Envelope msg1 = new Actors.Envelope();
			q.push(msg1);
			Assert.AreEqual(q.size(), 1);

			Actors.Envelope msg2 = q.pop();
			Assert.AreEqual(msg1, msg2);
			Assert.AreEqual(q.size(), 0);

			q.push(msg1);
			Assert.AreEqual(q.peek(), msg2);
			Assert.AreEqual(q.size(), 1);
		}
	}
}

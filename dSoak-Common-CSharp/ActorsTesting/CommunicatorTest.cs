using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Net;

namespace ActorsTesting
{
	[TestClass]
	public class CommunicatorTest
	{
		[TestMethod]
		public void Communicator_TestEverything()
		{
			Actors.Communicator com = new Actors.Communicator();
			Assert.IsTrue(com.hasRegistrar());
			Assert.AreEqual(com.getRegistrarURL(), "http://ec2-54-186-119-124.us-west-2.compute.amazonaws.com/Registrar.svc");
			Assert.AreEqual(com.getRegistrarEPReflector(), "ec2-54-186-119-124.us-west-2.compute.amazonaws.com:51999");

			Assert.IsNull(com.getLocalEP());
			com.setLocalEP(true);
			Assert.IsNotNull(com.getLocalEP());

			#region Building the Envelope
			SharedObjects.PlayerInfo player = new SharedObjects.PlayerInfo();
			player.EndPoint = com.getLocalEP();
			player.PlayerId = com.getProcessID();
			player.Status = SharedObjects.PlayerInfo.StateCode.OnLine;

			Messages.JoinGame msg = new Messages.JoinGame();
			msg.Player = player;
			msg.GameId = 2;

			#endregion
			int numSent = com.send(msg);
			int expected = msg.Encode().GetLength(0);

			Assert.AreEqual(numSent, expected);

			short initial = SharedObjects.MessageNumber.LocalProcessId;
			com.setProcessID();
			Assert.AreNotEqual(initial, SharedObjects.MessageNumber.LocalProcessId);

			Assert.IsTrue(com.listenerHasThread());
			com.clear();
			Assert.IsFalse(com.listenerHasThread());
		}
	}
}

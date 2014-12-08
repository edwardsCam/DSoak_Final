using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 * Runs in its own thread.
 * If anything is on the Message Queue, pop it and do the appropriate action.
 * */

namespace Actors
{
	public class Doer : BackgroundThread
	{

		#region Private Properties

		private static bool isInitialized = false;
		private List<SharedObjects.Penny> pennies_to_return;
		private SharedObjects.Balloon balloons_to_return;
		private SharedObjects.Umbrella umbrella_to_return;
		private string message_to_return;

		#endregion

		#region Constructors and Destructor

		public Doer()
		{
			if (!isInitialized)
				initialize(true);
		}

		public Doer(bool threading)
		{
			if (!isInitialized)
				initialize(threading);
		}

		public void clear()
		{
			abort_thread();
			remove_thread();
			conversation_queues = null;
			isInitialized = false;
		}

		#endregion

		#region Private Methods

		#region Initializer

		private void initialize(bool threading)
		{

			if (conversation_queues == null)
				conversation_queues = new ConversationList();

			if (threading)
			{
				create_thread(new ThreadStart(execute));
				start_thread();
			}
			isInitialized = true;
		}

		#endregion

		#region Resource Actions

		private void addPennies(List<SharedObjects.Penny> p)
		{
			pennies_to_return = p;
		}

		private void addBalloon(SharedObjects.Balloon b)
		{
			balloons_to_return = b;
		}

		private void addUmbrella(SharedObjects.Umbrella u)
		{
			umbrella_to_return = u;
		}

		#endregion

		#region THREAD LOOP

		private void execute()
		{
			while (true)
			{
				if (hasConversation())
				{
					Conversation convo = conversation_queues.peekReceived();
					if (convo != null)
					{
						Envelope request = convo.peek();
						if (request != null)
						{
							Messages.Message msg = request.getPayload();
							switch (msg.getTypeAsString())
							{
								case "GameJoined":
									Messages.GameJoined joined = msg as Messages.GameJoined;
									addPennies(joined.Pennies);
									break;

								case "UmbrellaPurchased":
									Messages.UmbrellaPurchased purchased = msg as Messages.UmbrellaPurchased;
									addUmbrella(purchased.Umbrella);
									break;

								case "BalloonPurchased":
									Messages.BalloonPurchased balloon = msg as Messages.BalloonPurchased;
									addBalloon(balloon.Balloon);
									break;

								case "Ack":
									message_to_return = "Ack";
									break;

								case "Nak":
									Messages.Nak nak = msg as Messages.Nak;
									message_to_return = nak.Error;
									break;
							}
						}
					}
				}
			}
		}

		#endregion

		#endregion

		#region Public Methods

		#region Return methods

		public List<SharedObjects.Penny> returnPennies()
		{
			return pennies_to_return;
		}

		public SharedObjects.Balloon returnBalloon()
		{
			return balloons_to_return;
		}

		public SharedObjects.Umbrella returnUmbrella()
		{
			return umbrella_to_return;
		}

		public string returnMessage()
		{
			return message_to_return;
		}

		#endregion

		#endregion

	}
}

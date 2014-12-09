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
		private string message_to_return;
		private bool hasResourceToReturn;

		private Messages.Message return_message;

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
			hasResourceToReturn = false;
			return_message = null;
		}

		#endregion

		#region THREAD LOOP

		private void execute()
		{
			while (true)
			{
				if (new_flag)
				{
					Conversation new_convo = conversation_queues.peek();
					if (new_convo != null)
					{
						Envelope request = new_convo.peek();
						if (request != null)
						{
							Messages.Message msg = request.getPayload();
							switch (msg.getTypeAsString())
							{
								case "AliveQuery":
									{
										Messages.ProcessSummary summary = new Messages.ProcessSummary();
										summary.Data = Brain.getProcessData();
										return_message = summary;
										hasResourceToReturn = true;
									}
									break;
								case "GameJoined":
									{
										return_message = msg as Messages.GameJoined;
										hasResourceToReturn = true;
									}
									break;

								case "UmbrellaPurchased":
									{
										Messages.UmbrellaPurchased purchased = msg as Messages.UmbrellaPurchased;
										//addUmbrella(purchased.Umbrella); //todo
									}
									break;

								case "BalloonPurchased":
									{
										return_message = msg as Messages.BalloonPurchased;
										hasResourceToReturn = true;
									}
									break;

								case "Ack":
									{
										message_to_return = "Ack";
									}
									break;

								case "Nak":
									{
										Messages.Nak nak = msg as Messages.Nak;
										message_to_return = nak.Error;
									}
									break;
							}
							new_flag = false;
						}
					}
				}
			}
		}

		#endregion

		private bool waitForResource()
		{
			short count = 0;
			while (!hasResourceToReturn)
			{
				if (count++ > 100)
					return false;
			}
			hasResourceToReturn = false;
			return true;
		}

		#endregion

		#region Public Methods

		#region Return methods

		public Messages.GameJoined gotGameJoinedMsg()
		{
			if (waitForResource())
				return return_message as Messages.GameJoined;
			return null;
		}

		public Messages.BalloonPurchased gotBalloonPurchasedMsg()
		{
			if (waitForResource())
				return return_message as Messages.BalloonPurchased;
			return null;
		}

		public Messages.ProcessSummary gotAlivePingRequest()
		{
			if (waitForResource())
				return return_message as Messages.ProcessSummary;
			return null;
		}

		#endregion

		#endregion

	}
}

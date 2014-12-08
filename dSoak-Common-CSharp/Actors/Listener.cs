using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 * Runs in its own thread.
 * Handles placing messages onto the queue
 * */

namespace Actors
{
	public class Listener : BackgroundThread
	{

		#region Private Properties

		private static bool isInitialized = false;
		private MessageQueue pendingMessages;
		private bool active;

		#endregion

		#region Constructors and Destructor

		public Listener()
		{
			if (!isInitialized)
				initialize(true);
		}

		public Listener(bool threading)
		{
			if (!isInitialized)
				initialize(threading);
		}

		public void clear()
		{
			abort_thread();
			remove_thread();
			conversation_queues = null;
			pendingMessages = null;
			isInitialized = false;
		}

		#endregion

		#region Private Methods

		#region Initializer

		private void initialize(bool threading)
		{
			if (conversation_queues == null)
				conversation_queues = new ConversationList();
			pendingMessages = new MessageQueue();
			if (threading)
			{
				create_thread(new ThreadStart(listen));
				start_thread();
			}
			isInitialized = true;
			active = false;
		}

		#endregion

		#region THREAD LOOP

		private void listen()
		{
			while (true)
			{
				if (active)
				{
					if (pendingMessages.size() > 0)
					{
						Envelope env = pendingMessages.pop();
						if (env.hasPayload())
						{
							conversation_queues.add(env);
							active = false;
							new_flag = true;
						}
					}
				}
			}
		}

		#endregion

		#endregion

		#region Public Methods

		public void addPending(Envelope msg)
		{
			if (msg.hasPayload())
			{
				pendingMessages.push(msg);
				active = true;
			}
		}

		#endregion

	}
}

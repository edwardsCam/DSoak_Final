﻿using System;
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
			q_request = null;
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

			if (q_request == null)
				q_request = new MessageQueue();

			pendingMessages = new MessageQueue();

			if (threading)
			{
				create_thread(new ThreadStart(listen));
				start_thread();
			}
			isInitialized = true;
		}

		#endregion

		#region Queue stuff

		private bool hasPendingRequests()
		{
			if (pendingMessages.size() > 0)
			{
				Messages.Message msg = pendingMessages.peek().getPayload();
				return msg.ConvId == msg.MessageNr;
			}
			return false;
		}

		private bool hasPendingConversation()
		{
			if (pendingMessages.size() > 0)
			{
				Messages.Message msg = pendingMessages.peek().getPayload();
				return msg.ConvId != msg.MessageNr;
			}
			return false;
		}

		#endregion

		#endregion

		#region Public Methods

		#region Queue stuff

		public void addConversation(Envelope msg)
		{
			if (msg.hasPayload())
				conversation_queues.add(msg);
		}

		public void addPending(Envelope msg)
		{
			if (msg.hasPayload())
				pendingMessages.push(msg);
		}

		#endregion

		#region THREAD LOOP

		public void listen()
		{
			while (true)
			{
				if (hasPendingRequests())
				{
					Envelope env = pendingMessages.pop();
					q_request.push(env);
				}

				if (hasPendingConversation())
				{
					Envelope env = pendingMessages.pop();
					conversation_queues.add(pendingMessages.pop());
				}
			}
		}

		#endregion

		#endregion

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 * Wrapper for Listener and Doer.
 * Runs in the background and operates on MessageQueues
 * */

namespace Actors
{
	public abstract class BackgroundThread
	{

		#region Private and Protected Properties

		private Thread _t;
		protected static MessageQueue q_request;
		protected static Dictionary<SharedObjects.MessageNumber, MessageQueue> conversation_queues;

		#endregion

		#region Public Methods

		#region Thread Methods

		public void start_thread()
		{
			if (_t != null)
				_t.Start();
		}

		public void abort_thread()
		{
			if (_t != null)
				_t.Abort();
		}

		public void create_thread(ThreadStart s)
		{
			if (_t == null)
				_t = new Thread(s);
		}

		public void remove_thread()
		{
			_t = null;
		}

		#endregion

		#region Queue stuff

		public bool hasRequests()
		{
			return q_request != null && q_request.size() > 0;
		}

		public bool hasConversation()
		{
			if (conversation_queues != null)
			{
				foreach (KeyValuePair<SharedObjects.MessageNumber, MessageQueue> entry in conversation_queues)
				{
					if (entry.Value.size() > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool hasConversation(SharedObjects.MessageNumber convID)
		{
			return conversation_queues != null &&
				conversation_queues[convID] != null &&
				conversation_queues[convID].size() > 0;
		}

		#endregion

		#region Unit Test Helpers

		public bool hasThread()
		{
			return _t != null;
		}

		#endregion

		#endregion

	}
}

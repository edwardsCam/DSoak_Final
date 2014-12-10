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
		protected static ConversationList conversation_queues;
		protected static bool new_flag;
		protected static bool listener_on;

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

		public bool hasConversation()
		{
			return conversation_queues != null && 
				conversation_queues.hasConvos();
		}

		public bool hasConversation(SharedObjects.MessageNumber convID)
		{
			return conversation_queues != null && 
				conversation_queues.hasConvo(convID);
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

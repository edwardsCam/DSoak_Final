using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actors
{
	public class Conversation
	{
		#region Private Properties

		private SharedObjects.MessageNumber id;
		private MessageQueue msgs;

		#endregion

		#region Constructor

		public Conversation()
		{
			id = new SharedObjects.MessageNumber();
			msgs = new MessageQueue();
		}

		public Conversation(SharedObjects.MessageNumber n)
		{
			id = n;
			msgs = new MessageQueue();
		}

		public Conversation(Envelope e)
		{
			id = e.getPayload().ConvId;
			msgs = new MessageQueue();
			msgs.push(e);
		}

		#endregion

		#region Public Methods


		public bool hasMsg()
		{
			return msgs.size() > 0;
		}

		#endregion

		#region Accessors and Mutators

		public SharedObjects.MessageNumber getID()
		{
			return id;
		}

		public Envelope peek()
		{
			return msgs.peek();
		}

		public Envelope pop()
		{
			return msgs.pop();
		}

		public void setID(SharedObjects.MessageNumber n)
		{
			id = n;
		}

		public void push(Envelope m)
		{
			msgs.push(m);
		}

		public void setQueue(MessageQueue q)
		{
			msgs = q;
		}
		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 * ConversationList is an abstraction that contains all the conversations
 * */

namespace Actors
{
	public class ConversationList
	{
		#region Private Properties

		private List<Conversation> convos;
		private readonly ReaderWriterLock _locker = new ReaderWriterLock();

		#endregion

		#region Constructor

		public ConversationList()
		{
			convos = new List<Conversation>();
		}

		#endregion

		#region Public Methods

		public bool hasConvos()
		{
			lock (_locker)
			{
				foreach (Conversation c in convos.ToList())
					if (c.hasMsg())
						return true;
			}
			return false;
		}

		public bool hasConvo(SharedObjects.MessageNumber n)
		{
			foreach (Conversation c in convos.ToList())
				if (c.getID() == n)
					return true;
			return false;
		}

		public void add(Envelope e)
		{
			SharedObjects.MessageNumber convID = e.getPayload().ConvId;
			if (hasConvo(convID))
			{
				lock (_locker)
				{
					foreach (Conversation c in convos.ToList())
					{
						if (c.getID() == convID)
						{
							c.push(e);
							c.setUnchecked();
						}
					}
				}
			}
			else
				convos.Add(new Conversation(e));
		}

		public Conversation peek()
		{
			lock (_locker)
			{
				foreach (Conversation c in convos.ToList())
				{
					Envelope e = c.peek();
					if (e.isIncoming() && !c.isChecked())
					{
						c.setChecked();
						return c;
					}
				}
			}
			return null;
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actors
{
	public class ConversationList
	{


		#region Private Properties

		private List<Conversation> convos;

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
			if (convos.Count > 0)
				foreach (Conversation c in convos)
					if (c.hasMsg())
						return true;
			return false;
		}

		public bool hasConvo(SharedObjects.MessageNumber n)
		{
			foreach (Conversation c in convos)
				if (c.getID() == n)
					return true;
			return false;
		}

		public void add(Envelope e)
		{
			SharedObjects.MessageNumber convID = e.getPayload().ConvId;
			if (hasConvo(convID))
			{
				foreach (Conversation c in convos)
				{
					if (c.getID() == convID)
					{
						c.push(e);
						c.setUnchecked();
					}
				}
			}
			else
				convos.Add(new Conversation(e));
		}

		#endregion

		#region Accessors

		public Conversation pop()
		{
			Conversation ret = convos.ElementAt(0);
			convos.Remove(ret);
			return ret;
		}

		public Conversation peek()
		{
			foreach (Conversation c in convos)
				if (!c.isChecked())			
					return c;			
			return null;
		}

		public Conversation peekReceived()
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
			return null;
		}

		#endregion
	}
}

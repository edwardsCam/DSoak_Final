using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 * Threadsafe queue of Envelopes
 * */

namespace Actors
{
	public class MessageQueue
	{

		#region Private Properties

		private readonly ReaderWriterLock _locker = new ReaderWriterLock();
		private Queue<Envelope> messages;

		#endregion

		#region Constructor

		public MessageQueue()
		{
			messages = new Queue<Envelope>();
		}

		#endregion

		#region Public Methods

		#region Accessors

		public int size()
		{
			return messages.Count;
		}

		#endregion

		#region Queue operations

		public void push(Envelope msg)
		{
			lock (_locker)
			{
				messages.Enqueue(msg);
			}
		}

		public Envelope pop()
		{
			lock (_locker)
			{
				if (messages.Count == 0)
					return null;
				else
					return messages.Dequeue();
			}
		}

		public Envelope peek()
		{
			lock (_locker)
			{
				return messages.ToList().Last();
			}
		}

		public List<Envelope> asList()
		{
			return messages.ToList();
		}

		#endregion

		#endregion

	}
}

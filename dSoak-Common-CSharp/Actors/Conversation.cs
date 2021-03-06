﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Conversation is an abstraction of a MessageQueue, that also has an id.
 * */

namespace Actors
{
	public class Conversation
	{
		#region Private Properties

		private SharedObjects.MessageNumber id;
		private MessageQueue msgs;
		private bool allChecked;

		#endregion

		#region Constructor

		public Conversation()
		{
			id = new SharedObjects.MessageNumber();
			msgs = new MessageQueue();
			allChecked = true;
		}

		public Conversation(SharedObjects.MessageNumber n)
		{
			id = n;
			msgs = new MessageQueue();
			allChecked = true;
		}

		public Conversation(Envelope e)
		{
			id = e.getPayload().ConvId;
			msgs = new MessageQueue();
			msgs.push(e);
			allChecked = false;
		}

		#endregion

		#region Public Methods

		public bool hasMsg()
		{
			return msgs.size() > 0;
		}

		public bool isChecked()
		{
			return allChecked;
		}

		public void setUnchecked()
		{
			allChecked = false;
		}

		public void setChecked()
		{
			allChecked = true;
		}

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

		public void push(Envelope m)
		{
			msgs.push(m);
		}

		#endregion

		#endregion
	}
}

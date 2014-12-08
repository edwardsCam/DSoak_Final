using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actors
{
	public class Envelope
	{

		#region Private Properties

		private Messages.Message payload;
		private SharedObjects.PublicEndPoint ep;
		private short direction;

		#endregion

		#region Constructors

		public Envelope()
		{
			payload = new Messages.Message();
			ep = null;
			direction = 1;
		}

		public Envelope(Messages.Message msg)
		{
			payload = msg;
			ep = null;
			direction = 1;
		}

		public Envelope(Messages.Message msg, SharedObjects.PublicEndPoint p)
		{
			payload = msg;
			ep = p;
			direction = 1;
		}

		#endregion

		#region Public Methods

		#region Accessors

		public Messages.Message getPayload()
		{
			return payload;
		}

		public SharedObjects.PublicEndPoint getEP()
		{
			return ep;
		}

		public bool hasPayload()
		{
			return payload != null;
		}

		public bool hasEP()
		{
			return ep != null;
		}

		public bool isOutgoing()
		{
			return direction == 1;
		}

		public bool isIncoming()
		{
			return direction == 2;
		}

		public void setDirection(short d)
		{
			direction = d;
		}

		#endregion

		#region Mutators

		public void setEP(SharedObjects.PublicEndPoint val)
		{
			ep = val;
		}

		#endregion

		#region Packing / Unpacking

		public byte[] encode()
		{
			return payload.Encode();
		}

		public static Envelope unpack(byte[] stream)
		{
			try
			{
				Messages.Message msg = Messages.Message.Decode(stream);
				Envelope ret = new Envelope(msg);
				ret.setDirection(2);
				return ret;
			}
			catch (System.Collections.Generic.KeyNotFoundException)
			{
				var IPstr = System.Text.Encoding.Default.GetString(stream);
				SharedObjects.PublicEndPoint publicEP = new SharedObjects.PublicEndPoint(IPstr);
				Envelope ret = new Envelope(null, publicEP);
				ret.setDirection(2);
				return ret;
			}
			catch (Exception)
			{
				return null;
			}
		}

		#endregion

		#endregion

	}
}

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

		#endregion

		#region Constructors

		public Envelope()
		{
			payload = new Messages.Message();
			ep = null;
		}

		public Envelope(Messages.Message msg)
		{
			payload = msg;
			ep = null;
		}

		public Envelope(Messages.Message msg, SharedObjects.PublicEndPoint p)
		{
			payload = msg;
			ep = p;
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

		#endregion

		#region Mutators

		public void setPayload(Messages.Message msg)
		{
			payload = msg;
		}

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
				return new Envelope(msg);
			}
			catch (System.Collections.Generic.KeyNotFoundException)
			{
				var IPstr = System.Text.Encoding.Default.GetString(stream);
				SharedObjects.PublicEndPoint publicEP = new SharedObjects.PublicEndPoint(IPstr);
				return new Envelope(null, publicEP);
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

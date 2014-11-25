using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

/*
 * Primary interface with external processes.
 * Has a Doer and a Listener
 * Uses UDPSockets
 * */

namespace Actors
{
	public class Communicator
	{

		#region Private Properties

		private _Registrar.Registrar myRegistrar;
		private SharedObjects.PublicEndPoint remoteEP, localEP;
		private UdpClient client;
		private Listener listener;
		private Doer doer;

		#endregion

		#region Constructor and Destructor

		public Communicator()
		{
			client = new UdpClient();
			client.Client.ReceiveTimeout = 10000;

			listener = new Listener();
			doer = new Doer();

			myRegistrar = new _Registrar.Registrar();
			string EPReflector = myRegistrar.EndPointReflector();
			remoteEP = new SharedObjects.PublicEndPoint(EPReflector);
			localEP = null;
		}

		public void clear()
		{
			client.Close();
			listener.clear();
			doer.clear();

			myRegistrar.Abort();
		}

		#endregion

		#region Public Methods

		#region Webservice stuff

		public SharedObjects.PublicEndPoint setLocalEP(bool generate)
		{
			if (generate)
			{
				send(new Envelope(new Messages.AliveQuery()));
				localEP = receiveAsEnvelope().getEP();
			}
			return localEP;
		}

		public void setRemoteEP(SharedObjects.PublicEndPoint s)
		{
			remoteEP = s;
		}

		public void setProcessID()
		{
			if (localEP != null)
			{
				short id;
				bool specified;
				_Registrar.PublicEndPoint ep = new _Registrar.PublicEndPoint();
				ep.HostAndPort = localEP.HostAndPort;
				myRegistrar.GetProcessId(ep, "A01982846", _Registrar.RegistryEntryProcessType.Player, true, out id, out specified);
				if (specified)
					SharedObjects.MessageNumber.LocalProcessId = id;
			}
		}

		#endregion

		#region UDP Client stuff

		public int send(Envelope msg)
		{
			try
			{
				if (msg.hasEP())
					client.Connect(msg.getEP().IPEndPoint);
				else
					client.Connect(remoteEP.IPEndPoint);

				byte[] datagram = msg.encode();
				return client.Send(datagram, datagram.Length);
			}
			catch (Exception)
			{
				return -1;
			}
		}

		public int send(Messages.Message msg)
		{
			return send(new Envelope(msg));
		}

		public bool receive()
		{
			byte[] streamBack;
			try
			{
				IPEndPoint server = remoteEP.IPEndPoint;
				streamBack = client.Receive(ref server);

				Envelope response = Envelope.unpack(streamBack);
				listener.addPending(response);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public Envelope receiveAsEnvelope()
		{
			try
			{
				IPEndPoint server = remoteEP.IPEndPoint;
				byte[] streamBack = client.Receive(ref server);
				return Envelope.unpack(streamBack);
			}
			catch (Exception)
			{
				return null;
			}
		}

		#endregion

		#region Queue stuff

		public bool hasConversation()
		{
			return doer.hasConversation();
		}

		public bool hasRequests()
		{
			return doer.hasRequests();
		}

		#endregion

		#region Resouce Getters

		public List<SharedObjects.Penny> returnPennies()
		{
			return doer.returnPennies();
		}

		public SharedObjects.Umbrella returnUmbrella()
		{
			return doer.returnUmbrella();
		}

		public SharedObjects.Balloon returnBalloon()
		{
			return doer.returnBalloon();
		}

		public string returnMessage()
		{
			return doer.returnMessage();
		}

		#endregion

		#region Accessors

		public _Registrar.GameInfo[] GetGamesList()
		{
			return myRegistrar.GetGames(_Registrar.GameInfoStatusCode.Available, true);
		}

		public SharedObjects.PublicEndPoint getLocalEP()
		{
			return localEP;
		}

		public SharedObjects.PublicEndPoint getRemoteEP()
		{
			return remoteEP;
		}

		#endregion

		#region Unit Test Helpers

		public bool hasRegistrar()
		{
			return myRegistrar != null;
		}

		public bool doerHasThread()
		{
			return doer.hasThread();
		}

		public bool listenerHasThread()
		{
			return listener.hasThread();
		}

		public string getRegistrarURL()
		{
			return myRegistrar.Url;
		}

		public string getRegistrarEPReflector()
		{
			return myRegistrar.EndPointReflector();
		}

		#endregion

		#endregion
	}
}

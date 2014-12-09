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
		private SharedObjects.PublicEndPoint gameManagerEP, localEP, registrarEP;
		private UdpClient client;
		private Listener listener;
		private short processID;

		#endregion

		#region Constructor and Destructor

		public Communicator()
		{
			client = new UdpClient();
			client.Client.ReceiveTimeout = 1500;

			listener = new Listener();

			myRegistrar = new _Registrar.Registrar();
			string EPReflector = myRegistrar.EndPointReflector();
			registrarEP = new SharedObjects.PublicEndPoint(EPReflector);
			_Registrar.RegistryEntry[] managers = myRegistrar.GetGameManagers();

			if (managers.Count() > 0)
				gameManagerEP = new SharedObjects.PublicEndPoint(managers.Last().Ep.HostAndPort);
			localEP = null;
		}

		public void clear()
		{
			client.Close();
			listener.clear();
			myRegistrar.Abort();
		}

		#endregion

		#region Public Methods

		#region Webservice stuff

		public SharedObjects.PublicEndPoint setLocalEP(bool generate)
		{
			if (generate)
			{
				sendToRegistrar(new Envelope(new Messages.AliveQuery()));
				localEP = receiveFromRegistrar();
			}
			return localEP;
		}

		public void setProcessID()
		{
			if (localEP != null)
			{
				bool specified;
				_Registrar.PublicEndPoint ep = new _Registrar.PublicEndPoint();
				ep.HostAndPort = localEP.HostAndPort;
				myRegistrar.GetProcessId(ep, "A01982846", _Registrar.RegistryEntryProcessType.Player, true, out processID, out specified);
				if (specified)
					SharedObjects.MessageNumber.LocalProcessId = processID;
			}
		}

		public void isAlive()
		{
			myRegistrar.AmAlive(processID, true);
		}

		public short getProcessID()
		{
			return processID;
		}

		#endregion

		#region UDP Client stuff

		public int send(Messages.Message payload)
		{
			Envelope msg = new Envelope(payload);
			try
			{
				client.Connect(gameManagerEP.IPEndPoint);
				listener.addPending(msg);
				byte[] datagram = msg.encode();
				return client.Send(datagram, datagram.Length);
			}
			catch (Exception)
			{
				return -1;
			}
		}

		public void sendToRegistrar(Envelope msg)
		{
			try
			{
				client.Connect(registrarEP.IPEndPoint);
				byte[] datagram = msg.encode();
				client.Send(datagram, datagram.Length);
			}
			catch (Exception)
			{
				return;
			}
		}

		public bool receive(string type)
		{
			try
			{
				IPEndPoint server = gameManagerEP.IPEndPoint;
				Envelope response = Envelope.unpack(client.Receive(ref server));
				if (response != null && response.hasPayload())
				{
					listener.addPending(response);
					if (response.getPayload().getTypeAsString() == type)
						return true;
				}
				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public SharedObjects.PublicEndPoint receiveFromRegistrar()
		{
			try
			{
				IPEndPoint server = registrarEP.IPEndPoint;
				byte[] streamBack = client.Receive(ref server);
				return Envelope.unpack(streamBack).getEP();
			}
			catch (Exception)
			{
				return null;
			}
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

		#endregion

		#region Unit Test Helpers

		public bool hasRegistrar()
		{
			return myRegistrar != null;
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

		public SharedObjects.PublicEndPoint getGameManagerEP()
		{
			return gameManagerEP;
		}

		#endregion

		#endregion
	}
}

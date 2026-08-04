using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UCS.Core;
using UCS.PacketProcessing;

namespace UCS.Network
{
	// Token: 0x02000022 RID: 34
	internal class Gateway
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000BDCB File Offset: 0x00009FCB
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000BDD2 File Offset: 0x00009FD2
		public static Socket Socket { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000BDDC File Offset: 0x00009FDC
		public IPAddress IP
		{
			get
			{
				if (this.ip == null)
				{
					this.ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where((IPAddress entry) => entry.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault<IPAddress>();
				}
				return this.ip;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000BE38 File Offset: 0x0000A038
		public bool Host(int port)
		{
			Gateway.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				Gateway.Socket.Bind(new IPEndPoint(IPAddress.Any, port));
				Gateway.Socket.Listen(30);
				Gateway.Socket.BeginAccept(new AsyncCallback(this.OnClientConnect), Gateway.Socket);
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Concat(new object[] { "[CRS]    Exception when attempting to host (", port, "): ", ex }));
				Gateway.Socket = null;
				return false;
			}
			return true;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public void Start()
		{
			if (this.Host(9339))
			{
				Console.WriteLine("[CRS]    Gateway started on port " + 9339);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000BF04 File Offset: 0x0000A104
		private static void Disconnect()
		{
			if (Gateway.Socket != null)
			{
				Gateway.Socket.BeginDisconnect(false, new AsyncCallback(Gateway.OnEndHostComplete), Gateway.Socket);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000BF2C File Offset: 0x0000A12C
		private void OnClientConnect(IAsyncResult result)
		{
			try
			{
				Socket socket = Gateway.Socket.EndAccept(result);
				ResourcesManager.AddClient(new Client(socket), ((IPEndPoint)socket.RemoteEndPoint).Address.ToString());
				SocketRead.Begin(socket, new SocketRead.IncomingReadHandler(Gateway.OnReceive), new SocketRead.IncomingReadErrorHandler(Gateway.OnReceiveError));
				string text = new WebClient().DownloadString("http://ipinfo.io/" + ((IPEndPoint)socket.RemoteEndPoint).Address + "/country").Trim();
				Console.WriteLine(string.Concat(new object[]
				{
					"[CRS]    Client connected (",
					((IPEndPoint)socket.RemoteEndPoint).Address,
					", ",
					text,
					")"
				}));
			}
			catch (Exception ex)
			{
				Console.WriteLine("[CRS]    Exception when accepting incoming connection: " + ex);
			}
			try
			{
				Gateway.Socket.BeginAccept(new AsyncCallback(this.OnClientConnect), Gateway.Socket);
			}
			catch (Exception ex2)
			{
				Console.WriteLine("[CRS]    Exception when starting new accept process: " + ex2);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000C058 File Offset: 0x0000A258
		private static void OnEndHostComplete(IAsyncResult result)
		{
			Gateway.Socket = null;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000C060 File Offset: 0x0000A260
		private static void OnReceive(SocketRead read, byte[] data)
		{
			try
			{
				Client client = ResourcesManager.GetClient(read.Socket.Handle.ToInt64());
				client.DataStream.AddRange(data);
				Message message;
				while (client.TryGetPacket(out message))
				{
					PacketManager.ProcessIncomingPacket(message);
				}
			}
			catch (Exception ex)
			{
				Debugger.WriteLine("[CRS]    Exception thrown when processing incoming packet : ", ex, 4);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		private static void OnReceiveError(SocketRead read, Exception exception)
		{
		}

		// Token: 0x04000150 RID: 336
		private const int kHostConnectionBacklog = 30;

		// Token: 0x04000151 RID: 337
		private const int kPort = 9339;

		// Token: 0x04000152 RID: 338
		private IPAddress ip;
	}
}

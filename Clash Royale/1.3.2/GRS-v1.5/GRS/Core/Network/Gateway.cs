using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using CRS.PacketProcessing;

namespace CRS.Core.Network
{
	// Token: 0x020000E0 RID: 224
	internal class Gateway
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x0002027C File Offset: 0x0001E47C
		public Gateway()
		{
			Gateway._S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			Gateway._S.Bind(new IPEndPoint(IPAddress.Any, 9339));
			Gateway._S.Listen(100);
			for (;;)
			{
				Gateway._E.Reset();
				Gateway._S.BeginAccept(new AsyncCallback(this.Connection), Gateway._S);
				Gateway._E.WaitOne();
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000202F4 File Offset: 0x0001E4F4
		private void Connection(IAsyncResult result)
		{
			try
			{
				Socket socket = Gateway._S.EndAccept(result);
				if (socket.IsBound)
				{
					Console.WriteLine("[GRS]    New player -> " + ((IPEndPoint)socket.RemoteEndPoint).Address);
					ResourcesManager.AddClient(new Device(socket));
					SocketRead.Begin(socket, new SocketRead.IncomingReadHandler(this.ProcessPacket));
					Gateway._S.BeginAccept(new AsyncCallback(this.Connection), Gateway._S);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00020384 File Offset: 0x0001E584
		private void ProcessPacket(SocketRead read, byte[] data)
		{
			try
			{
				Device client = ResourcesManager.GetClient(read._socket.Handle.ToInt64());
				client.DataStream.AddRange(data);
				Message message;
				while (client.TryGetPacket(out message))
				{
					PacketManager.ProcessIncomingPacket(message);
				}
			}
			catch (Exception)
			{
				read._socket.Close();
			}
		}

		// Token: 0x040003D6 RID: 982
		public static Socket _S;

		// Token: 0x040003D7 RID: 983
		public static ManualResetEvent _E = new ManualResetEvent(false);
	}
}

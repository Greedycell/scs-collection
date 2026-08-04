using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using CRS.PacketProcessing;

namespace CRS.Core.Network
{
	// Token: 0x020000E1 RID: 225
	internal class PacketManager : IDisposable
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x000203F8 File Offset: 0x0001E5F8
		public PacketManager()
		{
			PacketManager.IncomingProcessingDelegate incomingProcessingDelegate = new PacketManager.IncomingProcessingDelegate(PacketManager.IncomingProcessing);
			PacketManager.OutgoingProcessingDelegate outgoingProcessingDelegate = new PacketManager.OutgoingProcessingDelegate(PacketManager.OutgoingProcessing);
			incomingProcessingDelegate.BeginInvoke(null, null);
			outgoingProcessingDelegate.BeginInvoke(null, null);
			Console.WriteLine("[GRS]    Packet Manager started successfully");
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0002043F File Offset: 0x0001E63F
		public static void ProcessIncomingPacket(Message p)
		{
			PacketManager.m_vIncomingPackets.Add(p);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0002044C File Offset: 0x0001E64C
		public static void Send(Message p)
		{
			try
			{
				p.Encode();
				p.Process(p.Client.GetLevel());
				PacketManager.m_vOutgoingPackets.Add(p);
			}
			catch (Exception e)
			{
				Console.WriteLine("[GRS]    Error when sending a packet");
				Console.WriteLine(e);
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0002049C File Offset: 0x0001E69C
		public void Dispose()
		{
			PacketManager.m_vIncomingPackets.Dispose();
			PacketManager.m_vOutgoingPackets.Dispose();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000204B4 File Offset: 0x0001E6B4
		private static void IncomingProcessing()
		{
			for (;;)
			{
				Message message = PacketManager.m_vIncomingPackets.Take();
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					Message message2 = (Message)state;
					message2.Decrypt();
					MessageManager.ProcessPacket(message2);
				}, message);
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000204F4 File Offset: 0x0001E6F4
		private static void OutgoingProcessing()
		{
			for (;;)
			{
				Message message = PacketManager.m_vOutgoingPackets.Take();
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					Message message2 = (Message)state;
					try
					{
						Socket socket = message2.Client.Socket;
						if (socket != null)
						{
							socket.Send(message2.GetRawData());
						}
					}
					catch (Exception)
					{
						try
						{
							ResourcesManager.DropClient(message2.Client.GetSocketHandle());
						}
						catch (Exception)
						{
							Console.WriteLine("[GRS] Error when disconnecting the client");
						}
					}
				}, message);
			}
		}

		// Token: 0x040003D8 RID: 984
		private static readonly BlockingCollection<Message> m_vIncomingPackets = new BlockingCollection<Message>();

		// Token: 0x040003D9 RID: 985
		private static readonly BlockingCollection<Message> m_vOutgoingPackets = new BlockingCollection<Message>();

		// Token: 0x02000112 RID: 274
		// (Invoke) Token: 0x06000655 RID: 1621
		private delegate void IncomingProcessingDelegate();

		// Token: 0x02000113 RID: 275
		// (Invoke) Token: 0x06000659 RID: 1625
		private delegate void OutgoingProcessingDelegate();
	}
}

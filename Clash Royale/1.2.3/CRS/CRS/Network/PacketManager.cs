using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using UCS.Core;
using UCS.PacketProcessing;

namespace UCS.Network
{
	// Token: 0x02000023 RID: 35
	internal class PacketManager : IDisposable
	{
		// Token: 0x06000153 RID: 339 RVA: 0x0000C0CA File Offset: 0x0000A2CA
		public PacketManager()
		{
			PacketManager.m_vIncomingPackets = new ConcurrentQueue<Message>();
			PacketManager.m_vOutgoingPackets = new ConcurrentQueue<Message>();
			this.m_vIsRunning = false;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000C0ED File Offset: 0x0000A2ED
		public void Dispose()
		{
			PacketManager.m_vIncomingWaitHandle.Dispose();
			GC.SuppressFinalize(this);
			PacketManager.m_vOutgoingWaitHandle.Dispose();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000C109 File Offset: 0x0000A309
		public static void ProcessIncomingPacket(Message p)
		{
			PacketManager.m_vIncomingPackets.Enqueue(p);
			PacketManager.m_vIncomingWaitHandle.Set();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000C124 File Offset: 0x0000A324
		public static void ProcessOutgoingPacket(Message p)
		{
			p.Encode();
			try
			{
				PacketManager.m_vOutgoingPackets.Enqueue(p);
				PacketManager.m_vOutgoingWaitHandle.Set();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000C164 File Offset: 0x0000A364
		public void Start()
		{
			new PacketManager.IncomingProcessingDelegate(this.IncomingProcessing).BeginInvoke(null, null);
			new PacketManager.OutgoingProcessingDelegate(this.OutgoingProcessing).BeginInvoke(null, null);
			this.m_vIsRunning = true;
			Console.WriteLine("[CRS]    Packet Manager started successfully");
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
		private void IncomingProcessing()
		{
			while (this.m_vIsRunning)
			{
				PacketManager.m_vIncomingWaitHandle.WaitOne();
				Message message;
				while (PacketManager.m_vIncomingPackets.TryDequeue(out message))
				{
					message.GetData();
					Logger.WriteLine(message, "R", 4);
					MessageManager.ProcessPacket(message);
				}
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		private void OutgoingProcessing()
		{
			while (this.m_vIsRunning)
			{
				PacketManager.m_vOutgoingWaitHandle.WaitOne();
				Message message;
				while (PacketManager.m_vOutgoingPackets.TryDequeue(out message))
				{
					Logger.WriteLine(message, "S", 4);
					try
					{
						if (message.Client.Socket != null)
						{
							message.Client.Socket.Send(message.GetRawData());
						}
						else
						{
							ResourcesManager.DropClient(message.Client.GetSocketHandle());
						}
					}
					catch (Exception)
					{
						try
						{
							ResourcesManager.DropClient(message.Client.GetSocketHandle());
							message.Client.Socket.Shutdown(SocketShutdown.Both);
							message.Client.Socket.Close();
						}
						catch (Exception ex)
						{
							Debugger.WriteLine("[CRS]    Exception thrown when dropping client : ", ex, 4);
						}
					}
				}
			}
		}

		// Token: 0x04000154 RID: 340
		private static readonly EventWaitHandle m_vIncomingWaitHandle = new AutoResetEvent(false);

		// Token: 0x04000155 RID: 341
		private static readonly EventWaitHandle m_vOutgoingWaitHandle = new AutoResetEvent(false);

		// Token: 0x04000156 RID: 342
		private static ConcurrentQueue<Message> m_vIncomingPackets;

		// Token: 0x04000157 RID: 343
		private static ConcurrentQueue<Message> m_vOutgoingPackets;

		// Token: 0x04000158 RID: 344
		private bool m_vIsRunning;

		// Token: 0x020000CE RID: 206
		// (Invoke) Token: 0x060005D6 RID: 1494
		private delegate void IncomingProcessingDelegate();

		// Token: 0x020000CF RID: 207
		// (Invoke) Token: 0x060005DA RID: 1498
		private delegate void OutgoingProcessingDelegate();
	}
}

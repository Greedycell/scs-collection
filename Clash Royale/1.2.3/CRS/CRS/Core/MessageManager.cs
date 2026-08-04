using System;
using System.Collections.Concurrent;
using System.Threading;
using UCS.Logic;
using UCS.PacketProcessing;

namespace UCS.Core
{
	// Token: 0x020000BE RID: 190
	internal class MessageManager
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x00017CDC File Offset: 0x00015EDC
		public MessageManager()
		{
			MessageManager.m_vPackets = new ConcurrentQueue<Message>();
			this.m_vIsRunning = false;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00017CF5 File Offset: 0x00015EF5
		public void Start()
		{
			new MessageManager.PacketProcessingDelegate(this.PacketProcessing).BeginInvoke(null, null);
			this.m_vIsRunning = true;
			Console.WriteLine("[CRS]    Message manager has been successfully started !");
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00017D1C File Offset: 0x00015F1C
		private void PacketProcessing()
		{
			while (this.m_vIsRunning)
			{
				MessageManager.m_vWaitHandle.WaitOne();
				Message message;
				while (MessageManager.m_vPackets.TryDequeue(out message))
				{
					Level level = message.Client.GetLevel();
					string text = "(0, NoNameYet)";
					if (level != null)
					{
						text = string.Concat(new object[]
						{
							" (",
							level.GetPlayerAvatar().GetId(),
							", ",
							level.GetPlayerAvatar().GetAvatarName(),
							")"
						});
					}
					try
					{
						Debugger.WriteLine("[CRS]    Processing message " + message.GetType().Name + text, null, 4);
						message.Decode();
						message.Process(level);
					}
					catch (Exception ex)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Debugger.WriteLine("[CRS]    An exception occured during processing of message " + message.GetType().Name + text, ex, 4);
						Console.ResetColor();
					}
				}
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00017E20 File Offset: 0x00016020
		public static void ProcessPacket(Message p)
		{
			MessageManager.m_vPackets.Enqueue(p);
			MessageManager.m_vWaitHandle.Set();
		}

		// Token: 0x0400032E RID: 814
		private static ConcurrentQueue<Message> m_vPackets;

		// Token: 0x0400032F RID: 815
		private static readonly EventWaitHandle m_vWaitHandle = new AutoResetEvent(false);

		// Token: 0x04000330 RID: 816
		private bool m_vIsRunning;

		// Token: 0x02000130 RID: 304
		// (Invoke) Token: 0x06000725 RID: 1829
		private delegate void PacketProcessingDelegate();
	}
}

using System;
using System.Collections.Concurrent;
using System.Threading;
using CRS.Logic;
using CRS.PacketProcessing;

namespace CRS.Core
{
	// Token: 0x020000DE RID: 222
	internal class MessageManager
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x0001FC2C File Offset: 0x0001DE2C
		public MessageManager()
		{
			MessageManager.m_vPackets = new BlockingCollection<Message>();
			new MessageManager.PacketProcessingDelegate(this.PacketProcessing).BeginInvoke(null, null);
			Console.WriteLine("[GRS]    Message Manager started");
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001FC5C File Offset: 0x0001DE5C
		public static void ProcessPacket(Message p)
		{
			MessageManager.m_vPackets.Add(p);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001FC6C File Offset: 0x0001DE6C
		private void PacketProcessing()
		{
			for (;;)
			{
				Message message = MessageManager.m_vPackets.Take();
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					Message message2 = (Message)state;
					Level level = message2.Client.GetLevel();
					try
					{
						message2.Decode();
						message2.Process(level);
					}
					catch (Exception)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("[GRS]    Exception occured during decoding/processing message " + message2.GetType().Name);
						Console.ResetColor();
					}
				}, message);
			}
		}

		// Token: 0x040003C9 RID: 969
		private static BlockingCollection<Message> m_vPackets;

		// Token: 0x02000110 RID: 272
		// (Invoke) Token: 0x0600064E RID: 1614
		private delegate void PacketProcessingDelegate();
	}
}

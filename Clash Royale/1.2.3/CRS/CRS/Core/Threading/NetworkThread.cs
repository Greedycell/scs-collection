using System;
using System.Threading;
using UCS.Network;

namespace UCS.Core.Threading
{
	// Token: 0x020000C4 RID: 196
	internal class NetworkThread
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00018444 File Offset: 0x00016644
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0001844B File Offset: 0x0001664B
		private static Thread T { get; set; }

		// Token: 0x060005C6 RID: 1478 RVA: 0x00018453 File Offset: 0x00016653
		public static void Start()
		{
			NetworkThread.T = new Thread(new ThreadStart(delegate
			{
				new ResourcesManager();
				new ObjectManager();
				new PacketManager().Start();
				new MessageManager().Start();
				new Gateway().Start();
				Console.WriteLine("[CRS]    Server started, let's play Clash Royale !");
			}));
			NetworkThread.T.Start();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00018488 File Offset: 0x00016688
		public static void Stop()
		{
			if (NetworkThread.T.ThreadState == ThreadState.Running)
			{
				NetworkThread.T.Abort();
			}
		}
	}
}

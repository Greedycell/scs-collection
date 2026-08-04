using System;
using System.Threading;
using CRS.Core.Network;

namespace CRS.Core.Threading
{
	// Token: 0x020000E6 RID: 230
	internal class NetworkThread
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x00020820 File Offset: 0x0001EA20
		public NetworkThread()
		{
			new Thread(new ThreadStart(delegate
			{
				new PacketManager();
				new MessageManager();
				new ResourcesManager();
				new ObjectManager();
				new Gateway();
			})).Start();
		}
	}
}

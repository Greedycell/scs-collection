using System;
using System.Threading;

namespace UCS.Core.Threading
{
	// Token: 0x020000C1 RID: 193
	internal class InterfaceThread
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000182F9 File Offset: 0x000164F9
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x00018300 File Offset: 0x00016500
		private static Thread T { get; set; }

		// Token: 0x060005B7 RID: 1463 RVA: 0x00018308 File Offset: 0x00016508
		[STAThread]
		public static void Start()
		{
			InterfaceThread.T = new Thread(new ThreadStart(delegate
			{
			}));
			InterfaceThread.T.SetApartmentState(ApartmentState.STA);
			InterfaceThread.T.Start();
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00018348 File Offset: 0x00016548
		public static void Stop()
		{
			if (InterfaceThread.T.ThreadState == ThreadState.Running)
			{
				InterfaceThread.T.Abort();
			}
		}
	}
}

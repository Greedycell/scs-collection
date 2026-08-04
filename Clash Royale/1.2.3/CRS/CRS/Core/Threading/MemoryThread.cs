using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;

namespace UCS.Core.Threading
{
	// Token: 0x020000C2 RID: 194
	internal class MemoryThread
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00018360 File Offset: 0x00016560
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00018367 File Offset: 0x00016567
		private static Thread T { get; set; }

		// Token: 0x060005BC RID: 1468 RVA: 0x0001836F File Offset: 0x0001656F
		public static void Start()
		{
			MemoryThread.T = new Thread(new ThreadStart(delegate
			{
				global::System.Timers.Timer timer = new global::System.Timers.Timer();
				timer.Interval = 2500.0;
				timer.Elapsed += delegate(object s, ElapsedEventArgs a)
				{
					GC.Collect(GC.MaxGeneration);
					GC.WaitForPendingFinalizers();
					MemoryThread.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)uint.MaxValue, (UIntPtr)uint.MaxValue);
				};
				timer.Enabled = true;
			}));
			MemoryThread.T.Start();
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000183A4 File Offset: 0x000165A4
		public static void Stop()
		{
			if (MemoryThread.T.ThreadState == global::System.Threading.ThreadState.Running)
			{
				MemoryThread.T.Abort();
			}
		}

		// Token: 0x060005BE RID: 1470
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);
	}
}

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using CRS.Logic;

namespace CRS.Core.Threading
{
	// Token: 0x020000E4 RID: 228
	internal class MemoryThread
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x00020757 File Offset: 0x0001E957
		public MemoryThread()
		{
			new Thread(new ThreadStart(delegate
			{
				global::System.Timers.Timer timer = new global::System.Timers.Timer();
				timer.Interval = 5000.0;
				timer.Elapsed += delegate(object s, ElapsedEventArgs a)
				{
					foreach (Level level in ResourcesManager.GetInMemoryLevels())
					{
						if (!level.GetClient().IsClientSocketConnected())
						{
							ResourcesManager.DropClient(level.GetClient().GetSocketHandle());
						}
					}
					GC.Collect(GC.MaxGeneration);
					GC.WaitForPendingFinalizers();
					MemoryThread.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)uint.MaxValue, (UIntPtr)uint.MaxValue);
				};
				timer.Enabled = true;
			})).Start();
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00020788 File Offset: 0x0001E988
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0002078F File Offset: 0x0001E98F
		private static Thread T { get; set; }

		// Token: 0x060005BC RID: 1468
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);
	}
}

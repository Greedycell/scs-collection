using System;
using System.Runtime.InteropServices;

namespace UCS.Core.Threading
{
	// Token: 0x020000C3 RID: 195
	internal class PerformanceInfo
	{
		// Token: 0x060005C0 RID: 1472
		[DllImport("psapi.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetPerformanceInfo();

		// Token: 0x060005C1 RID: 1473 RVA: 0x000183BC File Offset: 0x000165BC
		public static long GetPhysicalAvailableMemoryInMiB()
		{
			PerformanceInfo.PerformanceInformation performanceInformation = default(PerformanceInfo.PerformanceInformation);
			if (PerformanceInfo.GetPerformanceInfo())
			{
				return Convert.ToInt64(performanceInformation.PhysicalAvailable.ToInt64() * performanceInformation.PageSize.ToInt64() / 1048576L);
			}
			return -1L;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00018400 File Offset: 0x00016600
		public static long GetTotalMemoryInMiB()
		{
			PerformanceInfo.PerformanceInformation performanceInformation = default(PerformanceInfo.PerformanceInformation);
			if (PerformanceInfo.GetPerformanceInfo())
			{
				return Convert.ToInt64(performanceInformation.PhysicalTotal.ToInt64() * performanceInformation.PageSize.ToInt64() / 1048576L);
			}
			return -1L;
		}

		// Token: 0x02000134 RID: 308
		public struct PerformanceInformation
		{
			// Token: 0x040003EE RID: 1006
			public int Size;

			// Token: 0x040003EF RID: 1007
			public IntPtr CommitTotal;

			// Token: 0x040003F0 RID: 1008
			public IntPtr CommitLimit;

			// Token: 0x040003F1 RID: 1009
			public IntPtr CommitPeak;

			// Token: 0x040003F2 RID: 1010
			public IntPtr PhysicalTotal;

			// Token: 0x040003F3 RID: 1011
			public IntPtr PhysicalAvailable;

			// Token: 0x040003F4 RID: 1012
			public IntPtr SystemCache;

			// Token: 0x040003F5 RID: 1013
			public IntPtr KernelTotal;

			// Token: 0x040003F6 RID: 1014
			public IntPtr KernelPaged;

			// Token: 0x040003F7 RID: 1015
			public IntPtr KernelNonPaged;

			// Token: 0x040003F8 RID: 1016
			public IntPtr PageSize;

			// Token: 0x040003F9 RID: 1017
			public int HandlesCount;

			// Token: 0x040003FA RID: 1018
			public int ProcessCount;

			// Token: 0x040003FB RID: 1019
			public int ThreadCount;
		}
	}
}

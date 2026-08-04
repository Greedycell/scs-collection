using System;
using System.Runtime.InteropServices;

namespace CRS.Core.Threading
{
	// Token: 0x020000E5 RID: 229
	internal class PerformanceInfo
	{
		// Token: 0x060005BD RID: 1469
		[DllImport("psapi.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetPerformanceInfo();

		// Token: 0x060005BE RID: 1470 RVA: 0x00020798 File Offset: 0x0001E998
		public static long GetPhysicalAvailableMemoryInMiB()
		{
			PerformanceInfo.PerformanceInformation performanceInformation = default(PerformanceInfo.PerformanceInformation);
			if (PerformanceInfo.GetPerformanceInfo())
			{
				return Convert.ToInt64(performanceInformation.PhysicalAvailable.ToInt64() * performanceInformation.PageSize.ToInt64() / 1048576L);
			}
			return -1L;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000207DC File Offset: 0x0001E9DC
		public static long GetTotalMemoryInMiB()
		{
			PerformanceInfo.PerformanceInformation performanceInformation = default(PerformanceInfo.PerformanceInformation);
			if (PerformanceInfo.GetPerformanceInfo())
			{
				return Convert.ToInt64(performanceInformation.PhysicalTotal.ToInt64() * performanceInformation.PageSize.ToInt64() / 1048576L);
			}
			return -1L;
		}

		// Token: 0x02000118 RID: 280
		public struct PerformanceInformation
		{
			// Token: 0x040004B2 RID: 1202
			public int Size;

			// Token: 0x040004B3 RID: 1203
			public IntPtr CommitTotal;

			// Token: 0x040004B4 RID: 1204
			public IntPtr CommitLimit;

			// Token: 0x040004B5 RID: 1205
			public IntPtr CommitPeak;

			// Token: 0x040004B6 RID: 1206
			public IntPtr PhysicalTotal;

			// Token: 0x040004B7 RID: 1207
			public IntPtr PhysicalAvailable;

			// Token: 0x040004B8 RID: 1208
			public IntPtr SystemCache;

			// Token: 0x040004B9 RID: 1209
			public IntPtr KernelTotal;

			// Token: 0x040004BA RID: 1210
			public IntPtr KernelPaged;

			// Token: 0x040004BB RID: 1211
			public IntPtr KernelNonPaged;

			// Token: 0x040004BC RID: 1212
			public IntPtr PageSize;

			// Token: 0x040004BD RID: 1213
			public int HandlesCount;

			// Token: 0x040004BE RID: 1214
			public int ProcessCount;

			// Token: 0x040004BF RID: 1215
			public int ThreadCount;
		}
	}
}

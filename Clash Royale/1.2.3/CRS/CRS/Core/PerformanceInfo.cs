using System;
using System.Runtime.InteropServices;

namespace UCS.Core
{
	// Token: 0x020000B8 RID: 184
	public static class PerformanceInfo
	{
		// Token: 0x06000565 RID: 1381
		[DllImport("psapi.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetPerformanceInfo(out PerformanceInfo.PerformanceInformation PerformanceInformation, [In] int Size);

		// Token: 0x06000566 RID: 1382 RVA: 0x00016924 File Offset: 0x00014B24
		public static long GetPhysicalAvailableMemoryInMiB()
		{
			PerformanceInfo.PerformanceInformation performanceInformation = default(PerformanceInfo.PerformanceInformation);
			if (PerformanceInfo.GetPerformanceInfo(out performanceInformation, Marshal.SizeOf<PerformanceInfo.PerformanceInformation>(performanceInformation)))
			{
				return Convert.ToInt64(performanceInformation.PhysicalAvailable.ToInt64() * performanceInformation.PageSize.ToInt64() / 1048576L);
			}
			return -1L;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00016970 File Offset: 0x00014B70
		public static long GetTotalMemoryInMiB()
		{
			PerformanceInfo.PerformanceInformation performanceInformation = default(PerformanceInfo.PerformanceInformation);
			if (PerformanceInfo.GetPerformanceInfo(out performanceInformation, Marshal.SizeOf<PerformanceInfo.PerformanceInformation>(performanceInformation)))
			{
				return Convert.ToInt64(performanceInformation.PhysicalTotal.ToInt64() * performanceInformation.PageSize.ToInt64() / 1048576L);
			}
			return -1L;
		}

		// Token: 0x0200012D RID: 301
		public struct PerformanceInformation
		{
			// Token: 0x040003D7 RID: 983
			public int Size;

			// Token: 0x040003D8 RID: 984
			public IntPtr CommitTotal;

			// Token: 0x040003D9 RID: 985
			public IntPtr CommitLimit;

			// Token: 0x040003DA RID: 986
			public IntPtr CommitPeak;

			// Token: 0x040003DB RID: 987
			public IntPtr PhysicalTotal;

			// Token: 0x040003DC RID: 988
			public IntPtr PhysicalAvailable;

			// Token: 0x040003DD RID: 989
			public IntPtr SystemCache;

			// Token: 0x040003DE RID: 990
			public IntPtr KernelTotal;

			// Token: 0x040003DF RID: 991
			public IntPtr KernelPaged;

			// Token: 0x040003E0 RID: 992
			public IntPtr KernelNonPaged;

			// Token: 0x040003E1 RID: 993
			public IntPtr PageSize;

			// Token: 0x040003E2 RID: 994
			public int HandlesCount;

			// Token: 0x040003E3 RID: 995
			public int ProcessCount;

			// Token: 0x040003E4 RID: 996
			public int ThreadCount;
		}
	}
}

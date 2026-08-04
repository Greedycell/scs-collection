using System;
using System.Runtime.InteropServices;

namespace CRS.Core
{
	// Token: 0x020000DA RID: 218
	public static class PerformanceInfo
	{
		// Token: 0x0600055E RID: 1374
		[DllImport("psapi.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetPerformanceInfo(out PerformanceInfo.PerformanceInformation PerformanceInformation, [In] int Size);

		// Token: 0x0600055F RID: 1375 RVA: 0x0001E590 File Offset: 0x0001C790
		public static long GetPhysicalAvailableMemoryInMiB()
		{
			PerformanceInfo.PerformanceInformation performanceInformation = default(PerformanceInfo.PerformanceInformation);
			if (PerformanceInfo.GetPerformanceInfo(out performanceInformation, Marshal.SizeOf<PerformanceInfo.PerformanceInformation>(performanceInformation)))
			{
				return Convert.ToInt64(performanceInformation.PhysicalAvailable.ToInt64() * performanceInformation.PageSize.ToInt64() / 1048576L);
			}
			return -1L;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001E5DC File Offset: 0x0001C7DC
		public static long GetTotalMemoryInMiB()
		{
			PerformanceInfo.PerformanceInformation performanceInformation = default(PerformanceInfo.PerformanceInformation);
			if (PerformanceInfo.GetPerformanceInfo(out performanceInformation, Marshal.SizeOf<PerformanceInfo.PerformanceInformation>(performanceInformation)))
			{
				return Convert.ToInt64(performanceInformation.PhysicalTotal.ToInt64() * performanceInformation.PageSize.ToInt64() / 1048576L);
			}
			return -1L;
		}

		// Token: 0x0200010D RID: 269
		public struct PerformanceInformation
		{
			// Token: 0x04000498 RID: 1176
			public int Size;

			// Token: 0x04000499 RID: 1177
			public IntPtr CommitTotal;

			// Token: 0x0400049A RID: 1178
			public IntPtr CommitLimit;

			// Token: 0x0400049B RID: 1179
			public IntPtr CommitPeak;

			// Token: 0x0400049C RID: 1180
			public IntPtr PhysicalTotal;

			// Token: 0x0400049D RID: 1181
			public IntPtr PhysicalAvailable;

			// Token: 0x0400049E RID: 1182
			public IntPtr SystemCache;

			// Token: 0x0400049F RID: 1183
			public IntPtr KernelTotal;

			// Token: 0x040004A0 RID: 1184
			public IntPtr KernelPaged;

			// Token: 0x040004A1 RID: 1185
			public IntPtr KernelNonPaged;

			// Token: 0x040004A2 RID: 1186
			public IntPtr PageSize;

			// Token: 0x040004A3 RID: 1187
			public int HandlesCount;

			// Token: 0x040004A4 RID: 1188
			public int ProcessCount;

			// Token: 0x040004A5 RID: 1189
			public int ThreadCount;
		}
	}
}

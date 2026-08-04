using System;

namespace UCS.Core
{
	// Token: 0x020000B7 RID: 183
	internal class Performances
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x00016858 File Offset: 0x00014A58
		public static string GetFreeMemoryMB()
		{
			return PerformanceInfo.GetPhysicalAvailableMemoryInMiB().ToString();
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00016874 File Offset: 0x00014A74
		public static string GetTotalMemory()
		{
			return PerformanceInfo.GetTotalMemoryInMiB().ToString();
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00016890 File Offset: 0x00014A90
		public static string GetUsedMemory()
		{
			long physicalAvailableMemoryInMiB = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
			long totalMemoryInMiB = PerformanceInfo.GetTotalMemoryInMiB();
			decimal num = physicalAvailableMemoryInMiB / totalMemoryInMiB * 100m;
			return (100m - num).ToString("##.##");
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000168E0 File Offset: 0x00014AE0
		public static string GetFreeMemory()
		{
			long physicalAvailableMemoryInMiB = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
			long totalMemoryInMiB = PerformanceInfo.GetTotalMemoryInMiB();
			return (physicalAvailableMemoryInMiB / totalMemoryInMiB * 100m).ToString("##.##");
		}
	}
}

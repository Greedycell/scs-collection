using System;

namespace CRS.Core
{
	// Token: 0x020000DB RID: 219
	internal class Performances
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0001E628 File Offset: 0x0001C828
		public static string GetFreeMemory()
		{
			long physicalAvailableMemoryInMiB = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
			long totalMemoryInMiB = PerformanceInfo.GetTotalMemoryInMiB();
			return (physicalAvailableMemoryInMiB / totalMemoryInMiB * 100m).ToString("##.##");
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001E66C File Offset: 0x0001C86C
		public static string GetFreeMemoryMB()
		{
			return PerformanceInfo.GetPhysicalAvailableMemoryInMiB().ToString();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001E688 File Offset: 0x0001C888
		public static string GetTotalMemory()
		{
			return PerformanceInfo.GetTotalMemoryInMiB().ToString();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001E6A4 File Offset: 0x0001C8A4
		public static string GetUsedMemory()
		{
			long physicalAvailableMemoryInMiB = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
			long totalMemoryInMiB = PerformanceInfo.GetTotalMemoryInMiB();
			decimal num = physicalAvailableMemoryInMiB / totalMemoryInMiB * 100m;
			return (100m - num).ToString("##.##");
		}
	}
}

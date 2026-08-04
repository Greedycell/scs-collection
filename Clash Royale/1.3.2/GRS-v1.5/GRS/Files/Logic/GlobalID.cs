using System;

namespace CRS.Files.Logic
{
	// Token: 0x02000009 RID: 9
	public static class GlobalID
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000028A4 File Offset: 0x00000AA4
		public static int CreateGlobalID(int index, int count)
		{
			return count + 1000000 * index;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028AF File Offset: 0x00000AAF
		public static int GetClassID(int commandType)
		{
			commandType = (int)(1125899907L * (long)commandType >> 32);
			return (commandType >> 18) + (commandType >> 31);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028CC File Offset: 0x00000ACC
		public static int GetInstanceID(int globalID)
		{
			int num = 1125899907;
			num = (int)((long)num * (long)globalID >> 32);
			return globalID - 1000000 * ((num >> 18) + (num >> 31));
		}
	}
}

using System;

namespace UCS.Logic
{
	// Token: 0x02000097 RID: 151
	public static class GlobalID
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x00013723 File Offset: 0x00011923
		public static int CreateGlobalID(int index, int count)
		{
			return count + 1000000 * index;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001372E File Offset: 0x0001192E
		public static int GetClassID(int commandType)
		{
			commandType = (int)(1125899907L * (long)commandType >> 32);
			return (commandType >> 18) + (commandType >> 31);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00013748 File Offset: 0x00011948
		public static int GetInstanceID(int globalID)
		{
			int num = 1125899907;
			num = (int)((long)num * (long)globalID >> 32);
			return globalID - 1000000 * ((num >> 18) + (num >> 31));
		}
	}
}

using System;

namespace CRS.Helpers
{
	// Token: 0x02000005 RID: 5
	internal static class GamePlayUtil
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002107 File Offset: 0x00000307
		public static int CalculateResourceCost(int sup, int inf, int supCost, int infCost, int amount)
		{
			return (int)Math.Round((double)((long)(supCost - infCost) * (long)(amount - inf)) / ((double)sup - (double)inf * 1.0)) + infCost;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002107 File Offset: 0x00000307
		public static int CalculateSpeedUpCost(int sup, int inf, int supCost, int infCost, int amount)
		{
			return (int)Math.Round((double)((long)(supCost - infCost) * (long)(amount - inf)) / ((double)sup - (double)inf * 1.0)) + infCost;
		}
	}
}

using System;

namespace UCS.Helpers
{
	// Token: 0x02000026 RID: 38
	internal static class GamePlayUtil
	{
		// Token: 0x06000163 RID: 355 RVA: 0x0000C546 File Offset: 0x0000A746
		public static int CalculateResourceCost(int sup, int inf, int supCost, int infCost, int amount)
		{
			return (int)Math.Round((double)((long)(supCost - infCost) * (long)(amount - inf)) / ((double)sup - (double)inf * 1.0)) + infCost;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000C546 File Offset: 0x0000A746
		public static int CalculateSpeedUpCost(int sup, int inf, int supCost, int infCost, int amount)
		{
			return (int)Math.Round((double)((long)(supCost - infCost) * (long)(amount - inf)) / ((double)sup - (double)inf * 1.0)) + infCost;
		}
	}
}

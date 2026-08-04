using System;

namespace CRS.Utilities.ZLib
{
	// Token: 0x02000029 RID: 41
	internal static class InternalConstants
	{
		// Token: 0x04000131 RID: 305
		internal static readonly int MAX_BITS = 15;

		// Token: 0x04000132 RID: 306
		internal static readonly int BL_CODES = 19;

		// Token: 0x04000133 RID: 307
		internal static readonly int D_CODES = 30;

		// Token: 0x04000134 RID: 308
		internal static readonly int LITERALS = 256;

		// Token: 0x04000135 RID: 309
		internal static readonly int LENGTH_CODES = 29;

		// Token: 0x04000136 RID: 310
		internal static readonly int L_CODES = InternalConstants.LITERALS + 1 + InternalConstants.LENGTH_CODES;

		// Token: 0x04000137 RID: 311
		internal static readonly int MAX_BL_BITS = 7;

		// Token: 0x04000138 RID: 312
		internal static readonly int REP_3_6 = 16;

		// Token: 0x04000139 RID: 313
		internal static readonly int REPZ_3_10 = 17;

		// Token: 0x0400013A RID: 314
		internal static readonly int REPZ_11_138 = 18;
	}
}

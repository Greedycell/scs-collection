using System;

namespace Ionic.Zlib
{
	// Token: 0x02000016 RID: 22
	internal static class InternalConstants
	{
		// Token: 0x040000FD RID: 253
		internal static readonly int MAX_BITS = 15;

		// Token: 0x040000FE RID: 254
		internal static readonly int BL_CODES = 19;

		// Token: 0x040000FF RID: 255
		internal static readonly int D_CODES = 30;

		// Token: 0x04000100 RID: 256
		internal static readonly int LITERALS = 256;

		// Token: 0x04000101 RID: 257
		internal static readonly int LENGTH_CODES = 29;

		// Token: 0x04000102 RID: 258
		internal static readonly int L_CODES = InternalConstants.LITERALS + 1 + InternalConstants.LENGTH_CODES;

		// Token: 0x04000103 RID: 259
		internal static readonly int MAX_BL_BITS = 7;

		// Token: 0x04000104 RID: 260
		internal static readonly int REP_3_6 = 16;

		// Token: 0x04000105 RID: 261
		internal static readonly int REPZ_3_10 = 17;

		// Token: 0x04000106 RID: 262
		internal static readonly int REPZ_11_138 = 18;
	}
}

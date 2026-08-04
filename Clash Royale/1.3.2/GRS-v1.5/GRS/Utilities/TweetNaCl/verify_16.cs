using System;

namespace CRS.Utilities.TweetNaCl
{
	// Token: 0x02000036 RID: 54
	public class verify_16
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		public static int crypto_verify(byte[] x, int xoffset, byte[] y)
		{
			int num = 0;
			for (int i = 0; i < 15; i++)
			{
				num |= (int)((x[xoffset + i] ^ y[i]) & byte.MaxValue);
			}
			return (int)((1U & ((uint)(num - 1) >> 8)) - 1U);
		}

		// Token: 0x0400018D RID: 397
		internal readonly int crypto_verify_16_ref_BYTES = 16;
	}
}

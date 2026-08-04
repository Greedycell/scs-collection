using System;

namespace CRS.Utilities.TweetNaCl
{
	// Token: 0x02000037 RID: 55
	public class xsalsa20
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x0000DE28 File Offset: 0x0000C028
		public static int crypto_stream(byte[] c, int clen, byte[] n, byte[] k)
		{
			byte[] array = new byte[32];
			hsalsa20.crypto_core(array, n, k, xsalsa20.sigma);
			return salsa20.crypto_stream(c, clen, n, 16, array);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000DE58 File Offset: 0x0000C058
		public static int crypto_stream_xor(byte[] c, byte[] m, long mlen, byte[] n, byte[] k)
		{
			byte[] array = new byte[32];
			hsalsa20.crypto_core(array, n, k, xsalsa20.sigma);
			return salsa20.crypto_stream_xor(c, m, (int)mlen, n, 16, array);
		}

		// Token: 0x0400018E RID: 398
		internal readonly int crypto_stream_xsalsa20_ref_KEYBYTES = 32;

		// Token: 0x0400018F RID: 399
		internal readonly int crypto_stream_xsalsa20_ref_NONCEBYTES = 24;

		// Token: 0x04000190 RID: 400
		public static readonly byte[] sigma = new byte[]
		{
			101, 120, 112, 97, 110, 100, 32, 51, 50, 45,
			98, 121, 116, 101, 32, 107
		};
	}
}

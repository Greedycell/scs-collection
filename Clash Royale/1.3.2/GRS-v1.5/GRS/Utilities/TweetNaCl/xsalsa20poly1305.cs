using System;

namespace CRS.Utilities.TweetNaCl
{
	// Token: 0x02000038 RID: 56
	public class xsalsa20poly1305
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x0000DEBC File Offset: 0x0000C0BC
		public static int crypto_secretbox(byte[] c, byte[] m, long mlen, byte[] n, byte[] k)
		{
			if (mlen < 32L)
			{
				return -1;
			}
			xsalsa20.crypto_stream_xor(c, m, mlen, n, k);
			poly1305.crypto_onetimeauth(c, 16, c, 32, mlen - 32L, c);
			for (int i = 0; i < 16; i++)
			{
				c[i] = 0;
			}
			return 0;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000DF04 File Offset: 0x0000C104
		public static int crypto_secretbox_open(byte[] m, byte[] c, long clen, byte[] n, byte[] k)
		{
			if (clen < 32L)
			{
				return -1;
			}
			byte[] array = new byte[32];
			xsalsa20.crypto_stream(array, 32, n, k);
			if (poly1305.crypto_onetimeauth_verify(c, 16, c, 32, clen - 32L, array) != 0)
			{
				return -1;
			}
			xsalsa20.crypto_stream_xor(m, c, clen, n, k);
			for (int i = 0; i < 32; i++)
			{
				m[i] = 0;
			}
			return 0;
		}

		// Token: 0x04000191 RID: 401
		internal readonly int crypto_secretbox_KEYBYTES = 32;

		// Token: 0x04000192 RID: 402
		internal readonly int crypto_secretbox_NONCEBYTES = 24;

		// Token: 0x04000193 RID: 403
		internal readonly int crypto_secretbox_ZEROBYTES = 32;

		// Token: 0x04000194 RID: 404
		internal readonly int crypto_secretbox_BOXZEROBYTES = 16;
	}
}

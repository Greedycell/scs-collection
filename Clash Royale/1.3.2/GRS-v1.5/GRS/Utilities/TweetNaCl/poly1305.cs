using System;

namespace CRS.Utilities.TweetNaCl
{
	// Token: 0x02000034 RID: 52
	public class poly1305
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0000D424 File Offset: 0x0000B624
		public static int crypto_onetimeauth_verify(byte[] h, int hoffset, byte[] inv, int invoffset, long inlen, byte[] k)
		{
			byte[] array = new byte[16];
			poly1305.crypto_onetimeauth(array, 0, inv, invoffset, inlen, k);
			return verify_16.crypto_verify(h, hoffset, array);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000D450 File Offset: 0x0000B650
		internal static void add(int[] h, int[] c)
		{
			int num = 0;
			for (int i = 0; i < 17; i++)
			{
				num += h[i] + c[i];
				h[i] = num & 255;
				num = (int)((uint)num >> 8);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000D484 File Offset: 0x0000B684
		internal static void squeeze(int[] h)
		{
			int num = 0;
			for (int i = 0; i < 16; i++)
			{
				num += h[i];
				h[i] = num & 255;
				num = (int)((uint)num >> 8);
			}
			num += h[16];
			h[16] = num & 3;
			num = (int)(5U * ((uint)num >> 2));
			for (int j = 0; j < 16; j++)
			{
				num += h[j];
				h[j] = num & 255;
				num = (int)((uint)num >> 8);
			}
			num += h[16];
			h[16] = num;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		internal static void freeze(int[] h)
		{
			int[] array = new int[17];
			for (int i = 0; i < 17; i++)
			{
				array[i] = h[i];
			}
			poly1305.add(h, poly1305.minusp);
			int num = (int)(-(int)((uint)h[16] >> 7));
			for (int j = 0; j < 17; j++)
			{
				h[j] ^= num & (array[j] ^ h[j]);
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000D554 File Offset: 0x0000B754
		internal static void mulmod(int[] h, int[] r)
		{
			int[] array = new int[17];
			for (int i = 0; i < 17; i++)
			{
				int num = 0;
				for (int j = 0; j <= i; j++)
				{
					num += h[j] * r[i - j];
				}
				for (int k = i + 1; k < 17; k++)
				{
					num += 320 * h[k] * r[i + 17 - k];
				}
				array[i] = num;
			}
			for (int l = 0; l < 17; l++)
			{
				h[l] = array[l];
			}
			poly1305.squeeze(h);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		public static int crypto_onetimeauth(byte[] outv, int outvoffset, byte[] inv, int invoffset, long inlen, byte[] k)
		{
			int[] array = new int[17];
			int[] array2 = new int[17];
			int[] array3 = new int[17];
			array[0] = (int)(k[0] & byte.MaxValue);
			array[1] = (int)(k[1] & byte.MaxValue);
			array[2] = (int)(k[2] & byte.MaxValue);
			array[3] = (int)(k[3] & 15);
			array[4] = (int)(k[4] & 252);
			array[5] = (int)(k[5] & byte.MaxValue);
			array[6] = (int)(k[6] & byte.MaxValue);
			array[7] = (int)(k[7] & 15);
			array[8] = (int)(k[8] & 252);
			array[9] = (int)(k[9] & byte.MaxValue);
			array[10] = (int)(k[10] & byte.MaxValue);
			array[11] = (int)(k[11] & 15);
			array[12] = (int)(k[12] & 252);
			array[13] = (int)(k[13] & byte.MaxValue);
			array[14] = (int)(k[14] & byte.MaxValue);
			array[15] = (int)(k[15] & 15);
			array[16] = 0;
			for (int i = 0; i < 17; i++)
			{
				array2[i] = 0;
			}
			while (inlen > 0L)
			{
				int i;
				for (i = 0; i < 17; i++)
				{
					array3[i] = 0;
				}
				i = 0;
				while (i < 16 && (long)i < inlen)
				{
					array3[i] = (int)(inv[invoffset + i] & byte.MaxValue);
					i++;
				}
				array3[i] = 1;
				invoffset += i;
				inlen -= (long)i;
				poly1305.add(array2, array3);
				poly1305.mulmod(array2, array);
			}
			poly1305.freeze(array2);
			for (int i = 0; i < 16; i++)
			{
				array3[i] = (int)(k[i + 16] & byte.MaxValue);
			}
			array3[16] = 0;
			poly1305.add(array2, array3);
			for (int i = 0; i < 16; i++)
			{
				outv[i + outvoffset] = (byte)array2[i];
			}
			return 0;
		}

		// Token: 0x04000183 RID: 387
		internal readonly int CRYPTO_BYTES = 16;

		// Token: 0x04000184 RID: 388
		internal readonly int CRYPTO_KEYBYTES = 32;

		// Token: 0x04000185 RID: 389
		internal static readonly int[] minusp = new int[]
		{
			5, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 252
		};
	}
}

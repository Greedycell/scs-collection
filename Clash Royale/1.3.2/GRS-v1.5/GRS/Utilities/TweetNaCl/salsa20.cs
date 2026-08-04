using System;

namespace CRS.Utilities.TweetNaCl
{
	// Token: 0x02000035 RID: 53
	public class salsa20
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x0000D7BE File Offset: 0x0000B9BE
		internal static long rotate(int u, int c)
		{
			return (long)((u << c) | (int)((uint)u >> 32 - c));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000CFAE File Offset: 0x0000B1AE
		internal static int load_littleendian(byte[] x, int offset)
		{
			return (int)(x[offset] & byte.MaxValue) | ((int)(x[offset + 1] & byte.MaxValue) << 8) | ((int)(x[offset + 2] & byte.MaxValue) << 16) | ((int)(x[offset + 3] & byte.MaxValue) << 24);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000CFE5 File Offset: 0x0000B1E5
		internal static void store_littleendian(byte[] x, int offset, int u)
		{
			x[offset] = (byte)u;
			u = (int)((uint)u >> 8);
			x[offset + 1] = (byte)u;
			u = (int)((uint)u >> 8);
			x[offset + 2] = (byte)u;
			u = (int)((uint)u >> 8);
			x[offset + 3] = (byte)u;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		public static int crypto_core(byte[] outv, byte[] inv, byte[] k, byte[] c)
		{
			int num2;
			int num = (num2 = salsa20.load_littleendian(c, 0));
			int num4;
			int num3 = (num4 = salsa20.load_littleendian(k, 0));
			int num6;
			int num5 = (num6 = salsa20.load_littleendian(k, 4));
			int num8;
			int num7 = (num8 = salsa20.load_littleendian(k, 8));
			int num10;
			int num9 = (num10 = salsa20.load_littleendian(k, 12));
			int num12;
			int num11 = (num12 = salsa20.load_littleendian(c, 4));
			int num14;
			int num13 = (num14 = salsa20.load_littleendian(inv, 0));
			int num16;
			int num15 = (num16 = salsa20.load_littleendian(inv, 4));
			int num18;
			int num17 = (num18 = salsa20.load_littleendian(inv, 8));
			int num20;
			int num19 = (num20 = salsa20.load_littleendian(inv, 12));
			int num22;
			int num21 = (num22 = salsa20.load_littleendian(c, 8));
			int num24;
			int num23 = (num24 = salsa20.load_littleendian(k, 16));
			int num26;
			int num25 = (num26 = salsa20.load_littleendian(k, 20));
			int num28;
			int num27 = (num28 = salsa20.load_littleendian(k, 24));
			int num30;
			int num29 = (num30 = salsa20.load_littleendian(k, 28));
			int num32;
			int num31 = (num32 = salsa20.load_littleendian(c, 12));
			for (int i = 20; i > 0; i -= 2)
			{
				num9 ^= (int)salsa20.rotate(num + num25, 7);
				num17 ^= (int)salsa20.rotate(num9 + num, 9);
				num25 ^= (int)salsa20.rotate(num17 + num9, 13);
				num ^= (int)salsa20.rotate(num25 + num17, 18);
				num19 ^= (int)salsa20.rotate(num11 + num3, 7);
				num27 ^= (int)salsa20.rotate(num19 + num11, 9);
				num3 ^= (int)salsa20.rotate(num27 + num19, 13);
				num11 ^= (int)salsa20.rotate(num3 + num27, 18);
				num29 ^= (int)salsa20.rotate(num21 + num13, 7);
				num5 ^= (int)salsa20.rotate(num29 + num21, 9);
				num13 ^= (int)salsa20.rotate(num5 + num29, 13);
				num21 ^= (int)salsa20.rotate(num13 + num5, 18);
				num7 ^= (int)salsa20.rotate(num31 + num23, 7);
				num15 ^= (int)salsa20.rotate(num7 + num31, 9);
				num23 ^= (int)salsa20.rotate(num15 + num7, 13);
				num31 ^= (int)salsa20.rotate(num23 + num15, 18);
				num3 ^= (int)salsa20.rotate(num + num7, 7);
				num5 ^= (int)salsa20.rotate(num3 + num, 9);
				num7 ^= (int)salsa20.rotate(num5 + num3, 13);
				num ^= (int)salsa20.rotate(num7 + num5, 18);
				num13 ^= (int)salsa20.rotate(num11 + num9, 7);
				num15 ^= (int)salsa20.rotate(num13 + num11, 9);
				num9 ^= (int)salsa20.rotate(num15 + num13, 13);
				num11 ^= (int)salsa20.rotate(num9 + num15, 18);
				num23 ^= (int)salsa20.rotate(num21 + num19, 7);
				num17 ^= (int)salsa20.rotate(num23 + num21, 9);
				num19 ^= (int)salsa20.rotate(num17 + num23, 13);
				num21 ^= (int)salsa20.rotate(num19 + num17, 18);
				num25 ^= (int)salsa20.rotate(num31 + num29, 7);
				num27 ^= (int)salsa20.rotate(num25 + num31, 9);
				num29 ^= (int)salsa20.rotate(num27 + num25, 13);
				num31 ^= (int)salsa20.rotate(num29 + num27, 18);
			}
			num += num2;
			num3 += num4;
			num5 += num6;
			num7 += num8;
			num9 += num10;
			num11 += num12;
			num13 += num14;
			num15 += num16;
			num17 += num18;
			num19 += num20;
			num21 += num22;
			num23 += num24;
			num25 += num26;
			num27 += num28;
			num29 += num30;
			num31 += num32;
			salsa20.store_littleendian(outv, 0, num);
			salsa20.store_littleendian(outv, 4, num3);
			salsa20.store_littleendian(outv, 8, num5);
			salsa20.store_littleendian(outv, 12, num7);
			salsa20.store_littleendian(outv, 16, num9);
			salsa20.store_littleendian(outv, 20, num11);
			salsa20.store_littleendian(outv, 24, num13);
			salsa20.store_littleendian(outv, 28, num15);
			salsa20.store_littleendian(outv, 32, num17);
			salsa20.store_littleendian(outv, 36, num19);
			salsa20.store_littleendian(outv, 40, num21);
			salsa20.store_littleendian(outv, 44, num23);
			salsa20.store_littleendian(outv, 48, num25);
			salsa20.store_littleendian(outv, 52, num27);
			salsa20.store_littleendian(outv, 56, num29);
			salsa20.store_littleendian(outv, 60, num31);
			return 0;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		public static int crypto_stream(byte[] c, int clen, byte[] n, int noffset, byte[] k)
		{
			byte[] array = new byte[16];
			byte[] array2 = new byte[64];
			int num = 0;
			if (clen == 0)
			{
				return 0;
			}
			for (int i = 0; i < 8; i++)
			{
				array[i] = n[noffset + i];
			}
			for (int j = 8; j < 16; j++)
			{
				array[j] = 0;
			}
			while (clen >= 64)
			{
				salsa20.crypto_core(c, array, k, xsalsa20.sigma);
				int num2 = 1;
				for (int l = 8; l < 16; l++)
				{
					num2 += (int)(array[l] & byte.MaxValue);
					array[l] = (byte)num2;
					num2 = (int)((uint)num2 >> 8);
				}
				clen -= 64;
				num += 64;
			}
			if (clen != 0)
			{
				salsa20.crypto_core(array2, array, k, xsalsa20.sigma);
				for (int m = 0; m < clen; m++)
				{
					c[num + m] = array2[m];
				}
			}
			return 0;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		public static int crypto_stream_xor(byte[] c, byte[] m, int mlen, byte[] n, int noffset, byte[] k)
		{
			byte[] array = new byte[16];
			byte[] array2 = new byte[64];
			int num = 0;
			int num2 = 0;
			if (mlen == 0)
			{
				return 0;
			}
			for (int i = 0; i < 8; i++)
			{
				array[i] = n[noffset + i];
			}
			for (int j = 8; j < 16; j++)
			{
				array[j] = 0;
			}
			while (mlen >= 64)
			{
				salsa20.crypto_core(array2, array, k, xsalsa20.sigma);
				for (int l = 0; l < 64; l++)
				{
					c[num + l] = (byte)(m[num2 + l] ^ array2[l]);
				}
				int num3 = 1;
				for (int num4 = 8; num4 < 16; num4++)
				{
					num3 += (int)(array[num4] & byte.MaxValue);
					array[num4] = (byte)num3;
					num3 = (int)((uint)num3 >> 8);
				}
				mlen -= 64;
				num += 64;
				num2 += 64;
			}
			if (mlen != 0)
			{
				salsa20.crypto_core(array2, array, k, xsalsa20.sigma);
				for (int num5 = 0; num5 < mlen; num5++)
				{
					c[num + num5] = (byte)(m[num2 + num5] ^ array2[num5]);
				}
			}
			return 0;
		}

		// Token: 0x04000186 RID: 390
		internal readonly int crypto_core_salsa20_ref_OUTPUTBYTES = 64;

		// Token: 0x04000187 RID: 391
		internal readonly int crypto_core_salsa20_ref_INPUTBYTES = 16;

		// Token: 0x04000188 RID: 392
		internal readonly int crypto_core_salsa20_ref_KEYBYTES = 32;

		// Token: 0x04000189 RID: 393
		internal readonly int crypto_core_salsa20_ref_CONSTBYTES = 16;

		// Token: 0x0400018A RID: 394
		internal readonly int crypto_stream_salsa20_ref_KEYBYTES = 32;

		// Token: 0x0400018B RID: 395
		internal readonly int crypto_stream_salsa20_ref_NONCEBYTES = 8;

		// Token: 0x0400018C RID: 396
		internal const int ROUNDS = 20;
	}
}

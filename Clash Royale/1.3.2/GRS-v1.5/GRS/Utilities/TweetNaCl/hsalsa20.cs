using System;

namespace CRS.Utilities.TweetNaCl
{
	// Token: 0x02000033 RID: 51
	public class hsalsa20
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000CF9C File Offset: 0x0000B19C
		internal static int rotate(int u, int c)
		{
			return (u << c) | (int)((uint)u >> 32 - c);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000CFAE File Offset: 0x0000B1AE
		internal static int load_littleendian(byte[] x, int offset)
		{
			return (int)(x[offset] & byte.MaxValue) | ((int)(x[offset + 1] & byte.MaxValue) << 8) | ((int)(x[offset + 2] & byte.MaxValue) << 16) | ((int)(x[offset + 3] & byte.MaxValue) << 24);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000CFE5 File Offset: 0x0000B1E5
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

		// Token: 0x060001CF RID: 463 RVA: 0x0000D010 File Offset: 0x0000B210
		public static int crypto_core(byte[] outv, byte[] inv, byte[] k, byte[] c)
		{
			int num2;
			int num = (num2 = hsalsa20.load_littleendian(c, 0));
			int num4;
			int num3 = (num4 = hsalsa20.load_littleendian(k, 0));
			int num6;
			int num5 = (num6 = hsalsa20.load_littleendian(k, 4));
			int num8;
			int num7 = (num8 = hsalsa20.load_littleendian(k, 8));
			int num10;
			int num9 = (num10 = hsalsa20.load_littleendian(k, 12));
			int num12;
			int num11 = (num12 = hsalsa20.load_littleendian(c, 4));
			int num14;
			int num13;
			int num16;
			int num15;
			int num18;
			int num17;
			int num20;
			int num19;
			if (inv != null)
			{
				num13 = (num14 = hsalsa20.load_littleendian(inv, 0));
				num15 = (num16 = hsalsa20.load_littleendian(inv, 4));
				num17 = (num18 = hsalsa20.load_littleendian(inv, 8));
				num19 = (num20 = hsalsa20.load_littleendian(inv, 12));
			}
			else
			{
				num13 = (num14 = (num16 = (num15 = (num18 = (num17 = (num20 = (num19 = 0)))))));
			}
			int num22;
			int num21 = (num22 = hsalsa20.load_littleendian(c, 8));
			int num24;
			int num23 = (num24 = hsalsa20.load_littleendian(k, 16));
			int num26;
			int num25 = (num26 = hsalsa20.load_littleendian(k, 20));
			int num28;
			int num27 = (num28 = hsalsa20.load_littleendian(k, 24));
			int num30;
			int num29 = (num30 = hsalsa20.load_littleendian(k, 28));
			int num32;
			int num31 = (num32 = hsalsa20.load_littleendian(c, 12));
			for (int i = 20; i > 0; i -= 2)
			{
				num9 ^= hsalsa20.rotate(num + num25, 7);
				num17 ^= hsalsa20.rotate(num9 + num, 9);
				num25 ^= hsalsa20.rotate(num17 + num9, 13);
				num ^= hsalsa20.rotate(num25 + num17, 18);
				num19 ^= hsalsa20.rotate(num11 + num3, 7);
				num27 ^= hsalsa20.rotate(num19 + num11, 9);
				num3 ^= hsalsa20.rotate(num27 + num19, 13);
				num11 ^= hsalsa20.rotate(num3 + num27, 18);
				num29 ^= hsalsa20.rotate(num21 + num13, 7);
				num5 ^= hsalsa20.rotate(num29 + num21, 9);
				num13 ^= hsalsa20.rotate(num5 + num29, 13);
				num21 ^= hsalsa20.rotate(num13 + num5, 18);
				num7 ^= hsalsa20.rotate(num31 + num23, 7);
				num15 ^= hsalsa20.rotate(num7 + num31, 9);
				num23 ^= hsalsa20.rotate(num15 + num7, 13);
				num31 ^= hsalsa20.rotate(num23 + num15, 18);
				num3 ^= hsalsa20.rotate(num + num7, 7);
				num5 ^= hsalsa20.rotate(num3 + num, 9);
				num7 ^= hsalsa20.rotate(num5 + num3, 13);
				num ^= hsalsa20.rotate(num7 + num5, 18);
				num13 ^= hsalsa20.rotate(num11 + num9, 7);
				num15 ^= hsalsa20.rotate(num13 + num11, 9);
				num9 ^= hsalsa20.rotate(num15 + num13, 13);
				num11 ^= hsalsa20.rotate(num9 + num15, 18);
				num23 ^= hsalsa20.rotate(num21 + num19, 7);
				num17 ^= hsalsa20.rotate(num23 + num21, 9);
				num19 ^= hsalsa20.rotate(num17 + num23, 13);
				num21 ^= hsalsa20.rotate(num19 + num17, 18);
				num25 ^= hsalsa20.rotate(num31 + num29, 7);
				num27 ^= hsalsa20.rotate(num25 + num31, 9);
				num29 ^= hsalsa20.rotate(num27 + num25, 13);
				num31 ^= hsalsa20.rotate(num29 + num27, 18);
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
			num -= hsalsa20.load_littleendian(c, 0);
			num11 -= hsalsa20.load_littleendian(c, 4);
			num21 -= hsalsa20.load_littleendian(c, 8);
			num31 -= hsalsa20.load_littleendian(c, 12);
			if (inv != null)
			{
				num13 -= hsalsa20.load_littleendian(inv, 0);
				num15 -= hsalsa20.load_littleendian(inv, 4);
				num17 -= hsalsa20.load_littleendian(inv, 8);
				num19 -= hsalsa20.load_littleendian(inv, 12);
			}
			hsalsa20.store_littleendian(outv, 0, num);
			hsalsa20.store_littleendian(outv, 4, num11);
			hsalsa20.store_littleendian(outv, 8, num21);
			hsalsa20.store_littleendian(outv, 12, num31);
			hsalsa20.store_littleendian(outv, 16, num13);
			hsalsa20.store_littleendian(outv, 20, num15);
			hsalsa20.store_littleendian(outv, 24, num17);
			hsalsa20.store_littleendian(outv, 28, num19);
			return 0;
		}

		// Token: 0x04000182 RID: 386
		internal const int ROUNDS = 20;
	}
}

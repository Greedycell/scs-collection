using System;

namespace CRS.Utilities.TweetNaCl
{
	// Token: 0x02000031 RID: 49
	public class curve25519
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000C438 File Offset: 0x0000A638
		public static int crypto_scalarmult_base(byte[] q, byte[] n)
		{
			byte[] array = curve25519.basev;
			return curve25519.crypto_scalarmult(q, n, array);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000C454 File Offset: 0x0000A654
		internal static void add(int[] outv, int outvoffset, int[] a, int aoffset, int[] b, int boffset)
		{
			int num = 0;
			for (int i = 0; i < 31; i++)
			{
				num += a[aoffset + i] + b[boffset + i];
				outv[outvoffset + i] = num & 255;
				num = (int)((uint)num >> 8);
			}
			num += a[aoffset + 31] + b[boffset + 31];
			outv[outvoffset + 31] = num;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000C4AC File Offset: 0x0000A6AC
		internal static void sub(int[] outv, int outvoffset, int[] a, int aoffset, int[] b, int boffset)
		{
			int num = 218;
			for (int i = 0; i < 31; i++)
			{
				num += a[aoffset + i] + 65280 - b[boffset + i];
				outv[outvoffset + i] = num & 255;
				num = (int)((uint)num >> 8);
			}
			num += a[aoffset + 31] - b[boffset + 31];
			outv[outvoffset + 31] = num;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000C50C File Offset: 0x0000A70C
		internal static void squeeze(int[] a, int aoffset)
		{
			int num = 0;
			for (int i = 0; i < 31; i++)
			{
				num += a[aoffset + i];
				a[aoffset + i] = num & 255;
				num = (int)((uint)num >> 8);
			}
			num += a[aoffset + 31];
			a[aoffset + 31] = num & 127;
			num = (int)(19U * ((uint)num >> 7));
			for (int j = 0; j < 31; j++)
			{
				num += a[aoffset + j];
				a[aoffset + j] = num & 255;
				num = (int)((uint)num >> 8);
			}
			num += a[aoffset + 31];
			a[aoffset + 31] = num;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000C590 File Offset: 0x0000A790
		internal static void freeze(int[] a, int aoffset)
		{
			int[] array = new int[32];
			for (int i = 0; i < 32; i++)
			{
				array[i] = a[aoffset + i];
			}
			int[] array2 = curve25519.minusp;
			curve25519.add(a, 0, a, 0, array2, 0);
			int num = (int)(-(int)(((uint)a[aoffset + 31] >> 7) & 1U));
			for (int j = 0; j < 32; j++)
			{
				a[aoffset + j] ^= num & (array[j] ^ a[aoffset + j]);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000C604 File Offset: 0x0000A804
		internal static void mult(int[] outv, int outvoffset, int[] a, int aoffset, int[] b, int boffset)
		{
			for (int i = 0; i < 32; i++)
			{
				int num = 0;
				for (int j = 0; j <= i; j++)
				{
					num += a[aoffset + j] * b[boffset + i - j];
				}
				for (int j = i + 1; j < 32; j++)
				{
					num += 38 * a[aoffset + j] * b[boffset + i + 32 - j];
				}
				outv[outvoffset + i] = num;
			}
			curve25519.squeeze(outv, outvoffset);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000C674 File Offset: 0x0000A874
		internal static void mult121665(int[] outv, int[] a)
		{
			int num = 0;
			int i;
			for (i = 0; i < 31; i++)
			{
				num += 121665 * a[i];
				outv[i] = num & 255;
				num = (int)((uint)num >> 8);
			}
			num += 121665 * a[31];
			outv[31] = num & 127;
			num = (int)(19U * ((uint)num >> 7));
			for (i = 0; i < 31; i++)
			{
				num += outv[i];
				outv[i] = num & 255;
				num = (int)((uint)num >> 8);
			}
			num += outv[i];
			outv[i] = num;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		internal static void square(int[] outv, int outvoffset, int[] a, int aoffset)
		{
			for (int i = 0; i < 32; i++)
			{
				int num = 0;
				for (int j = 0; j < i - j; j++)
				{
					num += a[aoffset + j] * a[aoffset + i - j];
				}
				for (int j = i + 1; j < i + 32 - j; j++)
				{
					num += 38 * a[aoffset + j] * a[aoffset + i + 32 - j];
				}
				num *= 2;
				if ((i & 1) == 0)
				{
					num += a[aoffset + i / 2] * a[aoffset + i / 2];
					num += 38 * a[aoffset + i / 2 + 16] * a[aoffset + i / 2 + 16];
				}
				outv[outvoffset + i] = num;
			}
			curve25519.squeeze(outv, outvoffset);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		internal static void select(int[] p, int[] q, int[] r, int[] s, int b)
		{
			int num = b - 1;
			for (int i = 0; i < 64; i++)
			{
				int num2 = num & (r[i] ^ s[i]);
				p[i] = s[i] ^ num2;
				q[i] = r[i] ^ num2;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		internal static void mainloop(int[] work, byte[] e)
		{
			int[] array = new int[64];
			int[] array2 = new int[64];
			int[] array3 = new int[64];
			int[] array4 = new int[64];
			int[] array5 = new int[64];
			int[] array6 = new int[64];
			int[] array7 = new int[64];
			int[] array8 = new int[64];
			int[] array9 = new int[64];
			int[] array10 = new int[64];
			int[] array11 = new int[64];
			int[] array12 = new int[32];
			int[] array13 = new int[32];
			int[] array14 = new int[32];
			int[] array15 = new int[32];
			for (int i = 0; i < 32; i++)
			{
				array[i] = work[i];
			}
			array[32] = 1;
			for (int j = 33; j < 64; j++)
			{
				array[j] = 0;
			}
			array2[0] = 1;
			for (int k = 1; k < 64; k++)
			{
				array2[k] = 0;
			}
			int[] array16 = array3;
			int[] array17 = array7;
			int[] array18 = array4;
			int[] array19 = array8;
			int[] array20 = array9;
			int[] array21 = array10;
			int[] array22 = array11;
			int[] array23 = array5;
			int[] array24 = array15;
			int[] array25 = array6;
			int[] array26 = array13;
			int[] array27 = array12;
			for (int l = 254; l >= 0; l--)
			{
				int num = (int)((uint)(e[l / 8] & byte.MaxValue) >> (l & 7));
				num &= 1;
				curve25519.select(array3, array4, array2, array, num);
				curve25519.add(array7, 0, array3, 0, array16, 32);
				curve25519.sub(array17, 32, array3, 0, array16, 32);
				curve25519.add(array8, 0, array4, 0, array18, 32);
				curve25519.sub(array19, 32, array4, 0, array18, 32);
				curve25519.square(array20, 0, array17, 0);
				curve25519.square(array20, 32, array17, 32);
				curve25519.mult(array21, 0, array19, 0, array17, 32);
				curve25519.mult(array21, 32, array19, 32, array17, 0);
				curve25519.add(array11, 0, array10, 0, array21, 32);
				curve25519.sub(array22, 32, array10, 0, array21, 32);
				curve25519.square(array27, 0, array22, 32);
				curve25519.sub(array26, 0, array9, 0, array20, 32);
				curve25519.mult121665(array14, array13);
				curve25519.add(array15, 0, array14, 0, array20, 0);
				curve25519.mult(array23, 0, array20, 0, array20, 32);
				curve25519.mult(array23, 32, array26, 0, array24, 0);
				curve25519.square(array25, 0, array22, 0);
				curve25519.mult(array25, 32, array27, 0, work, 0);
				curve25519.select(array2, array, array5, array6, num);
			}
			for (int m = 0; m < 64; m++)
			{
				work[m] = array2[m];
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000CA50 File Offset: 0x0000AC50
		internal static void recip(int[] outv, int outvoffset, int[] z, int zoffset)
		{
			int[] array = new int[32];
			int[] array2 = new int[32];
			int[] array3 = new int[32];
			int[] array4 = new int[32];
			int[] array5 = new int[32];
			int[] array6 = new int[32];
			int[] array7 = new int[32];
			int[] array8 = new int[32];
			int[] array9 = new int[32];
			int[] array10 = new int[32];
			curve25519.square(array, 0, z, zoffset);
			curve25519.square(array10, 0, array, 0);
			curve25519.square(array9, 0, array10, 0);
			int[] array11 = array2;
			int[] array12 = array9;
			curve25519.mult(array11, 0, array12, 0, z, zoffset);
			curve25519.mult(array3, 0, array2, 0, array, 0);
			curve25519.square(array9, 0, array3, 0);
			curve25519.mult(array4, 0, array9, 0, array2, 0);
			curve25519.square(array9, 0, array4, 0);
			curve25519.square(array10, 0, array9, 0);
			curve25519.square(array9, 0, array10, 0);
			curve25519.square(array10, 0, array9, 0);
			curve25519.square(array9, 0, array10, 0);
			curve25519.mult(array5, 0, array9, 0, array4, 0);
			curve25519.square(array9, 0, array5, 0);
			curve25519.square(array10, 0, array9, 0);
			for (int i = 2; i < 10; i += 2)
			{
				curve25519.square(array9, 0, array10, 0);
				curve25519.square(array10, 0, array9, 0);
			}
			curve25519.mult(array6, 0, array10, 0, array5, 0);
			curve25519.square(array9, 0, array6, 0);
			curve25519.square(array10, 0, array9, 0);
			for (int j = 2; j < 20; j += 2)
			{
				curve25519.square(array9, 0, array10, 0);
				curve25519.square(array10, 0, array9, 0);
			}
			curve25519.mult(array9, 0, array10, 0, array6, 0);
			curve25519.square(array10, 0, array9, 0);
			curve25519.square(array9, 0, array10, 0);
			for (int k = 2; k < 10; k += 2)
			{
				curve25519.square(array10, 0, array9, 0);
				curve25519.square(array9, 0, array10, 0);
			}
			curve25519.mult(array7, 0, array9, 0, array5, 0);
			curve25519.square(array9, 0, array7, 0);
			curve25519.square(array10, 0, array9, 0);
			for (int l = 2; l < 50; l += 2)
			{
				curve25519.square(array9, 0, array10, 0);
				curve25519.square(array10, 0, array9, 0);
			}
			curve25519.mult(array8, 0, array10, 0, array7, 0);
			curve25519.square(array10, 0, array8, 0);
			curve25519.square(array9, 0, array10, 0);
			for (int m = 2; m < 100; m += 2)
			{
				curve25519.square(array10, 0, array9, 0);
				curve25519.square(array9, 0, array10, 0);
			}
			curve25519.mult(array10, 0, array9, 0, array8, 0);
			curve25519.square(array9, 0, array10, 0);
			curve25519.square(array10, 0, array9, 0);
			for (int n = 2; n < 50; n += 2)
			{
				curve25519.square(array9, 0, array10, 0);
				curve25519.square(array10, 0, array9, 0);
			}
			curve25519.mult(array9, 0, array10, 0, array7, 0);
			curve25519.square(array10, 0, array9, 0);
			curve25519.square(array9, 0, array10, 0);
			curve25519.square(array10, 0, array9, 0);
			curve25519.square(array9, 0, array10, 0);
			curve25519.square(array10, 0, array9, 0);
			int[] array13 = array10;
			int[] array14 = array3;
			curve25519.mult(outv, outvoffset, array13, 0, array14, 0);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000CD54 File Offset: 0x0000AF54
		public static int crypto_scalarmult(byte[] q, byte[] n, byte[] p)
		{
			int[] array = new int[96];
			byte[] array2 = new byte[32];
			for (int i = 0; i < 32; i++)
			{
				array2[i] = n[i];
			}
			byte[] array3 = array2;
			int num = 0;
			array3[num] &= 248;
			byte[] array4 = array2;
			int num2 = 31;
			array4[num2] &= 127;
			byte[] array5 = array2;
			int num3 = 31;
			array5[num3] |= 64;
			for (int j = 0; j < 32; j++)
			{
				array[j] = (int)(p[j] & byte.MaxValue);
			}
			curve25519.mainloop(array, array2);
			curve25519.recip(array, 32, array, 32);
			curve25519.mult(array, 64, array, 0, array, 32);
			curve25519.freeze(array, 64);
			for (int k = 0; k < 32; k++)
			{
				q[k] = (byte)array[64 + k];
			}
			return 0;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000CE29 File Offset: 0x0000B029
		// Note: this type is marked as 'beforefieldinit'.
		static curve25519()
		{
			byte[] array = new byte[32];
			array[0] = 9;
			curve25519.basev = array;
			curve25519.minusp = new int[]
			{
				19, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 128
			};
		}

		// Token: 0x04000178 RID: 376
		internal readonly int CRYPTO_BYTES = 32;

		// Token: 0x04000179 RID: 377
		internal readonly int CRYPTO_SCALARBYTES = 32;

		// Token: 0x0400017A RID: 378
		internal static byte[] basev;

		// Token: 0x0400017B RID: 379
		internal static int[] minusp;
	}
}

using System;

namespace CRS.Utilities.ZLib
{
	// Token: 0x0200000F RID: 15
	internal sealed class Tree
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00008F03 File Offset: 0x00007103
		internal static int DistanceCode(int dist)
		{
			if (dist >= 256)
			{
				return (int)Tree._dist_code[256 + SharedUtils.URShift(dist, 7)];
			}
			return (int)Tree._dist_code[dist];
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00008F28 File Offset: 0x00007128
		internal void gen_bitlen(DeflateManager s)
		{
			short[] array = this.dyn_tree;
			short[] treeCodes = this.staticTree.treeCodes;
			int[] extraBits = this.staticTree.extraBits;
			int extraBase = this.staticTree.extraBase;
			int maxLength = this.staticTree.maxLength;
			int num = 0;
			for (int i = 0; i <= InternalConstants.MAX_BITS; i++)
			{
				s.bl_count[i] = 0;
			}
			array[s.heap[s.heap_max] * 2 + 1] = 0;
			int j;
			for (j = s.heap_max + 1; j < Tree.HEAP_SIZE; j++)
			{
				int num2 = s.heap[j];
				int i = (int)(array[(int)(array[num2 * 2 + 1] * 2 + 1)] + 1);
				if (i > maxLength)
				{
					i = maxLength;
					num++;
				}
				array[num2 * 2 + 1] = (short)i;
				if (num2 <= this.max_code)
				{
					short[] bl_count = s.bl_count;
					int num3 = i;
					bl_count[num3] += 1;
					int num4 = 0;
					if (num2 >= extraBase)
					{
						num4 = extraBits[num2 - extraBase];
					}
					short num5 = array[num2 * 2];
					s.opt_len += (int)num5 * (i + num4);
					if (treeCodes != null)
					{
						s.static_len += (int)num5 * ((int)treeCodes[num2 * 2 + 1] + num4);
					}
				}
			}
			if (num == 0)
			{
				return;
			}
			do
			{
				int i = maxLength - 1;
				while (s.bl_count[i] == 0)
				{
					i--;
				}
				short[] bl_count2 = s.bl_count;
				int num6 = i;
				bl_count2[num6] -= 1;
				s.bl_count[i + 1] = (short)(s.bl_count[i + 1] + 2);
				short[] bl_count3 = s.bl_count;
				int num7 = maxLength;
				bl_count3[num7] -= 1;
				num -= 2;
			}
			while (num > 0);
			for (int i = maxLength; i != 0; i--)
			{
				int num2 = (int)s.bl_count[i];
				while (num2 != 0)
				{
					int num8 = s.heap[--j];
					if (num8 <= this.max_code)
					{
						if ((int)array[num8 * 2 + 1] != i)
						{
							s.opt_len = (int)((long)s.opt_len + ((long)i - (long)array[num8 * 2 + 1]) * (long)array[num8 * 2]);
							array[num8 * 2 + 1] = (short)i;
						}
						num2--;
					}
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00009148 File Offset: 0x00007348
		internal void build_tree(DeflateManager s)
		{
			short[] array = this.dyn_tree;
			short[] treeCodes = this.staticTree.treeCodes;
			int elems = this.staticTree.elems;
			int num = -1;
			s.heap_len = 0;
			s.heap_max = Tree.HEAP_SIZE;
			int num2;
			for (int i = 0; i < elems; i++)
			{
				if (array[i * 2] != 0)
				{
					int[] heap = s.heap;
					num2 = s.heap_len + 1;
					s.heap_len = num2;
					num = (heap[num2] = i);
					s.depth[i] = 0;
				}
				else
				{
					array[i * 2 + 1] = 0;
				}
			}
			int num3;
			while (s.heap_len < 2)
			{
				int[] heap2 = s.heap;
				num2 = s.heap_len + 1;
				s.heap_len = num2;
				num3 = (heap2[num2] = ((num < 2) ? (++num) : 0));
				array[num3 * 2] = 1;
				s.depth[num3] = 0;
				s.opt_len--;
				if (treeCodes != null)
				{
					s.static_len -= (int)treeCodes[num3 * 2 + 1];
				}
			}
			this.max_code = num;
			for (int i = s.heap_len / 2; i >= 1; i--)
			{
				s.pqdownheap(array, i);
			}
			num3 = elems;
			do
			{
				int i = s.heap[1];
				int[] heap3 = s.heap;
				int num4 = 1;
				int[] heap4 = s.heap;
				num2 = s.heap_len;
				s.heap_len = num2 - 1;
				heap3[num4] = heap4[num2];
				s.pqdownheap(array, 1);
				int num5 = s.heap[1];
				int[] heap5 = s.heap;
				num2 = s.heap_max - 1;
				s.heap_max = num2;
				heap5[num2] = i;
				int[] heap6 = s.heap;
				num2 = s.heap_max - 1;
				s.heap_max = num2;
				heap6[num2] = num5;
				array[num3 * 2] = (short)(array[i * 2] + array[num5 * 2]);
				s.depth[num3] = (sbyte)(Math.Max((byte)s.depth[i], (byte)s.depth[num5]) + 1);
				array[i * 2 + 1] = (array[num5 * 2 + 1] = (short)num3);
				s.heap[1] = num3++;
				s.pqdownheap(array, 1);
			}
			while (s.heap_len >= 2);
			int[] heap7 = s.heap;
			num2 = s.heap_max - 1;
			s.heap_max = num2;
			heap7[num2] = s.heap[1];
			this.gen_bitlen(s);
			Tree.gen_codes(array, num, s.bl_count);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00009388 File Offset: 0x00007588
		internal static void gen_codes(short[] tree, int max_code, short[] bl_count)
		{
			short[] array = new short[InternalConstants.MAX_BITS + 1];
			short num = 0;
			for (int i = 1; i <= InternalConstants.MAX_BITS; i++)
			{
				num = (array[i] = (short)(num + bl_count[i - 1] << 1));
			}
			for (int j = 0; j <= max_code; j++)
			{
				int num2 = (int)tree[j * 2 + 1];
				if (num2 != 0)
				{
					int num3 = j * 2;
					short[] array2 = array;
					int num4 = num2;
					short num5 = array2[num4];
					array2[num4] = (short)(num5 + 1);
					tree[num3] = (short)Tree.bi_reverse((int)num5, num2);
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00009400 File Offset: 0x00007600
		internal static int bi_reverse(int code, int len)
		{
			int num = 0;
			do
			{
				num |= code & 1;
				code >>= 1;
				num <<= 1;
			}
			while (--len > 0);
			return num >> 1;
		}

		// Token: 0x040000D4 RID: 212
		internal const int Buf_size = 16;

		// Token: 0x040000D5 RID: 213
		private static readonly int HEAP_SIZE = 2 * InternalConstants.L_CODES + 1;

		// Token: 0x040000D6 RID: 214
		internal static readonly int[] ExtraLengthBits = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0
		};

		// Token: 0x040000D7 RID: 215
		internal static readonly int[] ExtraDistanceBits = new int[]
		{
			0, 0, 0, 0, 1, 1, 2, 2, 3, 3,
			4, 4, 5, 5, 6, 6, 7, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 13, 13
		};

		// Token: 0x040000D8 RID: 216
		internal static readonly int[] extra_blbits = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 2, 3, 7
		};

		// Token: 0x040000D9 RID: 217
		internal static readonly sbyte[] bl_order = new sbyte[]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		// Token: 0x040000DA RID: 218
		private static readonly sbyte[] _dist_code = new sbyte[]
		{
			0, 1, 2, 3, 4, 4, 5, 5, 6, 6,
			6, 6, 7, 7, 7, 7, 8, 8, 8, 8,
			8, 8, 8, 8, 9, 9, 9, 9, 9, 9,
			9, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 0, 0, 16, 17,
			18, 18, 19, 19, 20, 20, 20, 20, 21, 21,
			21, 21, 22, 22, 22, 22, 22, 22, 22, 22,
			23, 23, 23, 23, 23, 23, 23, 23, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29
		};

		// Token: 0x040000DB RID: 219
		internal static readonly sbyte[] LengthCode = new sbyte[]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 12, 12,
			13, 13, 13, 13, 14, 14, 14, 14, 15, 15,
			15, 15, 16, 16, 16, 16, 16, 16, 16, 16,
			17, 17, 17, 17, 17, 17, 17, 17, 18, 18,
			18, 18, 18, 18, 18, 18, 19, 19, 19, 19,
			19, 19, 19, 19, 20, 20, 20, 20, 20, 20,
			20, 20, 20, 20, 20, 20, 20, 20, 20, 20,
			21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
			21, 21, 21, 21, 21, 21, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 28
		};

		// Token: 0x040000DC RID: 220
		internal static readonly int[] LengthBase = new int[]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 10,
			12, 14, 16, 20, 24, 28, 32, 40, 48, 56,
			64, 80, 96, 112, 128, 160, 192, 224, 0
		};

		// Token: 0x040000DD RID: 221
		internal static readonly int[] DistanceBase = new int[]
		{
			0, 1, 2, 3, 4, 6, 8, 12, 16, 24,
			32, 48, 64, 96, 128, 192, 256, 384, 512, 768,
			1024, 1536, 2048, 3072, 4096, 6144, 8192, 12288, 16384, 24576
		};

		// Token: 0x040000DE RID: 222
		internal short[] dyn_tree;

		// Token: 0x040000DF RID: 223
		internal int max_code;

		// Token: 0x040000E0 RID: 224
		internal StaticTree staticTree;
	}
}
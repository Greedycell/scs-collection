using System;

namespace CRS.Utilities.ZLib
{
	// Token: 0x02000008 RID: 8
	internal sealed class InflateBlocks
	{
		// Token: 0x06000071 RID: 113 RVA: 0x0000505C File Offset: 0x0000325C
		internal InflateBlocks(ZlibCodec codec, object checkfn, int w)
		{
			this._codec = codec;
			this.hufts = new int[4320];
			this.window = new byte[w];
			this.end = w;
			this.checkfn = checkfn;
			this.mode = InflateBlocks.InflateBlockMode.TYPE;
			this.Reset();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000050DC File Offset: 0x000032DC
		internal uint Reset()
		{
			uint num = this.check;
			this.mode = InflateBlocks.InflateBlockMode.TYPE;
			this.bitk = 0;
			this.bitb = 0;
			this.readAt = (this.writeAt = 0);
			if (this.checkfn != null)
			{
				this._codec._Adler32 = (this.check = Adler.Adler32(0U, null, 0, 0));
			}
			return num;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000513C File Offset: 0x0000333C
		internal int Process(int r)
		{
			int num = this._codec.NextIn;
			int num2 = this._codec.AvailableBytesIn;
			int num3 = this.bitb;
			int i = this.bitk;
			int num4 = this.writeAt;
			int num5 = ((num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4));
			int num6;
			for (; ; )
			{
				switch (this.mode)
				{
					case InflateBlocks.InflateBlockMode.TYPE:
						while (i < 3)
						{
							if (num2 == 0)
							{
								goto IL_0096;
							}
							r = 0;
							num2--;
							num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
							i += 8;
						}
						num6 = num3 & 7;
						this.last = num6 & 1;
						switch ((uint)num6 >> 1)
						{
							case 0U:
								num3 >>= 3;
								i -= 3;
								num6 = i & 7;
								num3 >>= num6;
								i -= num6;
								this.mode = InflateBlocks.InflateBlockMode.LENS;
								continue;
							case 1U:
								{
									int[] array = new int[1];
									int[] array2 = new int[1];
									int[][] array3 = new int[1][];
									int[][] array4 = new int[1][];
									InfTree.inflate_trees_fixed(array, array2, array3, array4, this._codec);
									this.codes.Init(array[0], array2[0], array3[0], 0, array4[0], 0);
									num3 >>= 3;
									i -= 3;
									this.mode = InflateBlocks.InflateBlockMode.CODES;
									continue;
								}
							case 2U:
								num3 >>= 3;
								i -= 3;
								this.mode = InflateBlocks.InflateBlockMode.TABLE;
								continue;
							case 3U:
								goto IL_01E7;
							default:
								continue;
						}
						break;
					case InflateBlocks.InflateBlockMode.LENS:
						while (i < 32)
						{
							if (num2 == 0)
							{
								goto IL_026B;
							}
							r = 0;
							num2--;
							num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
							i += 8;
						}
						if (((~num3 >> 16) & 65535) != (num3 & 65535))
						{
							goto Block_8;
						}
						this.left = num3 & 65535;
						i = (num3 = 0);
						this.mode = ((this.left != 0) ? InflateBlocks.InflateBlockMode.STORED : ((this.last != 0) ? InflateBlocks.InflateBlockMode.DRY : InflateBlocks.InflateBlockMode.TYPE));
						continue;
					case InflateBlocks.InflateBlockMode.STORED:
						if (num2 == 0)
						{
							goto Block_11;
						}
						if (num5 == 0)
						{
							if (num4 == this.end && this.readAt != 0)
							{
								num4 = 0;
								num5 = ((num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4));
							}
							if (num5 == 0)
							{
								this.writeAt = num4;
								r = this.Flush(r);
								num4 = this.writeAt;
								num5 = ((num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4));
								if (num4 == this.end && this.readAt != 0)
								{
									num4 = 0;
									num5 = ((num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4));
								}
								if (num5 == 0)
								{
									goto Block_21;
								}
							}
						}
						r = 0;
						num6 = this.left;
						if (num6 > num2)
						{
							num6 = num2;
						}
						if (num6 > num5)
						{
							num6 = num5;
						}
						Array.Copy(this._codec.InputBuffer, num, this.window, num4, num6);
						num += num6;
						num2 -= num6;
						num4 += num6;
						num5 -= num6;
						if ((this.left -= num6) == 0)
						{
							this.mode = ((this.last != 0) ? InflateBlocks.InflateBlockMode.DRY : InflateBlocks.InflateBlockMode.TYPE);
							continue;
						}
						continue;
					case InflateBlocks.InflateBlockMode.TABLE:
						while (i < 14)
						{
							if (num2 == 0)
							{
								goto IL_059C;
							}
							r = 0;
							num2--;
							num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
							i += 8;
						}
						num6 = (this.table = num3 & 16383);
						if ((num6 & 31) > 29 || ((num6 >> 5) & 31) > 29)
						{
							goto IL_0645;
						}
						num6 = 258 + (num6 & 31) + ((num6 >> 5) & 31);
						if (this.blens == null || this.blens.Length < num6)
						{
							this.blens = new int[num6];
						}
						else
						{
							Array.Clear(this.blens, 0, num6);
						}
						num3 >>= 14;
						i -= 14;
						this.index = 0;
						this.mode = InflateBlocks.InflateBlockMode.BTREE;
						goto IL_07D1;
					case InflateBlocks.InflateBlockMode.BTREE:
						goto IL_07D1;
					case InflateBlocks.InflateBlockMode.DTREE:
						goto IL_08C4;
					case InflateBlocks.InflateBlockMode.CODES:
						goto IL_0CC9;
					case InflateBlocks.InflateBlockMode.DRY:
						goto IL_0DA2;
					case InflateBlocks.InflateBlockMode.DONE:
						goto IL_0E49;
					case InflateBlocks.InflateBlockMode.BAD:
						goto IL_0EA3;
				}
				break;
			IL_07D1:
				while (this.index < 4 + (this.table >> 10))
				{
					while (i < 3)
					{
						if (num2 == 0)
						{
							goto IL_071E;
						}
						r = 0;
						num2--;
						num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					int[] array5 = this.blens;
					int[] array6 = InflateBlocks.border;
					int num7 = this.index;
					this.index = num7 + 1;
					array5[array6[num7]] = num3 & 7;
					num3 >>= 3;
					i -= 3;
				}
				while (this.index < 19)
				{
					int[] array7 = this.blens;
					int[] array8 = InflateBlocks.border;
					int num7 = this.index;
					this.index = num7 + 1;
					array7[array8[num7]] = 0;
				}
				this.bb[0] = 7;
				num6 = this.inftree.inflate_trees_bits(this.blens, this.bb, this.tb, this.hufts, this._codec);
				if (num6 != 0)
				{
					goto Block_34;
				}
				this.index = 0;
				this.mode = InflateBlocks.InflateBlockMode.DTREE;
			IL_08C4:
				for (; ; )
				{
					num6 = this.table;
					num6 = this.table;
					if (this.index >= 258 + (num6 & 31) + ((num6 >> 5) & 31))
					{
						break;
					}
					num6 = this.bb[0];
					while (i < num6)
					{
						if (num2 == 0)
						{
							goto IL_08FE;
						}
						r = 0;
						num2--;
						num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					num6 = this.hufts[(this.tb[0] + (num3 & InternalInflateConstants.InflateMask[num6])) * 3 + 1];
					int num8 = this.hufts[(this.tb[0] + (num3 & InternalInflateConstants.InflateMask[num6])) * 3 + 2];
					if (num8 < 16)
					{
						num3 >>= num6;
						i -= num6;
						int[] array9 = this.blens;
						int num7 = this.index;
						this.index = num7 + 1;
						array9[num7] = num8;
					}
					else
					{
						int num9 = ((num8 == 18) ? 7 : (num8 - 14));
						int num10 = ((num8 == 18) ? 11 : 3);
						while (i < num6 + num9)
						{
							if (num2 == 0)
							{
								goto IL_0A20;
							}
							r = 0;
							num2--;
							num3 |= (int)(this._codec.InputBuffer[num++] & byte.MaxValue) << i;
							i += 8;
						}
						num3 >>= num6;
						i -= num6;
						num10 += num3 & InternalInflateConstants.InflateMask[num9];
						num3 >>= num9;
						i -= num9;
						num9 = this.index;
						num6 = this.table;
						if (num9 + num10 > 258 + (num6 & 31) + ((num6 >> 5) & 31) || (num8 == 16 && num9 < 1))
						{
							goto IL_0B03;
						}
						num8 = ((num8 == 16) ? this.blens[num9 - 1] : 0);
						do
						{
							this.blens[num9++] = num8;
						}
						while (--num10 != 0);
						this.index = num9;
					}
				}
				this.tb[0] = -1;
				int[] array10 = new int[] { 9 };
				int[] array11 = new int[] { 6 };
				int[] array12 = new int[1];
				int[] array13 = new int[1];
				num6 = this.table;
				num6 = this.inftree.inflate_trees_dynamic(257 + (num6 & 31), 1 + ((num6 >> 5) & 31), this.blens, array10, array11, array12, array13, this.hufts, this._codec);
				if (num6 != 0)
				{
					goto Block_48;
				}
				this.codes.Init(array10[0], array11[0], this.hufts, array12[0], this.hufts, array13[0]);
				this.mode = InflateBlocks.InflateBlockMode.CODES;
			IL_0CC9:
				this.bitb = num3;
				this.bitk = i;
				this._codec.AvailableBytesIn = num2;
				this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
				this._codec.NextIn = num;
				this.writeAt = num4;
				r = this.codes.Process(this, r);
				if (r != 1)
				{
					goto Block_50;
				}
				r = 0;
				num = this._codec.NextIn;
				num2 = this._codec.AvailableBytesIn;
				num3 = this.bitb;
				i = this.bitk;
				num4 = this.writeAt;
				num5 = ((num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4));
				if (this.last != 0)
				{
					goto IL_0D9B;
				}
				this.mode = InflateBlocks.InflateBlockMode.TYPE;
			}
			r = -2;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		IL_0096:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		IL_01E7:
			num3 >>= 3;
			i -= 3;
			this.mode = InflateBlocks.InflateBlockMode.BAD;
			this._codec.Message = "invalid block type";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		IL_026B:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		Block_8:
			this.mode = InflateBlocks.InflateBlockMode.BAD;
			this._codec.Message = "invalid stored block lengths";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		Block_11:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		Block_21:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		IL_059C:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		IL_0645:
			this.mode = InflateBlocks.InflateBlockMode.BAD;
			this._codec.Message = "too many length or distance symbols";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		IL_071E:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		Block_34:
			r = num6;
			if (r == -3)
			{
				this.blens = null;
				this.mode = InflateBlocks.InflateBlockMode.BAD;
			}
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		IL_08FE:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		IL_0A20:
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		IL_0B03:
			this.blens = null;
			this.mode = InflateBlocks.InflateBlockMode.BAD;
			this._codec.Message = "invalid bit length repeat";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		Block_48:
			if (num6 == -3)
			{
				this.blens = null;
				this.mode = InflateBlocks.InflateBlockMode.BAD;
			}
			r = num6;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		Block_50:
			return this.Flush(r);
		IL_0D9B:
			this.mode = InflateBlocks.InflateBlockMode.DRY;
		IL_0DA2:
			this.writeAt = num4;
			r = this.Flush(r);
			num4 = this.writeAt;
			int num11 = ((num4 < this.readAt) ? (this.readAt - num4 - 1) : (this.end - num4));
			if (this.readAt != this.writeAt)
			{
				this.bitb = num3;
				this.bitk = i;
				this._codec.AvailableBytesIn = num2;
				this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
				this._codec.NextIn = num;
				this.writeAt = num4;
				return this.Flush(r);
			}
			this.mode = InflateBlocks.InflateBlockMode.DONE;
		IL_0E49:
			r = 1;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		IL_0EA3:
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			this._codec.AvailableBytesIn = num2;
			this._codec.TotalBytesIn += (long)(num - this._codec.NextIn);
			this._codec.NextIn = num;
			this.writeAt = num4;
			return this.Flush(r);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000060A1 File Offset: 0x000042A1
		internal void Free()
		{
			this.Reset();
			this.window = null;
			this.hufts = null;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000060B8 File Offset: 0x000042B8
		internal void SetDictionary(byte[] d, int start, int n)
		{
			Array.Copy(d, start, this.window, 0, n);
			this.writeAt = n;
			this.readAt = n;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000060E4 File Offset: 0x000042E4
		internal int SyncPoint()
		{
			if (this.mode != InflateBlocks.InflateBlockMode.LENS)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000060F4 File Offset: 0x000042F4
		internal int Flush(int r)
		{
			for (int i = 0; i < 2; i++)
			{
				int num;
				if (i == 0)
				{
					num = ((this.readAt <= this.writeAt) ? this.writeAt : this.end) - this.readAt;
				}
				else
				{
					num = this.writeAt - this.readAt;
				}
				if (num == 0)
				{
					if (r == -5)
					{
						r = 0;
					}
					return r;
				}
				if (num > this._codec.AvailableBytesOut)
				{
					num = this._codec.AvailableBytesOut;
				}
				if (num != 0 && r == -5)
				{
					r = 0;
				}
				this._codec.AvailableBytesOut -= num;
				this._codec.TotalBytesOut += (long)num;
				if (this.checkfn != null)
				{
					this._codec._Adler32 = (this.check = Adler.Adler32(this.check, this.window, this.readAt, num));
				}
				Array.Copy(this.window, this.readAt, this._codec.OutputBuffer, this._codec.NextOut, num);
				this._codec.NextOut += num;
				this.readAt += num;
				if (this.readAt == this.end && i == 0)
				{
					this.readAt = 0;
					if (this.writeAt == this.end)
					{
						this.writeAt = 0;
					}
				}
				else
				{
					i++;
				}
			}
			return r;
		}

		// Token: 0x04000061 RID: 97
		private const int MANY = 1440;

		// Token: 0x04000062 RID: 98
		internal static readonly int[] border = new int[]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		// Token: 0x04000063 RID: 99
		internal ZlibCodec _codec;

		// Token: 0x04000064 RID: 100
		internal int[] bb = new int[1];

		// Token: 0x04000065 RID: 101
		internal int bitb;

		// Token: 0x04000066 RID: 102
		internal int bitk;

		// Token: 0x04000067 RID: 103
		internal int[] blens;

		// Token: 0x04000068 RID: 104
		internal uint check;

		// Token: 0x04000069 RID: 105
		internal object checkfn;

		// Token: 0x0400006A RID: 106
		internal InflateCodes codes = new InflateCodes();

		// Token: 0x0400006B RID: 107
		internal int end;

		// Token: 0x0400006C RID: 108
		internal int[] hufts;

		// Token: 0x0400006D RID: 109
		internal int index;

		// Token: 0x0400006E RID: 110
		internal InfTree inftree = new InfTree();

		// Token: 0x0400006F RID: 111
		internal int last;

		// Token: 0x04000070 RID: 112
		internal int left;

		// Token: 0x04000071 RID: 113
		private InflateBlocks.InflateBlockMode mode;

		// Token: 0x04000072 RID: 114
		internal int readAt;

		// Token: 0x04000073 RID: 115
		internal int table;

		// Token: 0x04000074 RID: 116
		internal int[] tb = new int[1];

		// Token: 0x04000075 RID: 117
		internal byte[] window;

		// Token: 0x04000076 RID: 118
		internal int writeAt;

		// Token: 0x020000C8 RID: 200
		private enum InflateBlockMode
		{
			// Token: 0x04000368 RID: 872
			BAD = 9,
			// Token: 0x04000369 RID: 873
			BTREE = 4,
			// Token: 0x0400036A RID: 874
			CODES = 6,
			// Token: 0x0400036B RID: 875
			DONE = 8,
			// Token: 0x0400036C RID: 876
			DRY = 7,
			// Token: 0x0400036D RID: 877
			DTREE = 5,
			// Token: 0x0400036E RID: 878
			LENS = 1,
			// Token: 0x0400036F RID: 879
			STORED,
			// Token: 0x04000370 RID: 880
			TABLE,
			// Token: 0x04000371 RID: 881
			TYPE = 0
		}
	}
}
using System;

namespace CRS.Utilities.ZLib
{
	// Token: 0x02000017 RID: 23
	internal sealed class DeflateManager
	{
		// Token: 0x0600009E RID: 158 RVA: 0x000037CC File Offset: 0x000019CC
		internal DeflateManager()
		{
			this.dyn_ltree = new short[DeflateManager.HEAP_SIZE * 2];
			this.dyn_dtree = new short[(2 * InternalConstants.D_CODES + 1) * 2];
			this.bl_tree = new short[(2 * InternalConstants.BL_CODES + 1) * 2];
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003880 File Offset: 0x00001A80
		private void _InitializeLazyMatch()
		{
			this.window_size = 2 * this.w_size;
			Array.Clear(this.head, 0, this.hash_size);
			this.config = DeflateManager.Config.Lookup(this.compressionLevel);
			this.SetDeflater();
			this.strstart = 0;
			this.block_start = 0;
			this.lookahead = 0;
			this.match_length = (this.prev_length = DeflateManager.MIN_MATCH - 1);
			this.match_available = 0;
			this.ins_h = 0;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003900 File Offset: 0x00001B00
		private void _InitializeTreeData()
		{
			this.treeLiterals.dyn_tree = this.dyn_ltree;
			this.treeLiterals.staticTree = StaticTree.Literals;
			this.treeDistances.dyn_tree = this.dyn_dtree;
			this.treeDistances.staticTree = StaticTree.Distances;
			this.treeBitLengths.dyn_tree = this.bl_tree;
			this.treeBitLengths.staticTree = StaticTree.BitLengths;
			this.bi_buf = 0;
			this.bi_valid = 0;
			this.last_eob_len = 8;
			this._InitializeBlocks();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000398C File Offset: 0x00001B8C
		internal void _InitializeBlocks()
		{
			for (int i = 0; i < InternalConstants.L_CODES; i++)
			{
				this.dyn_ltree[i * 2] = 0;
			}
			for (int j = 0; j < InternalConstants.D_CODES; j++)
			{
				this.dyn_dtree[j * 2] = 0;
			}
			for (int k = 0; k < InternalConstants.BL_CODES; k++)
			{
				this.bl_tree[k * 2] = 0;
			}
			this.dyn_ltree[DeflateManager.END_BLOCK * 2] = 1;
			this.opt_len = (this.static_len = 0);
			this.last_lit = (this.matches = 0);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003A1C File Offset: 0x00001C1C
		internal void pqdownheap(short[] tree, int k)
		{
			int num = this.heap[k];
			for (int i = k << 1; i <= this.heap_len; i <<= 1)
			{
				if (i < this.heap_len && DeflateManager._IsSmaller(tree, this.heap[i + 1], this.heap[i], this.depth))
				{
					i++;
				}
				if (DeflateManager._IsSmaller(tree, num, this.heap[i], this.depth))
				{
					break;
				}
				this.heap[k] = this.heap[i];
				k = i;
			}
			this.heap[k] = num;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003AA8 File Offset: 0x00001CA8
		internal static bool _IsSmaller(short[] tree, int n, int m, sbyte[] depth)
		{
			short num = tree[n * 2];
			short num2 = tree[m * 2];
			return num < num2 || (num == num2 && depth[n] <= depth[m]);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003AD8 File Offset: 0x00001CD8
		internal void scan_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = (int)tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			tree[(max_code + 1) * 2 + 1] = short.MaxValue;
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = (int)tree[(i + 1) * 2 + 1];
				if (++num3 >= num4 || num6 != num2)
				{
					if (num3 < num5)
					{
						this.bl_tree[num6 * 2] = (short)((int)this.bl_tree[num6 * 2] + num3);
					}
					else if (num6 != 0)
					{
						if (num6 != num)
						{
							short[] array = this.bl_tree;
							int num7 = num6 * 2;
							array[num7] += 1;
						}
						short[] array2 = this.bl_tree;
						int num8 = InternalConstants.REP_3_6 * 2;
						array2[num8] += 1;
					}
					else if (num3 <= 10)
					{
						short[] array3 = this.bl_tree;
						int num9 = InternalConstants.REPZ_3_10 * 2;
						array3[num9] += 1;
					}
					else
					{
						short[] array4 = this.bl_tree;
						int num10 = InternalConstants.REPZ_11_138 * 2;
						array4[num10] += 1;
					}
					num3 = 0;
					num = num6;
					if (num2 == 0)
					{
						num4 = 138;
						num5 = 3;
					}
					else if (num6 == num2)
					{
						num4 = 6;
						num5 = 3;
					}
					else
					{
						num4 = 7;
						num5 = 4;
					}
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003BF4 File Offset: 0x00001DF4
		internal int build_bl_tree()
		{
			this.scan_tree(this.dyn_ltree, this.treeLiterals.max_code);
			this.scan_tree(this.dyn_dtree, this.treeDistances.max_code);
			this.treeBitLengths.build_tree(this);
			int num = InternalConstants.BL_CODES - 1;
			while (num >= 3 && this.bl_tree[(int)(Tree.bl_order[num] * 2 + 1)] == 0)
			{
				num--;
			}
			this.opt_len += 3 * (num + 1) + 5 + 5 + 4;
			return num;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003C7C File Offset: 0x00001E7C
		internal void send_all_trees(int lcodes, int dcodes, int blcodes)
		{
			this.send_bits(lcodes - 257, 5);
			this.send_bits(dcodes - 1, 5);
			this.send_bits(blcodes - 4, 4);
			for (int i = 0; i < blcodes; i++)
			{
				this.send_bits((int)this.bl_tree[(int)(Tree.bl_order[i] * 2 + 1)], 3);
			}
			this.send_tree(this.dyn_ltree, lcodes - 1);
			this.send_tree(this.dyn_dtree, dcodes - 1);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003CF0 File Offset: 0x00001EF0
		internal void send_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = (int)tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = (int)tree[(i + 1) * 2 + 1];
				if (++num3 >= num4 || num6 != num2)
				{
					if (num3 < num5)
					{
						do
						{
							this.send_code(num6, this.bl_tree);
						}
						while (--num3 != 0);
					}
					else if (num6 != 0)
					{
						if (num6 != num)
						{
							this.send_code(num6, this.bl_tree);
							num3--;
						}
						this.send_code(InternalConstants.REP_3_6, this.bl_tree);
						this.send_bits(num3 - 3, 2);
					}
					else if (num3 <= 10)
					{
						this.send_code(InternalConstants.REPZ_3_10, this.bl_tree);
						this.send_bits(num3 - 3, 3);
					}
					else
					{
						this.send_code(InternalConstants.REPZ_11_138, this.bl_tree);
						this.send_bits(num3 - 11, 7);
					}
					num3 = 0;
					num = num6;
					if (num2 == 0)
					{
						num4 = 138;
						num5 = 3;
					}
					else if (num6 == num2)
					{
						num4 = 6;
						num5 = 3;
					}
					else
					{
						num4 = 7;
						num5 = 4;
					}
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003E06 File Offset: 0x00002006
		private void put_bytes(byte[] p, int start, int len)
		{
			Array.Copy(p, start, this.pending, this.pendingCount, len);
			this.pendingCount += len;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003E2C File Offset: 0x0000202C
		internal void send_code(int c, short[] tree)
		{
			int num = c * 2;
			this.send_bits((int)tree[num] & 65535, (int)tree[num + 1] & 65535);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003E58 File Offset: 0x00002058
		internal void send_bits(int value, int length)
		{
			if (this.bi_valid > DeflateManager.Buf_size - length)
			{
				this.bi_buf |= (short)((value << this.bi_valid) & 65535);
				byte[] array = this.pending;
				int num = this.pendingCount;
				this.pendingCount = num + 1;
				array[num] = (byte)this.bi_buf;
				byte[] array2 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array2[num] = (byte)(this.bi_buf >> 8);
				this.bi_buf = (short)((uint)value >> DeflateManager.Buf_size - this.bi_valid);
				this.bi_valid += length - DeflateManager.Buf_size;
				return;
			}
			this.bi_buf |= (short)((value << this.bi_valid) & 65535);
			this.bi_valid += length;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003F34 File Offset: 0x00002134
		internal void _tr_align()
		{
			this.send_bits(DeflateManager.STATIC_TREES << 1, 3);
			this.send_code(DeflateManager.END_BLOCK, StaticTree.lengthAndLiteralsTreeCodes);
			this.bi_flush();
			if (1 + this.last_eob_len + 10 - this.bi_valid < 9)
			{
				this.send_bits(DeflateManager.STATIC_TREES << 1, 3);
				this.send_code(DeflateManager.END_BLOCK, StaticTree.lengthAndLiteralsTreeCodes);
				this.bi_flush();
			}
			this.last_eob_len = 7;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003FA8 File Offset: 0x000021A8
		internal bool _tr_tally(int dist, int lc)
		{
			this.pending[this._distanceOffset + this.last_lit * 2] = (byte)((uint)dist >> 8);
			this.pending[this._distanceOffset + this.last_lit * 2 + 1] = (byte)dist;
			this.pending[this._lengthOffset + this.last_lit] = (byte)lc;
			this.last_lit++;
			if (dist == 0)
			{
				short[] array = this.dyn_ltree;
				int num = lc * 2;
				array[num] += 1;
			}
			else
			{
				this.matches++;
				dist--;
				short[] array2 = this.dyn_ltree;
				int num2 = ((int)Tree.LengthCode[lc] + InternalConstants.LITERALS + 1) * 2;
				array2[num2] += 1;
				short[] array3 = this.dyn_dtree;
				int num3 = Tree.DistanceCode(dist) * 2;
				array3[num3] += 1;
			}
			if ((this.last_lit & 8191) == 0 && this.compressionLevel > CompressionLevel.Level2)
			{
				int num4 = this.last_lit << 3;
				int num5 = this.strstart - this.block_start;
				for (int i = 0; i < InternalConstants.D_CODES; i++)
				{
					num4 = (int)((long)num4 + (long)this.dyn_dtree[i * 2] * (5L + (long)Tree.ExtraDistanceBits[i]));
				}
				num4 >>= 3;
				if (this.matches < this.last_lit / 2 && num4 < num5 / 2)
				{
					return true;
				}
			}
			return this.last_lit == this.lit_bufsize - 1 || this.last_lit == this.lit_bufsize;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000410C File Offset: 0x0000230C
		internal void send_compressed_block(short[] ltree, short[] dtree)
		{
			int num = 0;
			if (this.last_lit != 0)
			{
				do
				{
					int num2 = this._distanceOffset + num * 2;
					int num3 = (((int)this.pending[num2] << 8) & 65280) | (int)(this.pending[num2 + 1] & byte.MaxValue);
					int num4 = (int)(this.pending[this._lengthOffset + num] & byte.MaxValue);
					num++;
					if (num3 == 0)
					{
						this.send_code(num4, ltree);
					}
					else
					{
						int num5 = (int)Tree.LengthCode[num4];
						this.send_code(num5 + InternalConstants.LITERALS + 1, ltree);
						int num6 = Tree.ExtraLengthBits[num5];
						if (num6 != 0)
						{
							num4 -= Tree.LengthBase[num5];
							this.send_bits(num4, num6);
						}
						num3--;
						num5 = Tree.DistanceCode(num3);
						this.send_code(num5, dtree);
						num6 = Tree.ExtraDistanceBits[num5];
						if (num6 != 0)
						{
							num3 -= Tree.DistanceBase[num5];
							this.send_bits(num3, num6);
						}
					}
				}
				while (num < this.last_lit);
			}
			this.send_code(DeflateManager.END_BLOCK, ltree);
			this.last_eob_len = (int)ltree[DeflateManager.END_BLOCK * 2 + 1];
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004214 File Offset: 0x00002414
		internal void set_data_type()
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < 7)
			{
				num2 += (int)this.dyn_ltree[i * 2];
				i++;
			}
			while (i < 128)
			{
				num += (int)this.dyn_ltree[i * 2];
				i++;
			}
			while (i < InternalConstants.LITERALS)
			{
				num2 += (int)this.dyn_ltree[i * 2];
				i++;
			}
			this.data_type = (sbyte)((num2 > num >> 2) ? DeflateManager.Z_BINARY : DeflateManager.Z_ASCII);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004290 File Offset: 0x00002490
		internal void bi_flush()
		{
			if (this.bi_valid == 16)
			{
				byte[] array = this.pending;
				int num = this.pendingCount;
				this.pendingCount = num + 1;
				array[num] = (byte)this.bi_buf;
				byte[] array2 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array2[num] = (byte)(this.bi_buf >> 8);
				this.bi_buf = 0;
				this.bi_valid = 0;
				return;
			}
			if (this.bi_valid >= 8)
			{
				byte[] array3 = this.pending;
				int num = this.pendingCount;
				this.pendingCount = num + 1;
				array3[num] = (byte)this.bi_buf;
				this.bi_buf = (short)(this.bi_buf >> 8);
				this.bi_valid -= 8;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000433C File Offset: 0x0000253C
		internal void bi_windup()
		{
			if (this.bi_valid > 8)
			{
				byte[] array = this.pending;
				int num = this.pendingCount;
				this.pendingCount = num + 1;
				array[num] = (byte)this.bi_buf;
				byte[] array2 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array2[num] = (byte)(this.bi_buf >> 8);
			}
			else if (this.bi_valid > 0)
			{
				byte[] array3 = this.pending;
				int num = this.pendingCount;
				this.pendingCount = num + 1;
				array3[num] = (byte)this.bi_buf;
			}
			this.bi_buf = 0;
			this.bi_valid = 0;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000043CC File Offset: 0x000025CC
		internal void copy_block(int buf, int len, bool header)
		{
			this.bi_windup();
			this.last_eob_len = 8;
			if (header)
			{
				byte[] array = this.pending;
				int num = this.pendingCount;
				this.pendingCount = num + 1;
				array[num] = (byte)len;
				byte[] array2 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array2[num] = (byte)(len >> 8);
				byte[] array3 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array3[num] = (byte)(~(byte)len);
				byte[] array4 = this.pending;
				num = this.pendingCount;
				this.pendingCount = num + 1;
				array4[num] = (byte)(~len >> 8);
			}
			this.put_bytes(this.window, buf, len);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004465 File Offset: 0x00002665
		internal void flush_block_only(bool eof)
		{
			this._tr_flush_block((this.block_start >= 0) ? this.block_start : (-1), this.strstart - this.block_start, eof);
			this.block_start = this.strstart;
			this._codec.flush_pending();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000044A4 File Offset: 0x000026A4
		internal BlockState DeflateNone(FlushType flush)
		{
			int num = 65535;
			if (num > this.pending.Length - 5)
			{
				num = this.pending.Length - 5;
			}
			for (;;)
			{
				if (this.lookahead <= 1)
				{
					this._fillWindow();
					if (this.lookahead == 0 && flush == FlushType.None)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_00DB;
					}
				}
				this.strstart += this.lookahead;
				this.lookahead = 0;
				int num2 = this.block_start + num;
				if (this.strstart == 0 || this.strstart >= num2)
				{
					this.lookahead = this.strstart - num2;
					this.strstart = num2;
					this.flush_block_only(false);
					if (this._codec.AvailableBytesOut == 0)
					{
						return BlockState.NeedMore;
					}
				}
				if (this.strstart - this.block_start >= this.w_size - DeflateManager.MIN_LOOKAHEAD)
				{
					this.flush_block_only(false);
					if (this._codec.AvailableBytesOut == 0)
					{
						return BlockState.NeedMore;
					}
				}
			}
			return BlockState.NeedMore;
			IL_00DB:
			this.flush_block_only(flush == FlushType.Finish);
			if (this._codec.AvailableBytesOut == 0)
			{
				if (flush != FlushType.Finish)
				{
					return BlockState.NeedMore;
				}
				return BlockState.FinishStarted;
			}
			else
			{
				if (flush != FlushType.Finish)
				{
					return BlockState.BlockDone;
				}
				return BlockState.FinishDone;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000045B2 File Offset: 0x000027B2
		internal void _tr_stored_block(int buf, int stored_len, bool eof)
		{
			this.send_bits((DeflateManager.STORED_BLOCK << 1) + (eof ? 1 : 0), 3);
			this.copy_block(buf, stored_len, true);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000045D4 File Offset: 0x000027D4
		internal void _tr_flush_block(int buf, int stored_len, bool eof)
		{
			int num = 0;
			int num2;
			int num3;
			if (this.compressionLevel > CompressionLevel.None)
			{
				if ((int)this.data_type == DeflateManager.Z_UNKNOWN)
				{
					this.set_data_type();
				}
				this.treeLiterals.build_tree(this);
				this.treeDistances.build_tree(this);
				num = this.build_bl_tree();
				num2 = this.opt_len + 3 + 7 >> 3;
				num3 = this.static_len + 3 + 7 >> 3;
				if (num3 <= num2)
				{
					num2 = num3;
				}
			}
			else
			{
				num3 = (num2 = stored_len + 5);
			}
			if (stored_len + 4 <= num2 && buf != -1)
			{
				this._tr_stored_block(buf, stored_len, eof);
			}
			else if (num3 == num2)
			{
				this.send_bits((DeflateManager.STATIC_TREES << 1) + (eof ? 1 : 0), 3);
				this.send_compressed_block(StaticTree.lengthAndLiteralsTreeCodes, StaticTree.distTreeCodes);
			}
			else
			{
				this.send_bits((DeflateManager.DYN_TREES << 1) + (eof ? 1 : 0), 3);
				this.send_all_trees(this.treeLiterals.max_code + 1, this.treeDistances.max_code + 1, num + 1);
				this.send_compressed_block(this.dyn_ltree, this.dyn_dtree);
			}
			this._InitializeBlocks();
			if (eof)
			{
				this.bi_windup();
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000046E4 File Offset: 0x000028E4
		private void _fillWindow()
		{
			for (;;)
			{
				int num = this.window_size - this.lookahead - this.strstart;
				int num2;
				if (num == 0 && this.strstart == 0 && this.lookahead == 0)
				{
					num = this.w_size;
				}
				else if (num == -1)
				{
					num--;
				}
				else if (this.strstart >= this.w_size + this.w_size - DeflateManager.MIN_LOOKAHEAD)
				{
					Array.Copy(this.window, this.w_size, this.window, 0, this.w_size);
					this.match_start -= this.w_size;
					this.strstart -= this.w_size;
					this.block_start -= this.w_size;
					num2 = this.hash_size;
					int num3 = num2;
					do
					{
						int num4 = (int)this.head[--num3] & 65535;
						this.head[num3] = (short)((num4 >= this.w_size) ? (num4 - this.w_size) : 0);
					}
					while (--num2 != 0);
					num2 = this.w_size;
					num3 = num2;
					do
					{
						int num4 = (int)this.prev[--num3] & 65535;
						this.prev[num3] = (short)((num4 >= this.w_size) ? (num4 - this.w_size) : 0);
					}
					while (--num2 != 0);
					num += this.w_size;
				}
				if (this._codec.AvailableBytesIn == 0)
				{
					break;
				}
				num2 = this._codec.read_buf(this.window, this.strstart + this.lookahead, num);
				this.lookahead += num2;
				if (this.lookahead >= DeflateManager.MIN_MATCH)
				{
					this.ins_h = (int)(this.window[this.strstart] & byte.MaxValue);
					this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + 1] & byte.MaxValue)) & this.hash_mask;
				}
				if (this.lookahead >= DeflateManager.MIN_LOOKAHEAD || this._codec.AvailableBytesIn == 0)
				{
					return;
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000048E4 File Offset: 0x00002AE4
		internal BlockState DeflateFast(FlushType flush)
		{
			int num = 0;
			for (;;)
			{
				if (this.lookahead < DeflateManager.MIN_LOOKAHEAD)
				{
					this._fillWindow();
					if (this.lookahead < DeflateManager.MIN_LOOKAHEAD && flush == FlushType.None)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_02E8;
					}
				}
				if (this.lookahead >= DeflateManager.MIN_MATCH)
				{
					this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + (DeflateManager.MIN_MATCH - 1)] & byte.MaxValue)) & this.hash_mask;
					num = (int)this.head[this.ins_h] & 65535;
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				if ((long)num != 0L && ((this.strstart - num) & 65535) <= this.w_size - DeflateManager.MIN_LOOKAHEAD && this.compressionStrategy != CompressionStrategy.HuffmanOnly)
				{
					this.match_length = this.longest_match(num);
				}
				bool flag;
				if (this.match_length >= DeflateManager.MIN_MATCH)
				{
					flag = this._tr_tally(this.strstart - this.match_start, this.match_length - DeflateManager.MIN_MATCH);
					this.lookahead -= this.match_length;
					if (this.match_length <= this.config.MaxLazy && this.lookahead >= DeflateManager.MIN_MATCH)
					{
						this.match_length--;
						int num2;
						do
						{
							this.strstart++;
							this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + (DeflateManager.MIN_MATCH - 1)] & byte.MaxValue)) & this.hash_mask;
							num = (int)this.head[this.ins_h] & 65535;
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
							num2 = this.match_length - 1;
							this.match_length = num2;
						}
						while (num2 != 0);
						this.strstart++;
					}
					else
					{
						this.strstart += this.match_length;
						this.match_length = 0;
						this.ins_h = (int)(this.window[this.strstart] & byte.MaxValue);
						this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + 1] & byte.MaxValue)) & this.hash_mask;
					}
				}
				else
				{
					flag = this._tr_tally(0, (int)(this.window[this.strstart] & byte.MaxValue));
					this.lookahead--;
					this.strstart++;
				}
				if (flag)
				{
					this.flush_block_only(false);
					if (this._codec.AvailableBytesOut == 0)
					{
						return BlockState.NeedMore;
					}
				}
			}
			return BlockState.NeedMore;
			IL_02E8:
			this.flush_block_only(flush == FlushType.Finish);
			if (this._codec.AvailableBytesOut == 0)
			{
				if (flush == FlushType.Finish)
				{
					return BlockState.FinishStarted;
				}
				return BlockState.NeedMore;
			}
			else
			{
				if (flush != FlushType.Finish)
				{
					return BlockState.BlockDone;
				}
				return BlockState.FinishDone;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004C00 File Offset: 0x00002E00
		internal BlockState DeflateSlow(FlushType flush)
		{
			int num = 0;
			for (;;)
			{
				if (this.lookahead < DeflateManager.MIN_LOOKAHEAD)
				{
					this._fillWindow();
					if (this.lookahead < DeflateManager.MIN_LOOKAHEAD && flush == FlushType.None)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_0363;
					}
				}
				if (this.lookahead >= DeflateManager.MIN_MATCH)
				{
					this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + (DeflateManager.MIN_MATCH - 1)] & byte.MaxValue)) & this.hash_mask;
					num = (int)this.head[this.ins_h] & 65535;
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				this.prev_length = this.match_length;
				this.prev_match = this.match_start;
				this.match_length = DeflateManager.MIN_MATCH - 1;
				if (num != 0 && this.prev_length < this.config.MaxLazy && ((this.strstart - num) & 65535) <= this.w_size - DeflateManager.MIN_LOOKAHEAD)
				{
					if (this.compressionStrategy != CompressionStrategy.HuffmanOnly)
					{
						this.match_length = this.longest_match(num);
					}
					if (this.match_length <= 5 && (this.compressionStrategy == CompressionStrategy.Filtered || (this.match_length == DeflateManager.MIN_MATCH && this.strstart - this.match_start > 4096)))
					{
						this.match_length = DeflateManager.MIN_MATCH - 1;
					}
				}
				if (this.prev_length >= DeflateManager.MIN_MATCH && this.match_length <= this.prev_length)
				{
					int num2 = this.strstart + this.lookahead - DeflateManager.MIN_MATCH;
					bool flag = this._tr_tally(this.strstart - 1 - this.prev_match, this.prev_length - DeflateManager.MIN_MATCH);
					this.lookahead -= this.prev_length - 1;
					this.prev_length -= 2;
					int num3;
					do
					{
						num3 = this.strstart + 1;
						this.strstart = num3;
						if (num3 <= num2)
						{
							this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[this.strstart + (DeflateManager.MIN_MATCH - 1)] & byte.MaxValue)) & this.hash_mask;
							num = (int)this.head[this.ins_h] & 65535;
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
						}
						num3 = this.prev_length - 1;
						this.prev_length = num3;
					}
					while (num3 != 0);
					this.match_available = 0;
					this.match_length = DeflateManager.MIN_MATCH - 1;
					this.strstart++;
					if (flag)
					{
						this.flush_block_only(false);
						if (this._codec.AvailableBytesOut == 0)
						{
							return BlockState.NeedMore;
						}
					}
				}
				else if (this.match_available != 0)
				{
					bool flag = this._tr_tally(0, (int)(this.window[this.strstart - 1] & byte.MaxValue));
					if (flag)
					{
						this.flush_block_only(false);
					}
					this.strstart++;
					this.lookahead--;
					if (this._codec.AvailableBytesOut == 0)
					{
						return BlockState.NeedMore;
					}
				}
				else
				{
					this.match_available = 1;
					this.strstart++;
					this.lookahead--;
				}
			}
			return BlockState.NeedMore;
			IL_0363:
			if (this.match_available != 0)
			{
				bool flag = this._tr_tally(0, (int)(this.window[this.strstart - 1] & byte.MaxValue));
				this.match_available = 0;
			}
			this.flush_block_only(flush == FlushType.Finish);
			if (this._codec.AvailableBytesOut == 0)
			{
				if (flush == FlushType.Finish)
				{
					return BlockState.FinishStarted;
				}
				return BlockState.NeedMore;
			}
			else
			{
				if (flush != FlushType.Finish)
				{
					return BlockState.BlockDone;
				}
				return BlockState.FinishDone;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004FC4 File Offset: 0x000031C4
		internal int longest_match(int cur_match)
		{
			int num = this.config.MaxChainLength;
			int num2 = this.strstart;
			int num3 = this.prev_length;
			int num4 = ((this.strstart > this.w_size - DeflateManager.MIN_LOOKAHEAD) ? (this.strstart - (this.w_size - DeflateManager.MIN_LOOKAHEAD)) : 0);
			int niceLength = this.config.NiceLength;
			int num5 = this.w_mask;
			int num6 = this.strstart + DeflateManager.MAX_MATCH;
			byte b = this.window[num2 + num3 - 1];
			byte b2 = this.window[num2 + num3];
			if (this.prev_length >= this.config.GoodLength)
			{
				num >>= 2;
			}
			if (niceLength > this.lookahead)
			{
				niceLength = this.lookahead;
			}
			do
			{
				int num7 = cur_match;
				if (this.window[num7 + num3] == b2 && this.window[num7 + num3 - 1] == b && this.window[num7] == this.window[num2] && this.window[++num7] == this.window[num2 + 1])
				{
					num2 += 2;
					num7++;
					while (this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && this.window[++num2] == this.window[++num7] && num2 < num6)
					{
					}
					int num8 = DeflateManager.MAX_MATCH - (num6 - num2);
					num2 = num6 - DeflateManager.MAX_MATCH;
					if (num8 > num3)
					{
						this.match_start = cur_match;
						num3 = num8;
						if (num8 >= niceLength)
						{
							break;
						}
						b = this.window[num2 + num3 - 1];
						b2 = this.window[num2 + num3];
					}
				}
			}
			while ((cur_match = (int)this.prev[cur_match & num5] & 65535) > num4 && --num != 0);
			if (num3 <= this.lookahead)
			{
				return num3;
			}
			return this.lookahead;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000523A File Offset: 0x0000343A
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005242 File Offset: 0x00003442
		internal bool WantRfc1950HeaderBytes { get; set; } = true;

		// Token: 0x060000BC RID: 188 RVA: 0x0000524B File Offset: 0x0000344B
		internal int Initialize(ZlibCodec codec, CompressionLevel level)
		{
			return this.Initialize(codec, level, 15);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005257 File Offset: 0x00003457
		internal int Initialize(ZlibCodec codec, CompressionLevel level, int bits)
		{
			return this.Initialize(codec, level, bits, DeflateManager.MEM_LEVEL_DEFAULT, CompressionStrategy.Default);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005268 File Offset: 0x00003468
		internal int Initialize(ZlibCodec codec, CompressionLevel level, int bits, CompressionStrategy compressionStrategy)
		{
			return this.Initialize(codec, level, bits, DeflateManager.MEM_LEVEL_DEFAULT, compressionStrategy);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000527C File Offset: 0x0000347C
		internal int Initialize(ZlibCodec codec, CompressionLevel level, int windowBits, int memLevel, CompressionStrategy strategy)
		{
			this._codec = codec;
			this._codec.Message = null;
			if (windowBits < 9 || windowBits > 15)
			{
				throw new ZlibException("windowBits must be in the range 9..15.");
			}
			if (memLevel < 1 || memLevel > DeflateManager.MEM_LEVEL_MAX)
			{
				throw new ZlibException(string.Format("memLevel must be in the range 1.. {0}", DeflateManager.MEM_LEVEL_MAX));
			}
			this._codec.dstate = this;
			this.w_bits = windowBits;
			this.w_size = 1 << this.w_bits;
			this.w_mask = this.w_size - 1;
			this.hash_bits = memLevel + 7;
			this.hash_size = 1 << this.hash_bits;
			this.hash_mask = this.hash_size - 1;
			this.hash_shift = (this.hash_bits + DeflateManager.MIN_MATCH - 1) / DeflateManager.MIN_MATCH;
			this.window = new byte[this.w_size * 2];
			this.prev = new short[this.w_size];
			this.head = new short[this.hash_size];
			this.lit_bufsize = 1 << memLevel + 6;
			this.pending = new byte[this.lit_bufsize * 4];
			this._distanceOffset = this.lit_bufsize;
			this._lengthOffset = 3 * this.lit_bufsize;
			this.compressionLevel = level;
			this.compressionStrategy = strategy;
			this.Reset();
			return 0;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000053D8 File Offset: 0x000035D8
		internal void Reset()
		{
			this._codec.TotalBytesIn = (this._codec.TotalBytesOut = 0L);
			this._codec.Message = null;
			this.pendingCount = 0;
			this.nextPending = 0;
			this.Rfc1950BytesEmitted = false;
			this.status = (this.WantRfc1950HeaderBytes ? DeflateManager.INIT_STATE : DeflateManager.BUSY_STATE);
			this._codec._Adler32 = Adler.Adler32(0U, null, 0, 0);
			this.last_flush = 0;
			this._InitializeTreeData();
			this._InitializeLazyMatch();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005464 File Offset: 0x00003664
		internal int End()
		{
			if (this.status != DeflateManager.INIT_STATE && this.status != DeflateManager.BUSY_STATE && this.status != DeflateManager.FINISH_STATE)
			{
				return -2;
			}
			this.pending = null;
			this.head = null;
			this.prev = null;
			this.window = null;
			if (this.status != DeflateManager.BUSY_STATE)
			{
				return 0;
			}
			return -3;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000054C8 File Offset: 0x000036C8
		private void SetDeflater()
		{
			switch (this.config.Flavor)
			{
			case DeflateFlavor.Store:
				this.DeflateFunction = new DeflateManager.CompressFunc(this.DeflateNone);
				return;
			case DeflateFlavor.Fast:
				this.DeflateFunction = new DeflateManager.CompressFunc(this.DeflateFast);
				return;
			case DeflateFlavor.Slow:
				this.DeflateFunction = new DeflateManager.CompressFunc(this.DeflateSlow);
				return;
			default:
				return;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000552C File Offset: 0x0000372C
		internal int SetParams(CompressionLevel level, CompressionStrategy strategy)
		{
			int num = 0;
			if (this.compressionLevel != level)
			{
				DeflateManager.Config config = DeflateManager.Config.Lookup(level);
				if (config.Flavor != this.config.Flavor && this._codec.TotalBytesIn != 0L)
				{
					num = this._codec.Deflate(FlushType.Partial);
				}
				this.compressionLevel = level;
				this.config = config;
				this.SetDeflater();
			}
			this.compressionStrategy = strategy;
			return num;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005594 File Offset: 0x00003794
		internal int SetDictionary(byte[] dictionary)
		{
			int num = dictionary.Length;
			int num2 = 0;
			if (dictionary == null || this.status != DeflateManager.INIT_STATE)
			{
				throw new ZlibException("Stream error.");
			}
			this._codec._Adler32 = Adler.Adler32(this._codec._Adler32, dictionary, 0, dictionary.Length);
			if (num < DeflateManager.MIN_MATCH)
			{
				return 0;
			}
			if (num > this.w_size - DeflateManager.MIN_LOOKAHEAD)
			{
				num = this.w_size - DeflateManager.MIN_LOOKAHEAD;
				num2 = dictionary.Length - num;
			}
			Array.Copy(dictionary, num2, this.window, 0, num);
			this.strstart = num;
			this.block_start = num;
			this.ins_h = (int)(this.window[0] & byte.MaxValue);
			this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[1] & byte.MaxValue)) & this.hash_mask;
			for (int i = 0; i <= num - DeflateManager.MIN_MATCH; i++)
			{
				this.ins_h = ((this.ins_h << this.hash_shift) ^ (int)(this.window[i + (DeflateManager.MIN_MATCH - 1)] & byte.MaxValue)) & this.hash_mask;
				this.prev[i & this.w_mask] = this.head[this.ins_h];
				this.head[this.ins_h] = (short)i;
			}
			return 0;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000056E0 File Offset: 0x000038E0
		internal int Deflate(FlushType flush)
		{
			if (this._codec.OutputBuffer == null || (this._codec.InputBuffer == null && this._codec.AvailableBytesIn != 0) || (this.status == DeflateManager.FINISH_STATE && flush != FlushType.Finish))
			{
				this._codec.Message = DeflateManager._ErrorMessage[4];
				throw new ZlibException(string.Format("Something is fishy. [{0}]", this._codec.Message));
			}
			if (this._codec.AvailableBytesOut == 0)
			{
				this._codec.Message = DeflateManager._ErrorMessage[7];
				throw new ZlibException("OutputBuffer is full (AvailableBytesOut == 0)");
			}
			int num = this.last_flush;
			this.last_flush = (int)flush;
			int num4;
			if (this.status == DeflateManager.INIT_STATE)
			{
				int num2 = DeflateManager.Z_DEFLATED + (this.w_bits - 8 << 4) << 8;
				int num3 = ((this.compressionLevel - CompressionLevel.BestSpeed) & 255) >> 1;
				if (num3 > 3)
				{
					num3 = 3;
				}
				num2 |= num3 << 6;
				if (this.strstart != 0)
				{
					num2 |= DeflateManager.PRESET_DICT;
				}
				num2 += 31 - num2 % 31;
				this.status = DeflateManager.BUSY_STATE;
				byte[] array = this.pending;
				num4 = this.pendingCount;
				this.pendingCount = num4 + 1;
				array[num4] = (byte)(num2 >> 8);
				byte[] array2 = this.pending;
				num4 = this.pendingCount;
				this.pendingCount = num4 + 1;
				array2[num4] = (byte)num2;
				if (this.strstart != 0)
				{
					byte[] array3 = this.pending;
					num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array3[num4] = (byte)((this._codec._Adler32 & 4278190080U) >> 24);
					byte[] array4 = this.pending;
					num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array4[num4] = (byte)((this._codec._Adler32 & 16711680U) >> 16);
					byte[] array5 = this.pending;
					num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array5[num4] = (byte)((this._codec._Adler32 & 65280U) >> 8);
					byte[] array6 = this.pending;
					num4 = this.pendingCount;
					this.pendingCount = num4 + 1;
					array6[num4] = (byte)(this._codec._Adler32 & 255U);
				}
				this._codec._Adler32 = Adler.Adler32(0U, null, 0, 0);
			}
			if (this.pendingCount != 0)
			{
				this._codec.flush_pending();
				if (this._codec.AvailableBytesOut == 0)
				{
					this.last_flush = -1;
					return 0;
				}
			}
			else if (this._codec.AvailableBytesIn == 0 && flush <= (FlushType)num && flush != FlushType.Finish)
			{
				return 0;
			}
			if (this.status == DeflateManager.FINISH_STATE && this._codec.AvailableBytesIn != 0)
			{
				this._codec.Message = DeflateManager._ErrorMessage[7];
				throw new ZlibException("status == FINISH_STATE && _codec.AvailableBytesIn != 0");
			}
			if (this._codec.AvailableBytesIn != 0 || this.lookahead != 0 || (flush != FlushType.None && this.status != DeflateManager.FINISH_STATE))
			{
				BlockState blockState = this.DeflateFunction(flush);
				if (blockState == BlockState.FinishStarted || blockState == BlockState.FinishDone)
				{
					this.status = DeflateManager.FINISH_STATE;
				}
				if (blockState == BlockState.NeedMore || blockState == BlockState.FinishStarted)
				{
					if (this._codec.AvailableBytesOut == 0)
					{
						this.last_flush = -1;
					}
					return 0;
				}
				if (blockState == BlockState.BlockDone)
				{
					if (flush == FlushType.Partial)
					{
						this._tr_align();
					}
					else
					{
						this._tr_stored_block(0, 0, false);
						if (flush == FlushType.Full)
						{
							for (int i = 0; i < this.hash_size; i++)
							{
								this.head[i] = 0;
							}
						}
					}
					this._codec.flush_pending();
					if (this._codec.AvailableBytesOut == 0)
					{
						this.last_flush = -1;
						return 0;
					}
				}
			}
			if (flush != FlushType.Finish)
			{
				return 0;
			}
			if (!this.WantRfc1950HeaderBytes || this.Rfc1950BytesEmitted)
			{
				return 1;
			}
			byte[] array7 = this.pending;
			num4 = this.pendingCount;
			this.pendingCount = num4 + 1;
			array7[num4] = (byte)((this._codec._Adler32 & 4278190080U) >> 24);
			byte[] array8 = this.pending;
			num4 = this.pendingCount;
			this.pendingCount = num4 + 1;
			array8[num4] = (byte)((this._codec._Adler32 & 16711680U) >> 16);
			byte[] array9 = this.pending;
			num4 = this.pendingCount;
			this.pendingCount = num4 + 1;
			array9[num4] = (byte)((this._codec._Adler32 & 65280U) >> 8);
			byte[] array10 = this.pending;
			num4 = this.pendingCount;
			this.pendingCount = num4 + 1;
			array10[num4] = (byte)(this._codec._Adler32 & 255U);
			this._codec.flush_pending();
			this.Rfc1950BytesEmitted = true;
			if (this.pendingCount == 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04000039 RID: 57
		private static readonly int MEM_LEVEL_MAX = 9;

		// Token: 0x0400003A RID: 58
		private static readonly int MEM_LEVEL_DEFAULT = 8;

		// Token: 0x0400003B RID: 59
		private DeflateManager.CompressFunc DeflateFunction;

		// Token: 0x0400003C RID: 60
		private static readonly string[] _ErrorMessage = new string[] { "need dictionary", "stream end", "", "file error", "stream error", "data error", "insufficient memory", "buffer error", "incompatible version", "" };

		// Token: 0x0400003D RID: 61
		private static readonly int PRESET_DICT = 32;

		// Token: 0x0400003E RID: 62
		private static readonly int INIT_STATE = 42;

		// Token: 0x0400003F RID: 63
		private static readonly int BUSY_STATE = 113;

		// Token: 0x04000040 RID: 64
		private static readonly int FINISH_STATE = 666;

		// Token: 0x04000041 RID: 65
		private static readonly int Z_DEFLATED = 8;

		// Token: 0x04000042 RID: 66
		private static readonly int STORED_BLOCK = 0;

		// Token: 0x04000043 RID: 67
		private static readonly int STATIC_TREES = 1;

		// Token: 0x04000044 RID: 68
		private static readonly int DYN_TREES = 2;

		// Token: 0x04000045 RID: 69
		private static readonly int Z_BINARY = 0;

		// Token: 0x04000046 RID: 70
		private static readonly int Z_ASCII = 1;

		// Token: 0x04000047 RID: 71
		private static readonly int Z_UNKNOWN = 2;

		// Token: 0x04000048 RID: 72
		private static readonly int Buf_size = 16;

		// Token: 0x04000049 RID: 73
		private static readonly int MIN_MATCH = 3;

		// Token: 0x0400004A RID: 74
		private static readonly int MAX_MATCH = 258;

		// Token: 0x0400004B RID: 75
		private static readonly int MIN_LOOKAHEAD = DeflateManager.MAX_MATCH + DeflateManager.MIN_MATCH + 1;

		// Token: 0x0400004C RID: 76
		private static readonly int HEAP_SIZE = 2 * InternalConstants.L_CODES + 1;

		// Token: 0x0400004D RID: 77
		private static readonly int END_BLOCK = 256;

		// Token: 0x0400004E RID: 78
		internal ZlibCodec _codec;

		// Token: 0x0400004F RID: 79
		internal int status;

		// Token: 0x04000050 RID: 80
		internal byte[] pending;

		// Token: 0x04000051 RID: 81
		internal int nextPending;

		// Token: 0x04000052 RID: 82
		internal int pendingCount;

		// Token: 0x04000053 RID: 83
		internal sbyte data_type;

		// Token: 0x04000054 RID: 84
		internal int last_flush;

		// Token: 0x04000055 RID: 85
		internal int w_size;

		// Token: 0x04000056 RID: 86
		internal int w_bits;

		// Token: 0x04000057 RID: 87
		internal int w_mask;

		// Token: 0x04000058 RID: 88
		internal byte[] window;

		// Token: 0x04000059 RID: 89
		internal int window_size;

		// Token: 0x0400005A RID: 90
		internal short[] prev;

		// Token: 0x0400005B RID: 91
		internal short[] head;

		// Token: 0x0400005C RID: 92
		internal int ins_h;

		// Token: 0x0400005D RID: 93
		internal int hash_size;

		// Token: 0x0400005E RID: 94
		internal int hash_bits;

		// Token: 0x0400005F RID: 95
		internal int hash_mask;

		// Token: 0x04000060 RID: 96
		internal int hash_shift;

		// Token: 0x04000061 RID: 97
		internal int block_start;

		// Token: 0x04000062 RID: 98
		private DeflateManager.Config config;

		// Token: 0x04000063 RID: 99
		internal int match_length;

		// Token: 0x04000064 RID: 100
		internal int prev_match;

		// Token: 0x04000065 RID: 101
		internal int match_available;

		// Token: 0x04000066 RID: 102
		internal int strstart;

		// Token: 0x04000067 RID: 103
		internal int match_start;

		// Token: 0x04000068 RID: 104
		internal int lookahead;

		// Token: 0x04000069 RID: 105
		internal int prev_length;

		// Token: 0x0400006A RID: 106
		internal CompressionLevel compressionLevel;

		// Token: 0x0400006B RID: 107
		internal CompressionStrategy compressionStrategy;

		// Token: 0x0400006C RID: 108
		internal short[] dyn_ltree;

		// Token: 0x0400006D RID: 109
		internal short[] dyn_dtree;

		// Token: 0x0400006E RID: 110
		internal short[] bl_tree;

		// Token: 0x0400006F RID: 111
		internal Tree treeLiterals = new Tree();

		// Token: 0x04000070 RID: 112
		internal Tree treeDistances = new Tree();

		// Token: 0x04000071 RID: 113
		internal Tree treeBitLengths = new Tree();

		// Token: 0x04000072 RID: 114
		internal short[] bl_count = new short[InternalConstants.MAX_BITS + 1];

		// Token: 0x04000073 RID: 115
		internal int[] heap = new int[2 * InternalConstants.L_CODES + 1];

		// Token: 0x04000074 RID: 116
		internal int heap_len;

		// Token: 0x04000075 RID: 117
		internal int heap_max;

		// Token: 0x04000076 RID: 118
		internal sbyte[] depth = new sbyte[2 * InternalConstants.L_CODES + 1];

		// Token: 0x04000077 RID: 119
		internal int _lengthOffset;

		// Token: 0x04000078 RID: 120
		internal int lit_bufsize;

		// Token: 0x04000079 RID: 121
		internal int last_lit;

		// Token: 0x0400007A RID: 122
		internal int _distanceOffset;

		// Token: 0x0400007B RID: 123
		internal int opt_len;

		// Token: 0x0400007C RID: 124
		internal int static_len;

		// Token: 0x0400007D RID: 125
		internal int matches;

		// Token: 0x0400007E RID: 126
		internal int last_eob_len;

		// Token: 0x0400007F RID: 127
		internal short bi_buf;

		// Token: 0x04000080 RID: 128
		internal int bi_valid;

		// Token: 0x04000081 RID: 129
		private bool Rfc1950BytesEmitted;

		// Token: 0x020000EE RID: 238
		// (Invoke) Token: 0x060005E1 RID: 1505
		internal delegate BlockState CompressFunc(FlushType flush);

		// Token: 0x020000EF RID: 239
		internal class Config
		{
			// Token: 0x060005E5 RID: 1509 RVA: 0x00021014 File Offset: 0x0001F214
			private Config(int goodLength, int maxLazy, int niceLength, int maxChainLength, DeflateFlavor flavor)
			{
				this.GoodLength = goodLength;
				this.MaxLazy = maxLazy;
				this.NiceLength = niceLength;
				this.MaxChainLength = maxChainLength;
				this.Flavor = flavor;
			}

			// Token: 0x060005E6 RID: 1510 RVA: 0x00021041 File Offset: 0x0001F241
			public static DeflateManager.Config Lookup(CompressionLevel level)
			{
				return DeflateManager.Config.Table[(int)level];
			}

			// Token: 0x04000411 RID: 1041
			private static readonly DeflateManager.Config[] Table = new DeflateManager.Config[]
			{
				new DeflateManager.Config(0, 0, 0, 0, DeflateFlavor.Store),
				new DeflateManager.Config(4, 4, 8, 4, DeflateFlavor.Fast),
				new DeflateManager.Config(4, 5, 16, 8, DeflateFlavor.Fast),
				new DeflateManager.Config(4, 6, 32, 32, DeflateFlavor.Fast),
				new DeflateManager.Config(4, 4, 16, 16, DeflateFlavor.Slow),
				new DeflateManager.Config(8, 16, 32, 32, DeflateFlavor.Slow),
				new DeflateManager.Config(8, 16, 128, 128, DeflateFlavor.Slow),
				new DeflateManager.Config(8, 32, 128, 256, DeflateFlavor.Slow),
				new DeflateManager.Config(32, 128, 258, 1024, DeflateFlavor.Slow),
				new DeflateManager.Config(32, 258, 258, 4096, DeflateFlavor.Slow)
			};

			// Token: 0x04000412 RID: 1042
			internal DeflateFlavor Flavor;

			// Token: 0x04000413 RID: 1043
			internal int GoodLength;

			// Token: 0x04000414 RID: 1044
			internal int MaxChainLength;

			// Token: 0x04000415 RID: 1045
			internal int MaxLazy;

			// Token: 0x04000416 RID: 1046
			internal int NiceLength;
		}
	}
}

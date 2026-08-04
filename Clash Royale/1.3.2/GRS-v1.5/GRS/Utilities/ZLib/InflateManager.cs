using System;

namespace CRS.Utilities.ZLib
{
	// Token: 0x0200000B RID: 11
	internal sealed class InflateManager
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000073DF File Offset: 0x000055DF
		public InflateManager()
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000073E7 File Offset: 0x000055E7
		public InflateManager(bool expectRfc1950HeaderBytes)
		{
			this.HandleRfc1950HeaderBytes = expectRfc1950HeaderBytes;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000073F6 File Offset: 0x000055F6
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000073FE File Offset: 0x000055FE
		internal bool HandleRfc1950HeaderBytes { get; set; }

		// Token: 0x06000082 RID: 130 RVA: 0x00007408 File Offset: 0x00005608
		internal int Reset()
		{
			this._codec.TotalBytesIn = (this._codec.TotalBytesOut = 0L);
			this._codec.Message = null;
			this.mode = (this.HandleRfc1950HeaderBytes ? InflateManager.InflateManagerMode.METHOD : InflateManager.InflateManagerMode.BLOCKS);
			this.blocks.Reset();
			return 0;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000745B File Offset: 0x0000565B
		internal int End()
		{
			if (this.blocks != null)
			{
				this.blocks.Free();
			}
			this.blocks = null;
			return 0;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00007478 File Offset: 0x00005678
		internal int Initialize(ZlibCodec codec, int w)
		{
			this._codec = codec;
			this._codec.Message = null;
			this.blocks = null;
			if (w < 8 || w > 15)
			{
				this.End();
				throw new ZlibException("Bad window size.");
			}
			this.wbits = w;
			this.blocks = new InflateBlocks(codec, this.HandleRfc1950HeaderBytes ? this : null, 1 << w);
			this.Reset();
			return 0;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000074E8 File Offset: 0x000056E8
		internal int Inflate(FlushType flush)
		{
			if (this._codec.InputBuffer == null)
			{
				throw new ZlibException("InputBuffer is null. ");
			}
			int num = 0;
			int num2 = -5;
			int num3;
			for (; ; )
			{
				switch (this.mode)
				{
					case InflateManager.InflateManagerMode.METHOD:
						{
							if (this._codec.AvailableBytesIn == 0)
							{
								return num2;
							}
							num2 = num;
							this._codec.AvailableBytesIn--;
							this._codec.TotalBytesIn += 1L;
							byte[] inputBuffer = this._codec.InputBuffer;
							ZlibCodec codec = this._codec;
							num3 = codec.NextIn;
							codec.NextIn = num3 + 1;
							if (((this.method = inputBuffer[num3]) & 15) != 8)
							{
								this.mode = InflateManager.InflateManagerMode.BAD;
								this._codec.Message = string.Format("unknown compression method (0x{0:X2})", this.method);
								this.marker = 5;
								continue;
							}
							if ((this.method >> 4) + 8 > this.wbits)
							{
								this.mode = InflateManager.InflateManagerMode.BAD;
								this._codec.Message = string.Format("invalid window size ({0})", (this.method >> 4) + 8);
								this.marker = 5;
								continue;
							}
							this.mode = InflateManager.InflateManagerMode.FLAG;
							continue;
						}
					case InflateManager.InflateManagerMode.FLAG:
						{
							if (this._codec.AvailableBytesIn == 0)
							{
								return num2;
							}
							num2 = num;
							this._codec.AvailableBytesIn--;
							this._codec.TotalBytesIn += 1L;
							byte[] inputBuffer2 = this._codec.InputBuffer;
							ZlibCodec codec2 = this._codec;
							num3 = codec2.NextIn;
							codec2.NextIn = num3 + 1;
							int num4 = inputBuffer2[num3] & 255;
							if (((this.method << 8) + num4) % 31 != 0)
							{
								this.mode = InflateManager.InflateManagerMode.BAD;
								this._codec.Message = "incorrect header check";
								this.marker = 5;
								continue;
							}
							this.mode = (((num4 & 32) == 0) ? InflateManager.InflateManagerMode.BLOCKS : InflateManager.InflateManagerMode.DICT4);
							continue;
						}
					case InflateManager.InflateManagerMode.DICT4:
						{
							if (this._codec.AvailableBytesIn == 0)
							{
								return num2;
							}
							num2 = num;
							this._codec.AvailableBytesIn--;
							this._codec.TotalBytesIn += 1L;
							byte[] inputBuffer3 = this._codec.InputBuffer;
							ZlibCodec codec3 = this._codec;
							num3 = codec3.NextIn;
							codec3.NextIn = num3 + 1;
							this.expectedCheck = unchecked((uint)((inputBuffer3[num3] << 24) & (long)(ulong)(-16777216)));
							this.mode = InflateManager.InflateManagerMode.DICT3;
							continue;
						}
					case InflateManager.InflateManagerMode.DICT3:
						{
							if (this._codec.AvailableBytesIn == 0)
							{
								return num2;
							}
							num2 = num;
							this._codec.AvailableBytesIn--;
							this._codec.TotalBytesIn += 1L;
							uint num5 = this.expectedCheck;
							byte[] inputBuffer4 = this._codec.InputBuffer;
							ZlibCodec codec4 = this._codec;
							num3 = codec4.NextIn;
							codec4.NextIn = num3 + 1;
							this.expectedCheck = (uint)(num5 + ((inputBuffer4[num3] << 16) & 16711680U));
							this.mode = InflateManager.InflateManagerMode.DICT2;
							continue;
						}
					case InflateManager.InflateManagerMode.DICT2:
						{
							if (this._codec.AvailableBytesIn == 0)
							{
								return num2;
							}
							num2 = num;
							this._codec.AvailableBytesIn--;
							this._codec.TotalBytesIn += 1L;
							uint num6 = this.expectedCheck;
							byte[] inputBuffer5 = this._codec.InputBuffer;
							ZlibCodec codec5 = this._codec;
							num3 = codec5.NextIn;
							codec5.NextIn = num3 + 1;
							this.expectedCheck = (uint)(num6 + ((inputBuffer5[num3] << 8) & 65280U));
							this.mode = InflateManager.InflateManagerMode.DICT1;
							continue;
						}
					case InflateManager.InflateManagerMode.DICT1:
						goto IL_0383;
					case InflateManager.InflateManagerMode.DICT0:
						goto IL_040D;
					case InflateManager.InflateManagerMode.BLOCKS:
						num2 = this.blocks.Process(num2);
						if (num2 == -3)
						{
							this.mode = InflateManager.InflateManagerMode.BAD;
							this.marker = 0;
							continue;
						}
						if (num2 == 0)
						{
							num2 = num;
						}
						if (num2 != 1)
						{
							return num2;
						}
						num2 = num;
						this.computedCheck = this.blocks.Reset();
						if (!this.HandleRfc1950HeaderBytes)
						{
							goto Block_16;
						}
						this.mode = InflateManager.InflateManagerMode.CHECK4;
						continue;
					case InflateManager.InflateManagerMode.CHECK4:
						{
							if (this._codec.AvailableBytesIn == 0)
							{
								return num2;
							}
							num2 = num;
							this._codec.AvailableBytesIn--;
							this._codec.TotalBytesIn += 1L;
							byte[] inputBuffer6 = this._codec.InputBuffer;
							ZlibCodec codec6 = this._codec;
							num3 = codec6.NextIn;
							codec6.NextIn = num3 + 1;
							this.expectedCheck = unchecked((uint)((inputBuffer6[num3] << 24) & (long)(ulong)(-16777216)));
							this.mode = InflateManager.InflateManagerMode.CHECK3;
							continue;
						}
					case InflateManager.InflateManagerMode.CHECK3:
						{
							if (this._codec.AvailableBytesIn == 0)
							{
								return num2;
							}
							num2 = num;
							this._codec.AvailableBytesIn--;
							this._codec.TotalBytesIn += 1L;
							uint num7 = this.expectedCheck;
							byte[] inputBuffer7 = this._codec.InputBuffer;
							ZlibCodec codec7 = this._codec;
							num3 = codec7.NextIn;
							codec7.NextIn = num3 + 1;
							this.expectedCheck = (uint)(num7 + ((inputBuffer7[num3] << 16) & 16711680U));
							this.mode = InflateManager.InflateManagerMode.CHECK2;
							continue;
						}
					case InflateManager.InflateManagerMode.CHECK2:
						{
							if (this._codec.AvailableBytesIn == 0)
							{
								return num2;
							}
							num2 = num;
							this._codec.AvailableBytesIn--;
							this._codec.TotalBytesIn += 1L;
							uint num8 = this.expectedCheck;
							byte[] inputBuffer8 = this._codec.InputBuffer;
							ZlibCodec codec8 = this._codec;
							num3 = codec8.NextIn;
							codec8.NextIn = num3 + 1;
							this.expectedCheck = (uint)(num8 + ((inputBuffer8[num3] << 8) & 65280U));
							this.mode = InflateManager.InflateManagerMode.CHECK1;
							continue;
						}
					case InflateManager.InflateManagerMode.CHECK1:
						{
							if (this._codec.AvailableBytesIn == 0)
							{
								return num2;
							}
							num2 = num;
							this._codec.AvailableBytesIn--;
							this._codec.TotalBytesIn += 1L;
							uint num9 = this.expectedCheck;
							byte[] inputBuffer9 = this._codec.InputBuffer;
							ZlibCodec codec9 = this._codec;
							num3 = codec9.NextIn;
							codec9.NextIn = num3 + 1;
							this.expectedCheck = num9 + (inputBuffer9[num3] & 255U);
							if (this.computedCheck != this.expectedCheck)
							{
								this.mode = InflateManager.InflateManagerMode.BAD;
								this._codec.Message = "incorrect data check";
								this.marker = 5;
								continue;
							}
							goto IL_06AE;
						}
					case InflateManager.InflateManagerMode.DONE:
						return 1;
					case InflateManager.InflateManagerMode.BAD:
						goto IL_06BA;
				}
				break;
			}
			throw new ZlibException("Stream error.");
		IL_0383:
			if (this._codec.AvailableBytesIn == 0)
			{
				return num2;
			}
			this._codec.AvailableBytesIn--;
			this._codec.TotalBytesIn += 1L;
			uint num10 = this.expectedCheck;
			byte[] inputBuffer10 = this._codec.InputBuffer;
			ZlibCodec codec10 = this._codec;
			num3 = codec10.NextIn;
			codec10.NextIn = num3 + 1;
			this.expectedCheck = num10 + (inputBuffer10[num3] & 255U);
			this._codec._Adler32 = this.expectedCheck;
			this.mode = InflateManager.InflateManagerMode.DICT0;
			return 2;
		IL_040D:
			this.mode = InflateManager.InflateManagerMode.BAD;
			this._codec.Message = "need dictionary";
			this.marker = 0;
			return -2;
		Block_16:
			this.mode = InflateManager.InflateManagerMode.DONE;
			return 1;
		IL_06AE:
			this.mode = InflateManager.InflateManagerMode.DONE;
			return 1;
		IL_06BA:
			throw new ZlibException(string.Format("Bad state ({0})", this._codec.Message));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00007BD4 File Offset: 0x00005DD4
		internal int SetDictionary(byte[] dictionary)
		{
			int num = 0;
			int num2 = dictionary.Length;
			if (this.mode != InflateManager.InflateManagerMode.DICT0)
			{
				throw new ZlibException("Stream error.");
			}
			if (Adler.Adler32(1U, dictionary, 0, dictionary.Length) != this._codec._Adler32)
			{
				return -3;
			}
			this._codec._Adler32 = Adler.Adler32(0U, null, 0, 0);
			if (num2 >= 1 << this.wbits)
			{
				num2 = (1 << this.wbits) - 1;
				num = dictionary.Length - num2;
			}
			this.blocks.SetDictionary(dictionary, num, num2);
			this.mode = InflateManager.InflateManagerMode.BLOCKS;
			return 0;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00007C64 File Offset: 0x00005E64
		internal int Sync()
		{
			if (this.mode != InflateManager.InflateManagerMode.BAD)
			{
				this.mode = InflateManager.InflateManagerMode.BAD;
				this.marker = 0;
			}
			int num;
			if ((num = this._codec.AvailableBytesIn) == 0)
			{
				return -5;
			}
			int num2 = this._codec.NextIn;
			int num3 = this.marker;
			while (num != 0 && num3 < 4)
			{
				if (this._codec.InputBuffer[num2] == InflateManager.mark[num3])
				{
					num3++;
				}
				else if (this._codec.InputBuffer[num2] != 0)
				{
					num3 = 0;
				}
				else
				{
					num3 = 4 - num3;
				}
				num2++;
				num--;
			}
			this._codec.TotalBytesIn += (long)(num2 - this._codec.NextIn);
			this._codec.NextIn = num2;
			this._codec.AvailableBytesIn = num;
			this.marker = num3;
			if (num3 != 4)
			{
				return -3;
			}
			long totalBytesIn = this._codec.TotalBytesIn;
			long totalBytesOut = this._codec.TotalBytesOut;
			this.Reset();
			this._codec.TotalBytesIn = totalBytesIn;
			this._codec.TotalBytesOut = totalBytesOut;
			this.mode = InflateManager.InflateManagerMode.BLOCKS;
			return 0;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00007D7A File Offset: 0x00005F7A
		internal int SyncPoint(ZlibCodec z)
		{
			return this.blocks.SyncPoint();
		}

		// Token: 0x04000090 RID: 144
		private const int PRESET_DICT = 32;

		// Token: 0x04000091 RID: 145
		private const int Z_DEFLATED = 8;

		// Token: 0x04000092 RID: 146
		private static readonly byte[] mark = new byte[] { 0, 0, byte.MaxValue, byte.MaxValue };

		// Token: 0x04000093 RID: 147
		internal ZlibCodec _codec;

		// Token: 0x04000094 RID: 148
		internal InflateBlocks blocks;

		// Token: 0x04000095 RID: 149
		internal uint computedCheck;

		// Token: 0x04000096 RID: 150
		internal uint expectedCheck;

		// Token: 0x04000097 RID: 151
		internal int marker;

		// Token: 0x04000098 RID: 152
		internal int method;

		// Token: 0x04000099 RID: 153
		private InflateManager.InflateManagerMode mode;

		// Token: 0x0400009A RID: 154
		internal int wbits;

		// Token: 0x020000C9 RID: 201
		private enum InflateManagerMode
		{
			// Token: 0x04000373 RID: 883
			BAD = 13,
			// Token: 0x04000374 RID: 884
			BLOCKS = 7,
			// Token: 0x04000375 RID: 885
			CHECK1 = 11,
			// Token: 0x04000376 RID: 886
			CHECK2 = 10,
			// Token: 0x04000377 RID: 887
			CHECK3 = 9,
			// Token: 0x04000378 RID: 888
			CHECK4 = 8,
			// Token: 0x04000379 RID: 889
			DICT0 = 6,
			// Token: 0x0400037A RID: 890
			DICT1 = 5,
			// Token: 0x0400037B RID: 891
			DICT2 = 4,
			// Token: 0x0400037C RID: 892
			DICT3 = 3,
			// Token: 0x0400037D RID: 893
			DICT4 = 2,
			// Token: 0x0400037E RID: 894
			DONE = 12,
			// Token: 0x0400037F RID: 895
			FLAG = 1,
			// Token: 0x04000380 RID: 896
			METHOD = 0
		}
	}
}
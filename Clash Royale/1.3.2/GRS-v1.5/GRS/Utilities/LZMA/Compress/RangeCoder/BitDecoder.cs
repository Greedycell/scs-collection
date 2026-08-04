using System;

namespace CRS.Utilities.LZMA.Compress.RangeCoder
{
	// Token: 0x02000044 RID: 68
	internal struct BitDecoder
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000E573 File Offset: 0x0000C773
		public void UpdateModel(int numMoveBits, uint symbol)
		{
			if (symbol == 0U)
			{
				this.Prob += 2048U - this.Prob >> numMoveBits;
				return;
			}
			this.Prob -= this.Prob >> numMoveBits;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000E5AF File Offset: 0x0000C7AF
		public void Init()
		{
			this.Prob = 1024U;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		public uint Decode(Decoder rangeDecoder)
		{
			uint num = (rangeDecoder.Range >> 11) * this.Prob;
			if (rangeDecoder.Code < num)
			{
				rangeDecoder.Range = num;
				this.Prob += 2048U - this.Prob >> 5;
				if (rangeDecoder.Range < 16777216U)
				{
					rangeDecoder.Code = (rangeDecoder.Code << 8) | (uint)((byte)rangeDecoder.Stream.ReadByte());
					rangeDecoder.Range <<= 8;
				}
				return 0U;
			}
			rangeDecoder.Range -= num;
			rangeDecoder.Code -= num;
			this.Prob -= this.Prob >> 5;
			if (rangeDecoder.Range < 16777216U)
			{
				rangeDecoder.Code = (rangeDecoder.Code << 8) | (uint)((byte)rangeDecoder.Stream.ReadByte());
				rangeDecoder.Range <<= 8;
			}
			return 1U;
		}

		// Token: 0x040001B7 RID: 439
		public const int kNumBitModelTotalBits = 11;

		// Token: 0x040001B8 RID: 440
		public const uint kBitModelTotal = 2048U;

		// Token: 0x040001B9 RID: 441
		private const int kNumMoveBits = 5;

		// Token: 0x040001BA RID: 442
		private uint Prob;
	}
}

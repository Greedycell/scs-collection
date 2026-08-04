using System;

namespace CRS.Utilities.LZMA.Compress.RangeCoder
{
	// Token: 0x02000043 RID: 67
	internal struct BitEncoder
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		public void Init()
		{
			this.Prob = 1024U;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000E3FD File Offset: 0x0000C5FD
		public void UpdateModel(uint symbol)
		{
			if (symbol == 0U)
			{
				this.Prob += 2048U - this.Prob >> 5;
				return;
			}
			this.Prob -= this.Prob >> 5;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000E434 File Offset: 0x0000C634
		public void Encode(Encoder encoder, uint symbol)
		{
			uint num = (encoder.Range >> 11) * this.Prob;
			if (symbol == 0U)
			{
				encoder.Range = num;
				this.Prob += 2048U - this.Prob >> 5;
			}
			else
			{
				encoder.Low += (ulong)num;
				encoder.Range -= num;
				this.Prob -= this.Prob >> 5;
			}
			if (encoder.Range < 16777216U)
			{
				encoder.Range <<= 8;
				encoder.ShiftLow();
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		static BitEncoder()
		{
			for (int i = 8; i >= 0; i--)
			{
				uint num = 1U << 9 - i - 1;
				uint num2 = 1U << 9 - i;
				for (uint num3 = num; num3 < num2; num3 += 1U)
				{
					BitEncoder.ProbPrices[(int)num3] = (uint)((i << 6) + (int)(num2 - num3 << 6 >> 9 - i - 1));
				}
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000E52E File Offset: 0x0000C72E
		public uint GetPrice(uint symbol)
		{
			checked
			{
				return BitEncoder.ProbPrices[(int)((IntPtr)((unchecked((ulong)(this.Prob - symbol) ^ (ulong)((long)(-(long)symbol))) & 2047UL) >> 2))];
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000E54D File Offset: 0x0000C74D
		public uint GetPrice0()
		{
			return BitEncoder.ProbPrices[(int)(this.Prob >> 2)];
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000E55D File Offset: 0x0000C75D
		public uint GetPrice1()
		{
			return BitEncoder.ProbPrices[(int)(2048U - this.Prob >> 2)];
		}

		// Token: 0x040001B0 RID: 432
		public const int kNumBitModelTotalBits = 11;

		// Token: 0x040001B1 RID: 433
		public const uint kBitModelTotal = 2048U;

		// Token: 0x040001B2 RID: 434
		private const int kNumMoveBits = 5;

		// Token: 0x040001B3 RID: 435
		private const int kNumMoveReducingBits = 2;

		// Token: 0x040001B4 RID: 436
		public const int kNumBitPriceShiftBits = 6;

		// Token: 0x040001B5 RID: 437
		private uint Prob;

		// Token: 0x040001B6 RID: 438
		private static uint[] ProbPrices = new uint[512];
	}
}

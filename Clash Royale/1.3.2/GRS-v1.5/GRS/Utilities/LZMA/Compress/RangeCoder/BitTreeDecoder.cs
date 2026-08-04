using System;

namespace CRS.Utilities.LZMA.Compress.RangeCoder
{
	// Token: 0x02000046 RID: 70
	internal struct BitTreeDecoder
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000E88C File Offset: 0x0000CA8C
		public BitTreeDecoder(int numBitLevels)
		{
			this.NumBitLevels = numBitLevels;
			this.Models = new BitDecoder[1 << numBitLevels];
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		public void Init()
		{
			uint num = 1U;
			while ((ulong)num < (ulong)(1L << (this.NumBitLevels & 31)))
			{
				this.Models[(int)num].Init();
				num += 1U;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
		public uint Decode(Decoder rangeDecoder)
		{
			uint num = 1U;
			for (int i = this.NumBitLevels; i > 0; i--)
			{
				num = (num << 1) + this.Models[(int)num].Decode(rangeDecoder);
			}
			return num - (1U << this.NumBitLevels);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000E924 File Offset: 0x0000CB24
		public uint ReverseDecode(Decoder rangeDecoder)
		{
			uint num = 1U;
			uint num2 = 0U;
			for (int i = 0; i < this.NumBitLevels; i++)
			{
				uint num3 = this.Models[(int)num].Decode(rangeDecoder);
				num <<= 1;
				num += num3;
				num2 |= num3 << i;
			}
			return num2;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000E96C File Offset: 0x0000CB6C
		public static uint ReverseDecode(BitDecoder[] Models, uint startIndex, Decoder rangeDecoder, int NumBitLevels)
		{
			uint num = 1U;
			uint num2 = 0U;
			for (int i = 0; i < NumBitLevels; i++)
			{
				uint num3 = Models[(int)(startIndex + num)].Decode(rangeDecoder);
				num <<= 1;
				num += num3;
				num2 |= num3 << i;
			}
			return num2;
		}

		// Token: 0x040001BD RID: 445
		private BitDecoder[] Models;

		// Token: 0x040001BE RID: 446
		private int NumBitLevels;
	}
}

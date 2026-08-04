using System;

namespace CRS.Utilities.LZMA.Compress.RangeCoder
{
	// Token: 0x02000045 RID: 69
	internal struct BitTreeEncoder
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0000E6A5 File Offset: 0x0000C8A5
		public BitTreeEncoder(int numBitLevels)
		{
			this.NumBitLevels = numBitLevels;
			this.Models = new BitEncoder[1 << numBitLevels];
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		public void Init()
		{
			uint num = 1U;
			while ((ulong)num < (ulong)(1L << (this.NumBitLevels & 31)))
			{
				this.Models[(int)num].Init();
				num += 1U;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		public void Encode(Encoder rangeEncoder, uint symbol)
		{
			uint num = 1U;
			int i = this.NumBitLevels;
			while (i > 0)
			{
				i--;
				uint num2 = (symbol >> i) & 1U;
				this.Models[(int)num].Encode(rangeEncoder, num2);
				num = (num << 1) | num2;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000E73C File Offset: 0x0000C93C
		public void ReverseEncode(Encoder rangeEncoder, uint symbol)
		{
			uint num = 1U;
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)this.NumBitLevels))
			{
				uint num3 = symbol & 1U;
				this.Models[(int)num].Encode(rangeEncoder, num3);
				num = (num << 1) | num3;
				symbol >>= 1;
				num2 += 1U;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000E780 File Offset: 0x0000C980
		public uint GetPrice(uint symbol)
		{
			uint num = 0U;
			uint num2 = 1U;
			int i = this.NumBitLevels;
			while (i > 0)
			{
				i--;
				uint num3 = (symbol >> i) & 1U;
				num += this.Models[(int)num2].GetPrice(num3);
				num2 = (num2 << 1) + num3;
			}
			return num;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		public uint ReverseGetPrice(uint symbol)
		{
			uint num = 0U;
			uint num2 = 1U;
			for (int i = this.NumBitLevels; i > 0; i--)
			{
				uint num3 = symbol & 1U;
				symbol >>= 1;
				num += this.Models[(int)num2].GetPrice(num3);
				num2 = (num2 << 1) | num3;
			}
			return num;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000E810 File Offset: 0x0000CA10
		public static uint ReverseGetPrice(BitEncoder[] Models, uint startIndex, int NumBitLevels, uint symbol)
		{
			uint num = 0U;
			uint num2 = 1U;
			for (int i = NumBitLevels; i > 0; i--)
			{
				uint num3 = symbol & 1U;
				symbol >>= 1;
				num += Models[(int)(startIndex + num2)].GetPrice(num3);
				num2 = (num2 << 1) | num3;
			}
			return num;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000E850 File Offset: 0x0000CA50
		public static void ReverseEncode(BitEncoder[] Models, uint startIndex, Encoder rangeEncoder, int NumBitLevels, uint symbol)
		{
			uint num = 1U;
			for (int i = 0; i < NumBitLevels; i++)
			{
				uint num2 = symbol & 1U;
				Models[(int)(startIndex + num)].Encode(rangeEncoder, num2);
				num = (num << 1) | num2;
				symbol >>= 1;
			}
		}

		// Token: 0x040001BB RID: 443
		private BitEncoder[] Models;

		// Token: 0x040001BC RID: 444
		private int NumBitLevels;
	}
}

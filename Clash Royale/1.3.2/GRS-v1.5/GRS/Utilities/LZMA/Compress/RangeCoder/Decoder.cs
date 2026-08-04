using System;
using System.IO;

namespace CRS.Utilities.LZMA.Compress.RangeCoder
{
	// Token: 0x02000042 RID: 66
	internal class Decoder
	{
		// Token: 0x060001FC RID: 508 RVA: 0x0000E204 File Offset: 0x0000C404
		public void Init(Stream stream)
		{
			this.Stream = stream;
			this.Code = 0U;
			this.Range = uint.MaxValue;
			for (int i = 0; i < 5; i++)
			{
				this.Code = (this.Code << 8) | (uint)((byte)this.Stream.ReadByte());
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000E24D File Offset: 0x0000C44D
		public void ReleaseStream()
		{
			this.Stream = null;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000E256 File Offset: 0x0000C456
		public void CloseStream()
		{
			this.Stream.Close();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000E263 File Offset: 0x0000C463
		public void Normalize()
		{
			while (this.Range < 16777216U)
			{
				this.Code = (this.Code << 8) | (uint)((byte)this.Stream.ReadByte());
				this.Range <<= 8;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000E29D File Offset: 0x0000C49D
		public void Normalize2()
		{
			if (this.Range < 16777216U)
			{
				this.Code = (this.Code << 8) | (uint)((byte)this.Stream.ReadByte());
				this.Range <<= 8;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		public uint GetThreshold(uint total)
		{
			return this.Code / (this.Range /= total);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000E2FD File Offset: 0x0000C4FD
		public void Decode(uint start, uint size, uint total)
		{
			this.Code -= start * this.Range;
			this.Range *= size;
			this.Normalize();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000E328 File Offset: 0x0000C528
		public uint DecodeDirectBits(int numTotalBits)
		{
			uint num = this.Range;
			uint num2 = this.Code;
			uint num3 = 0U;
			for (int i = numTotalBits; i > 0; i--)
			{
				num >>= 1;
				uint num4 = num2 - num >> 31;
				num2 -= num & (num4 - 1U);
				num3 = (num3 << 1) | (1U - num4);
				if (num < 16777216U)
				{
					num2 = (num2 << 8) | (uint)((byte)this.Stream.ReadByte());
					num <<= 8;
				}
			}
			this.Range = num;
			this.Code = num2;
			return num3;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000E39C File Offset: 0x0000C59C
		public uint DecodeBit(uint size0, int numTotalBits)
		{
			uint num = (this.Range >> numTotalBits) * size0;
			uint num2;
			if (this.Code < num)
			{
				num2 = 0U;
				this.Range = num;
			}
			else
			{
				num2 = 1U;
				this.Code -= num;
				this.Range -= num;
			}
			this.Normalize();
			return num2;
		}

		// Token: 0x040001AC RID: 428
		public const uint kTopValue = 16777216U;

		// Token: 0x040001AD RID: 429
		public uint Range;

		// Token: 0x040001AE RID: 430
		public uint Code;

		// Token: 0x040001AF RID: 431
		public Stream Stream;
	}
}

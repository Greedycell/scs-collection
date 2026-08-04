using System;
using System.IO;

namespace CRS.Utilities.LZMA.Compress.RangeCoder
{
	// Token: 0x02000041 RID: 65
	internal class Encoder
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000DFA2 File Offset: 0x0000C1A2
		public void SetStream(Stream stream)
		{
			this.Stream = stream;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000DFAB File Offset: 0x0000C1AB
		public void ReleaseStream()
		{
			this.Stream = null;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000DFB4 File Offset: 0x0000C1B4
		public void Init()
		{
			this.StartPosition = this.Stream.Position;
			this.Low = 0UL;
			this.Range = uint.MaxValue;
			this._cacheSize = 1U;
			this._cache = 0;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		public void FlushData()
		{
			for (int i = 0; i < 5; i++)
			{
				this.ShiftLow();
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000E003 File Offset: 0x0000C203
		public void FlushStream()
		{
			this.Stream.Flush();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000E010 File Offset: 0x0000C210
		public void CloseStream()
		{
			this.Stream.Close();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000E020 File Offset: 0x0000C220
		public void Encode(uint start, uint size, uint total)
		{
			this.Low += (ulong)(start * (this.Range /= total));
			this.Range *= size;
			while (this.Range < 16777216U)
			{
				this.Range <<= 8;
				this.ShiftLow();
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000E080 File Offset: 0x0000C280
		public void ShiftLow()
		{
			if ((uint)this.Low < 4278190080U || (uint)(this.Low >> 32) == 1U)
			{
				byte b = this._cache;
				uint num;
				do
				{
					this.Stream.WriteByte((byte)((ulong)b + (this.Low >> 32)));
					b = byte.MaxValue;
					num = this._cacheSize - 1U;
					this._cacheSize = num;
				}
				while (num != 0U);
				this._cache = (byte)((uint)this.Low >> 24);
			}
			this._cacheSize += 1U;
			this.Low = (ulong)((ulong)((uint)this.Low) << 8);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000E110 File Offset: 0x0000C310
		public void EncodeDirectBits(uint v, int numTotalBits)
		{
			for (int i = numTotalBits - 1; i >= 0; i--)
			{
				this.Range >>= 1;
				if (((v >> i) & 1U) == 1U)
				{
					this.Low += (ulong)this.Range;
				}
				if (this.Range < 16777216U)
				{
					this.Range <<= 8;
					this.ShiftLow();
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000E17C File Offset: 0x0000C37C
		public void EncodeBit(uint size0, int numTotalBits, uint symbol)
		{
			uint num = (this.Range >> numTotalBits) * size0;
			if (symbol == 0U)
			{
				this.Range = num;
			}
			else
			{
				this.Low += (ulong)num;
				this.Range -= num;
			}
			while (this.Range < 16777216U)
			{
				this.Range <<= 8;
				this.ShiftLow();
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000E1E3 File Offset: 0x0000C3E3
		public long GetProcessedSizeAdd()
		{
			return (long)((ulong)this._cacheSize + (ulong)this.Stream.Position - (ulong)this.StartPosition + 4UL);
		}

		// Token: 0x040001A5 RID: 421
		public const uint kTopValue = 16777216U;

		// Token: 0x040001A6 RID: 422
		private Stream Stream;

		// Token: 0x040001A7 RID: 423
		public ulong Low;

		// Token: 0x040001A8 RID: 424
		public uint Range;

		// Token: 0x040001A9 RID: 425
		private uint _cacheSize;

		// Token: 0x040001AA RID: 426
		private byte _cache;

		// Token: 0x040001AB RID: 427
		private long StartPosition;
	}
}

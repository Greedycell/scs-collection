using System;
using System.IO;

namespace CRS.Utilities.LZMA.Compress.LZ
{
	// Token: 0x0200004B RID: 75
	public class OutWindow
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000F5D2 File Offset: 0x0000D7D2
		public void Create(uint windowSize)
		{
			if (this._windowSize != windowSize)
			{
				this._buffer = new byte[windowSize];
			}
			this._windowSize = windowSize;
			this._pos = 0U;
			this._streamPos = 0U;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000F5FE File Offset: 0x0000D7FE
		public void Init(Stream stream, bool solid)
		{
			this.ReleaseStream();
			this._stream = stream;
			if (!solid)
			{
				this._streamPos = 0U;
				this._pos = 0U;
				this.TrainSize = 0U;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000F628 File Offset: 0x0000D828
		public bool Train(Stream stream)
		{
			long length = stream.Length;
			uint num = ((length < (long)((ulong)this._windowSize)) ? ((uint)length) : this._windowSize);
			this.TrainSize = num;
			stream.Position = length - (long)((ulong)num);
			this._streamPos = (this._pos = 0U);
			while (num > 0U)
			{
				uint num2 = this._windowSize - this._pos;
				if (num < num2)
				{
					num2 = num;
				}
				int num3 = stream.Read(this._buffer, (int)this._pos, (int)num2);
				if (num3 == 0)
				{
					return false;
				}
				num -= (uint)num3;
				this._pos += (uint)num3;
				this._streamPos += (uint)num3;
				if (this._pos == this._windowSize)
				{
					this._streamPos = (this._pos = 0U);
				}
			}
			return true;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000F6E9 File Offset: 0x0000D8E9
		public void ReleaseStream()
		{
			this.Flush();
			this._stream = null;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		public void Flush()
		{
			uint num = this._pos - this._streamPos;
			if (num == 0U)
			{
				return;
			}
			this._stream.Write(this._buffer, (int)this._streamPos, (int)num);
			if (this._pos >= this._windowSize)
			{
				this._pos = 0U;
			}
			this._streamPos = this._pos;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000F750 File Offset: 0x0000D950
		public void CopyBlock(uint distance, uint len)
		{
			uint num = this._pos - distance - 1U;
			if (num >= this._windowSize)
			{
				num += this._windowSize;
			}
			while (len > 0U)
			{
				if (num >= this._windowSize)
				{
					num = 0U;
				}
				byte[] buffer = this._buffer;
				uint pos = this._pos;
				this._pos = pos + 1U;
				buffer[(int)pos] = this._buffer[(int)num++];
				if (this._pos >= this._windowSize)
				{
					this.Flush();
				}
				len -= 1U;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public void PutByte(byte b)
		{
			byte[] buffer = this._buffer;
			uint pos = this._pos;
			this._pos = pos + 1U;
			buffer[(int)pos] = b;
			if (this._pos >= this._windowSize)
			{
				this.Flush();
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000F804 File Offset: 0x0000DA04
		public byte GetByte(uint distance)
		{
			uint num = this._pos - distance - 1U;
			if (num >= this._windowSize)
			{
				num += this._windowSize;
			}
			return this._buffer[(int)num];
		}

		// Token: 0x040001DD RID: 477
		private byte[] _buffer;

		// Token: 0x040001DE RID: 478
		private uint _pos;

		// Token: 0x040001DF RID: 479
		private uint _windowSize;

		// Token: 0x040001E0 RID: 480
		private uint _streamPos;

		// Token: 0x040001E1 RID: 481
		private Stream _stream;

		// Token: 0x040001E2 RID: 482
		public uint TrainSize;
	}
}

using System;
using System.IO;

namespace CRS.Utilities.LZMA.Compress.LZ
{
	// Token: 0x0200004A RID: 74
	public class InWindow
	{
		// Token: 0x06000235 RID: 565 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		public void MoveBlock()
		{
			uint num = this._bufferOffset + this._pos - this._keepSizeBefore;
			if (num > 0U)
			{
				num -= 1U;
			}
			uint num2 = this._bufferOffset + this._streamPos - num;
			for (uint num3 = 0U; num3 < num2; num3 += 1U)
			{
				this._bufferBase[(int)num3] = this._bufferBase[(int)(num + num3)];
			}
			this._bufferOffset -= num;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000F348 File Offset: 0x0000D548
		public virtual void ReadBlock()
		{
			if (this._streamEndWasReached)
			{
				return;
			}
			for (;;)
			{
				int num = (int)(0U - this._bufferOffset + this._blockSize - this._streamPos);
				if (num == 0)
				{
					break;
				}
				int num2 = this._stream.Read(this._bufferBase, (int)(this._bufferOffset + this._streamPos), num);
				if (num2 == 0)
				{
					goto Block_3;
				}
				this._streamPos += (uint)num2;
				if (this._streamPos >= this._pos + this._keepSizeAfter)
				{
					this._posLimit = this._streamPos - this._keepSizeAfter;
				}
			}
			return;
			Block_3:
			this._posLimit = this._streamPos;
			if (this._bufferOffset + this._posLimit > this._pointerToLastSafePosition)
			{
				this._posLimit = this._pointerToLastSafePosition - this._bufferOffset;
			}
			this._streamEndWasReached = true;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000F415 File Offset: 0x0000D615
		private void Free()
		{
			this._bufferBase = null;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000F420 File Offset: 0x0000D620
		public void Create(uint keepSizeBefore, uint keepSizeAfter, uint keepSizeReserv)
		{
			this._keepSizeBefore = keepSizeBefore;
			this._keepSizeAfter = keepSizeAfter;
			uint num = keepSizeBefore + keepSizeAfter + keepSizeReserv;
			if (this._bufferBase == null || this._blockSize != num)
			{
				this.Free();
				this._blockSize = num;
				this._bufferBase = new byte[this._blockSize];
			}
			this._pointerToLastSafePosition = this._blockSize - keepSizeAfter;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000F47E File Offset: 0x0000D67E
		public void SetStream(Stream stream)
		{
			this._stream = stream;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000F487 File Offset: 0x0000D687
		public void ReleaseStream()
		{
			this._stream = null;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F490 File Offset: 0x0000D690
		public void Init()
		{
			this._bufferOffset = 0U;
			this._pos = 0U;
			this._streamPos = 0U;
			this._streamEndWasReached = false;
			this.ReadBlock();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		public void MovePos()
		{
			this._pos += 1U;
			if (this._pos > this._posLimit)
			{
				if (this._bufferOffset + this._pos > this._pointerToLastSafePosition)
				{
					this.MoveBlock();
				}
				this.ReadBlock();
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000F4F3 File Offset: 0x0000D6F3
		public byte GetIndexByte(int index)
		{
			checked
			{
				return this._bufferBase[(int)((IntPtr)(unchecked((ulong)(this._bufferOffset + this._pos) + (ulong)((long)index))))];
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000F510 File Offset: 0x0000D710
		public uint GetMatchLen(int index, uint distance, uint limit)
		{
			if (this._streamEndWasReached && (ulong)this._pos + (ulong)((long)index) + (ulong)limit > (ulong)this._streamPos)
			{
				limit = this._streamPos - (uint)((ulong)this._pos + (ulong)((long)index));
			}
			distance += 1U;
			uint num = this._bufferOffset + this._pos + (uint)index;
			uint num2 = 0U;
			while (num2 < limit && this._bufferBase[(int)(num + num2)] == this._bufferBase[(int)(num + num2 - distance)])
			{
				num2 += 1U;
			}
			return num2;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000F589 File Offset: 0x0000D789
		public uint GetNumAvailableBytes()
		{
			return this._streamPos - this._pos;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000F598 File Offset: 0x0000D798
		public void ReduceOffsets(int subValue)
		{
			this._bufferOffset += (uint)subValue;
			this._posLimit -= (uint)subValue;
			this._pos -= (uint)subValue;
			this._streamPos -= (uint)subValue;
		}

		// Token: 0x040001D2 RID: 466
		public byte[] _bufferBase;

		// Token: 0x040001D3 RID: 467
		private Stream _stream;

		// Token: 0x040001D4 RID: 468
		private uint _posLimit;

		// Token: 0x040001D5 RID: 469
		private bool _streamEndWasReached;

		// Token: 0x040001D6 RID: 470
		private uint _pointerToLastSafePosition;

		// Token: 0x040001D7 RID: 471
		public uint _bufferOffset;

		// Token: 0x040001D8 RID: 472
		public uint _blockSize;

		// Token: 0x040001D9 RID: 473
		public uint _pos;

		// Token: 0x040001DA RID: 474
		private uint _keepSizeBefore;

		// Token: 0x040001DB RID: 475
		private uint _keepSizeAfter;

		// Token: 0x040001DC RID: 476
		public uint _streamPos;
	}
}

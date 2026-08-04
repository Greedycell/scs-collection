using System;
using System.IO;

namespace CRS.Utilities.LZMA.Common
{
	// Token: 0x02000059 RID: 89
	public class InBuffer
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0001352A File Offset: 0x0001172A
		public InBuffer(uint bufferSize)
		{
			this.m_Buffer = new byte[bufferSize];
			this.m_BufferSize = bufferSize;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00013545 File Offset: 0x00011745
		public void Init(Stream stream)
		{
			this.m_Stream = stream;
			this.m_ProcessedSize = 0UL;
			this.m_Limit = 0U;
			this.m_Pos = 0U;
			this.m_StreamWasExhausted = false;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0001356C File Offset: 0x0001176C
		public bool ReadBlock()
		{
			if (this.m_StreamWasExhausted)
			{
				return false;
			}
			this.m_ProcessedSize += (ulong)this.m_Pos;
			int num = this.m_Stream.Read(this.m_Buffer, 0, (int)this.m_BufferSize);
			this.m_Pos = 0U;
			this.m_Limit = (uint)num;
			this.m_StreamWasExhausted = num == 0;
			return !this.m_StreamWasExhausted;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000135D1 File Offset: 0x000117D1
		public void ReleaseStream()
		{
			this.m_Stream = null;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000135DC File Offset: 0x000117DC
		public bool ReadByte(byte b)
		{
			if (this.m_Pos >= this.m_Limit && !this.ReadBlock())
			{
				return false;
			}
			byte[] buffer = this.m_Buffer;
			uint pos = this.m_Pos;
			this.m_Pos = pos + 1U;
			b = buffer[(int)pos];
			return true;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001361C File Offset: 0x0001181C
		public byte ReadByte()
		{
			if (this.m_Pos >= this.m_Limit && !this.ReadBlock())
			{
				return byte.MaxValue;
			}
			byte[] buffer = this.m_Buffer;
			uint pos = this.m_Pos;
			this.m_Pos = pos + 1U;
			return buffer[(int)pos];
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0001365D File Offset: 0x0001185D
		public ulong GetProcessedSize()
		{
			return this.m_ProcessedSize + (ulong)this.m_Pos;
		}

		// Token: 0x0400026E RID: 622
		private byte[] m_Buffer;

		// Token: 0x0400026F RID: 623
		private uint m_Pos;

		// Token: 0x04000270 RID: 624
		private uint m_Limit;

		// Token: 0x04000271 RID: 625
		private uint m_BufferSize;

		// Token: 0x04000272 RID: 626
		private Stream m_Stream;

		// Token: 0x04000273 RID: 627
		private bool m_StreamWasExhausted;

		// Token: 0x04000274 RID: 628
		private ulong m_ProcessedSize;
	}
}

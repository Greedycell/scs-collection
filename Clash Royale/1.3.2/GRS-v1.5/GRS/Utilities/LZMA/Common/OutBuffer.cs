using System;
using System.IO;

namespace CRS.Utilities.LZMA.Common
{
	// Token: 0x0200005A RID: 90
	public class OutBuffer
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0001366D File Offset: 0x0001186D
		public OutBuffer(uint bufferSize)
		{
			this.m_Buffer = new byte[bufferSize];
			this.m_BufferSize = bufferSize;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00013688 File Offset: 0x00011888
		public void SetStream(Stream stream)
		{
			this.m_Stream = stream;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00013691 File Offset: 0x00011891
		public void FlushStream()
		{
			this.m_Stream.Flush();
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0001369E File Offset: 0x0001189E
		public void CloseStream()
		{
			this.m_Stream.Close();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000136AB File Offset: 0x000118AB
		public void ReleaseStream()
		{
			this.m_Stream = null;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000136B4 File Offset: 0x000118B4
		public void Init()
		{
			this.m_ProcessedSize = 0UL;
			this.m_Pos = 0U;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000136C8 File Offset: 0x000118C8
		public void WriteByte(byte b)
		{
			byte[] buffer = this.m_Buffer;
			uint pos = this.m_Pos;
			this.m_Pos = pos + 1U;
			buffer[(int)pos] = b;
			if (this.m_Pos >= this.m_BufferSize)
			{
				this.FlushData();
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00013702 File Offset: 0x00011902
		public void FlushData()
		{
			if (this.m_Pos == 0U)
			{
				return;
			}
			this.m_Stream.Write(this.m_Buffer, 0, (int)this.m_Pos);
			this.m_Pos = 0U;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0001372C File Offset: 0x0001192C
		public ulong GetProcessedSize()
		{
			return this.m_ProcessedSize + (ulong)this.m_Pos;
		}

		// Token: 0x04000275 RID: 629
		private byte[] m_Buffer;

		// Token: 0x04000276 RID: 630
		private uint m_Pos;

		// Token: 0x04000277 RID: 631
		private uint m_BufferSize;

		// Token: 0x04000278 RID: 632
		private Stream m_Stream;

		// Token: 0x04000279 RID: 633
		private ulong m_ProcessedSize;
	}
}

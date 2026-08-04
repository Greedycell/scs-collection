using System;

namespace CRS.Utilities.ZLib
{
	// Token: 0x02000020 RID: 32
	internal class WorkItem
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00009C60 File Offset: 0x00007E60
		public WorkItem(int size, CompressionLevel compressLevel, CompressionStrategy strategy, int ix)
		{
			this.buffer = new byte[size];
			int num = size + (size / 32768 + 1) * 5 * 2;
			this.compressed = new byte[num];
			this.compressor = new ZlibCodec();
			this.compressor.InitializeDeflate(compressLevel, false);
			this.compressor.OutputBuffer = this.compressed;
			this.compressor.InputBuffer = this.buffer;
			this.index = ix;
		}

		// Token: 0x040000E4 RID: 228
		public byte[] buffer;

		// Token: 0x040000E5 RID: 229
		public byte[] compressed;

		// Token: 0x040000E6 RID: 230
		public int compressedBytesAvailable;

		// Token: 0x040000E7 RID: 231
		public ZlibCodec compressor;

		// Token: 0x040000E8 RID: 232
		public int crc;

		// Token: 0x040000E9 RID: 233
		public int index;

		// Token: 0x040000EA RID: 234
		public int inputBytesAvailable;

		// Token: 0x040000EB RID: 235
		public int ordinal;
	}
}

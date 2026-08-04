using System;

namespace Ionic.Zlib
{
	// Token: 0x0200000D RID: 13
	internal class WorkItem
	{
		// Token: 0x06000091 RID: 145 RVA: 0x000084CC File Offset: 0x000066CC
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

		// Token: 0x040000B0 RID: 176
		public byte[] buffer;

		// Token: 0x040000B1 RID: 177
		public byte[] compressed;

		// Token: 0x040000B2 RID: 178
		public int compressedBytesAvailable;

		// Token: 0x040000B3 RID: 179
		public ZlibCodec compressor;

		// Token: 0x040000B4 RID: 180
		public int crc;

		// Token: 0x040000B5 RID: 181
		public int index;

		// Token: 0x040000B6 RID: 182
		public int inputBytesAvailable;

		// Token: 0x040000B7 RID: 183
		public int ordinal;
	}
}

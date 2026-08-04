using System;
using System.IO;

namespace CRS.Utilities.LZMA.Compress.LZ
{
	// Token: 0x02000047 RID: 71
	internal interface IInWindowStream
	{
		// Token: 0x0600021D RID: 541
		void SetStream(Stream inStream);

		// Token: 0x0600021E RID: 542
		void Init();

		// Token: 0x0600021F RID: 543
		void ReleaseStream();

		// Token: 0x06000220 RID: 544
		byte GetIndexByte(int index);

		// Token: 0x06000221 RID: 545
		uint GetMatchLen(int index, uint distance, uint limit);

		// Token: 0x06000222 RID: 546
		uint GetNumAvailableBytes();
	}
}

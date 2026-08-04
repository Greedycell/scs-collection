using System;

namespace Ionic.Zlib
{
	// Token: 0x0200001C RID: 28
	public static class ZlibConstants
	{
		// Token: 0x04000138 RID: 312
		public const int WindowBitsMax = 15;

		// Token: 0x04000139 RID: 313
		public const int WindowBitsDefault = 15;

		// Token: 0x0400013A RID: 314
		public const int Z_OK = 0;

		// Token: 0x0400013B RID: 315
		public const int Z_STREAM_END = 1;

		// Token: 0x0400013C RID: 316
		public const int Z_NEED_DICT = 2;

		// Token: 0x0400013D RID: 317
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x0400013E RID: 318
		public const int Z_DATA_ERROR = -3;

		// Token: 0x0400013F RID: 319
		public const int Z_BUF_ERROR = -5;

		// Token: 0x04000140 RID: 320
		public const int WorkingBufferSizeDefault = 16384;

		// Token: 0x04000141 RID: 321
		public const int WorkingBufferSizeMin = 1024;
	}
}

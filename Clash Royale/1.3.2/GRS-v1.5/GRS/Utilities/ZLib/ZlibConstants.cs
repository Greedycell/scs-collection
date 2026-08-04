using System;

namespace CRS.Utilities.ZLib
{
	// Token: 0x0200002F RID: 47
	public static class ZlibConstants
	{
		// Token: 0x0400016C RID: 364
		public const int WindowBitsMax = 15;

		// Token: 0x0400016D RID: 365
		public const int WindowBitsDefault = 15;

		// Token: 0x0400016E RID: 366
		public const int Z_OK = 0;

		// Token: 0x0400016F RID: 367
		public const int Z_STREAM_END = 1;

		// Token: 0x04000170 RID: 368
		public const int Z_NEED_DICT = 2;

		// Token: 0x04000171 RID: 369
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x04000172 RID: 370
		public const int Z_DATA_ERROR = -3;

		// Token: 0x04000173 RID: 371
		public const int Z_BUF_ERROR = -5;

		// Token: 0x04000174 RID: 372
		public const int WorkingBufferSizeDefault = 16384;

		// Token: 0x04000175 RID: 373
		public const int WorkingBufferSizeMin = 1024;
	}
}

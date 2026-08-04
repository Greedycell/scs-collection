using System;

namespace CRS.Utilities.LZMA.Compress.LZ
{
	// Token: 0x02000048 RID: 72
	internal interface IMatchFinder : IInWindowStream
	{
		// Token: 0x06000223 RID: 547
		void Create(uint historySize, uint keepAddBufferBefore, uint matchMaxLen, uint keepAddBufferAfter);

		// Token: 0x06000224 RID: 548
		uint GetMatches(uint[] distances);

		// Token: 0x06000225 RID: 549
		void Skip(uint num);
	}
}

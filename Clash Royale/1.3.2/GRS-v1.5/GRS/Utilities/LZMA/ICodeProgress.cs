using System;

namespace CRS.Utilities.LZMA
{
	// Token: 0x0200003B RID: 59
	public interface ICodeProgress
	{
		// Token: 0x060001EB RID: 491
		void SetProgress(long inSize, long outSize);
	}
}

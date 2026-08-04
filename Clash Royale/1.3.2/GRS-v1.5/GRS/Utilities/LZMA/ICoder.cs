using System;
using System.IO;

namespace CRS.Utilities.LZMA
{
	// Token: 0x0200003C RID: 60
	public interface ICoder
	{
		// Token: 0x060001EC RID: 492
		void Code(Stream inStream, Stream outStream, long inSize, long outSize, ICodeProgress progress);
	}
}

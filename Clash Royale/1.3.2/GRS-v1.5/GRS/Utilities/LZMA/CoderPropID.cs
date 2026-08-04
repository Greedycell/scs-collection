using System;

namespace CRS.Utilities.LZMA
{
	// Token: 0x0200003D RID: 61
	public enum CoderPropID
	{
		// Token: 0x04000196 RID: 406
		DefaultProp,
		// Token: 0x04000197 RID: 407
		DictionarySize,
		// Token: 0x04000198 RID: 408
		UsedMemorySize,
		// Token: 0x04000199 RID: 409
		Order,
		// Token: 0x0400019A RID: 410
		BlockSize,
		// Token: 0x0400019B RID: 411
		PosStateBits,
		// Token: 0x0400019C RID: 412
		LitContextBits,
		// Token: 0x0400019D RID: 413
		LitPosBits,
		// Token: 0x0400019E RID: 414
		NumFastBytes,
		// Token: 0x0400019F RID: 415
		MatchFinder,
		// Token: 0x040001A0 RID: 416
		MatchFinderCycles,
		// Token: 0x040001A1 RID: 417
		NumPasses,
		// Token: 0x040001A2 RID: 418
		Algorithm,
		// Token: 0x040001A3 RID: 419
		NumThreads,
		// Token: 0x040001A4 RID: 420
		EndMarker
	}
}

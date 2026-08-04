using System;
using System.Collections;

namespace CRS.Utilities.LZMA.Common
{
	// Token: 0x02000054 RID: 84
	public class SwitchResult
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00012FD9 File Offset: 0x000111D9
		public SwitchResult()
		{
			this.ThereIs = false;
		}

		// Token: 0x0400025E RID: 606
		public int PostCharIndex;

		// Token: 0x0400025F RID: 607
		public ArrayList PostStrings = new ArrayList();

		// Token: 0x04000260 RID: 608
		public bool ThereIs;

		// Token: 0x04000261 RID: 609
		public bool WithMinus;
	}
}

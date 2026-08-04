using System;

namespace CRS.Utilities.LZMA.Common
{
	// Token: 0x02000053 RID: 83
	public class SwitchForm
	{
		// Token: 0x06000290 RID: 656 RVA: 0x00012F85 File Offset: 0x00011185
		public SwitchForm(string idString, SwitchType type, bool multi, int minLen, int maxLen, string postCharSet)
		{
			this.IDString = idString;
			this.Type = type;
			this.Multi = multi;
			this.MinLen = minLen;
			this.MaxLen = maxLen;
			this.PostCharSet = postCharSet;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00012FBA File Offset: 0x000111BA
		public SwitchForm(string idString, SwitchType type, bool multi, int minLen)
			: this(idString, type, multi, minLen, 0, "")
		{
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00012FCD File Offset: 0x000111CD
		public SwitchForm(string idString, SwitchType type, bool multi)
			: this(idString, type, multi, 0)
		{
		}

		// Token: 0x04000258 RID: 600
		public string IDString;

		// Token: 0x04000259 RID: 601
		public int MaxLen;

		// Token: 0x0400025A RID: 602
		public int MinLen;

		// Token: 0x0400025B RID: 603
		public bool Multi;

		// Token: 0x0400025C RID: 604
		public string PostCharSet;

		// Token: 0x0400025D RID: 605
		public SwitchType Type;
	}
}

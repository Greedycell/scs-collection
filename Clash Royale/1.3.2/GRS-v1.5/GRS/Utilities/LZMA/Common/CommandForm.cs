using System;

namespace CRS.Utilities.LZMA.Common
{
	// Token: 0x02000056 RID: 86
	public class CommandForm
	{
		// Token: 0x0600029B RID: 667 RVA: 0x000133FE File Offset: 0x000115FE
		public CommandForm(string idString, bool postStringMode)
		{
			this.IDString = idString;
			this.PostStringMode = postStringMode;
		}

		// Token: 0x04000268 RID: 616
		public string IDString = "";

		// Token: 0x04000269 RID: 617
		public bool PostStringMode;
	}
}

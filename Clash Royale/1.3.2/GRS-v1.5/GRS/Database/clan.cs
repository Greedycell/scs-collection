using System;

namespace CRS.Database
{
	// Token: 0x02000011 RID: 17
	public class clan
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003083 File Offset: 0x00001283
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000308B File Offset: 0x0000128B
		public long ClanId { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003094 File Offset: 0x00001294
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000309C File Offset: 0x0000129C
		public DateTime LastUpdateTime { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000030A5 File Offset: 0x000012A5
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000030AD File Offset: 0x000012AD
		public string Data { get; set; }
	}
}

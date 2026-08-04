using System;

namespace CRS.Database
{
	// Token: 0x02000012 RID: 18
	public class player
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000030B6 File Offset: 0x000012B6
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000030BE File Offset: 0x000012BE
		public long PlayerId { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000030C7 File Offset: 0x000012C7
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000030CF File Offset: 0x000012CF
		public byte AccountStatus { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000030D8 File Offset: 0x000012D8
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000030E0 File Offset: 0x000012E0
		public byte AccountPrivileges { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000030E9 File Offset: 0x000012E9
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000030F1 File Offset: 0x000012F1
		public DateTime LastUpdateTime { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000030FA File Offset: 0x000012FA
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003102 File Offset: 0x00001302
		public string IPAddress { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000310B File Offset: 0x0000130B
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003113 File Offset: 0x00001313
		public string Avatar { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000311C File Offset: 0x0000131C
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003124 File Offset: 0x00001324
		public string GameObjects { get; set; }
	}
}

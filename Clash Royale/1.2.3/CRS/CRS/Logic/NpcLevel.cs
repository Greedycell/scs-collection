using System;

namespace UCS.Logic
{
	// Token: 0x020000B1 RID: 177
	internal class NpcLevel
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x000073DF File Offset: 0x000055DF
		public NpcLevel()
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000156B8 File Offset: 0x000138B8
		public NpcLevel(int index)
		{
			this.Index = index;
			this.Stars = 0;
			this.LootedGold = 0;
			this.LootedElixir = 0;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x000156DC File Offset: 0x000138DC
		public int Id
		{
			get
			{
				return 17000000 + this.Index;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000156EA File Offset: 0x000138EA
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x000156F2 File Offset: 0x000138F2
		public int Index { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x000156FB File Offset: 0x000138FB
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00015703 File Offset: 0x00013903
		public int LootedElixir { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001570C File Offset: 0x0001390C
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00015714 File Offset: 0x00013914
		public int LootedGold { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0001571D File Offset: 0x0001391D
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00015725 File Offset: 0x00013925
		public string Name { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001572E File Offset: 0x0001392E
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x00015736 File Offset: 0x00013936
		public int Stars { get; set; }

		// Token: 0x040002F5 RID: 757
		private const int m_vType = 17000000;
	}
}

using System;

namespace UCS.Logic
{
	// Token: 0x020000AF RID: 175
	internal class Achievement
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x000073DF File Offset: 0x000055DF
		public Achievement()
		{
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00015649 File Offset: 0x00013849
		public Achievement(int index)
		{
			this.Index = index;
			this.Unlocked = false;
			this.Value = 0;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00015666 File Offset: 0x00013866
		public int Id
		{
			get
			{
				return 23000000 + this.Index;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00015674 File Offset: 0x00013874
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x0001567C File Offset: 0x0001387C
		public int Index { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00015685 File Offset: 0x00013885
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x0001568D File Offset: 0x0001388D
		public string Name { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00015696 File Offset: 0x00013896
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0001569E File Offset: 0x0001389E
		public bool Unlocked { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x000156A7 File Offset: 0x000138A7
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x000156AF File Offset: 0x000138AF
		public int Value { get; set; }

		// Token: 0x040002F0 RID: 752
		private const int m_vType = 23000000;
	}
}

using System;

namespace CRS.Logic
{
	// Token: 0x020000D1 RID: 209
	internal class Achievement
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x000020FF File Offset: 0x000002FF
		public Achievement()
		{
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001D3F5 File Offset: 0x0001B5F5
		public Achievement(int index)
		{
			this.Index = index;
			this.Unlocked = false;
			this.Value = 0;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001D412 File Offset: 0x0001B612
		public int Id
		{
			get
			{
				return 23000000 + this.Index;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001D420 File Offset: 0x0001B620
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x0001D428 File Offset: 0x0001B628
		public int Index { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0001D431 File Offset: 0x0001B631
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x0001D439 File Offset: 0x0001B639
		public string Name { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0001D442 File Offset: 0x0001B642
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0001D44A File Offset: 0x0001B64A
		public bool Unlocked { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001D453 File Offset: 0x0001B653
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0001D45B File Offset: 0x0001B65B
		public int Value { get; set; }

		// Token: 0x0400038B RID: 907
		private const int m_vType = 23000000;
	}
}

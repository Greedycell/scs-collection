using System;
using System.Collections.Generic;
using CRS.Files.Logic;

namespace CRS.Logic
{
	// Token: 0x020000CC RID: 204
	internal class Avatar
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0001C418 File Offset: 0x0001A618
		public Avatar()
		{
			this.m_vResources = new List<DataSlot>();
			this.m_vResourceCaps = new List<DataSlot>();
			this.m_vUnitCount = new List<DataSlot>();
			this.m_vUnitUpgradeLevel = new List<DataSlot>();
			this.m_vHeroHealth = new List<DataSlot>();
			this.m_vHeroUpgradeLevel = new List<DataSlot>();
			this.m_vHeroState = new List<DataSlot>();
			this.m_vSpellCount = new List<DataSlot>();
			this.m_vSpellUpgradeLevel = new List<DataSlot>();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001C490 File Offset: 0x0001A690
		public static int GetDataIndex(List<DataSlot> dsl, Data d)
		{
			return dsl.FindIndex((DataSlot ds) => ds.Data == d);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001C4BC File Offset: 0x0001A6BC
		public List<DataSlot> GetResourceCaps()
		{
			return this.m_vResourceCaps;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001C4C4 File Offset: 0x0001A6C4
		public List<DataSlot> GetResources()
		{
			return this.m_vResources;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001C4CC File Offset: 0x0001A6CC
		public List<DataSlot> GetSpells()
		{
			return this.m_vSpellCount;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001C4D4 File Offset: 0x0001A6D4
		public List<DataSlot> GetUnits()
		{
			return this.m_vUnitCount;
		}

		// Token: 0x0400035B RID: 859
		protected List<DataSlot> m_vHeroHealth;

		// Token: 0x0400035C RID: 860
		protected List<DataSlot> m_vHeroState;

		// Token: 0x0400035D RID: 861
		protected List<DataSlot> m_vHeroUpgradeLevel;

		// Token: 0x0400035E RID: 862
		protected List<DataSlot> m_vResourceCaps;

		// Token: 0x0400035F RID: 863
		protected List<DataSlot> m_vResources;

		// Token: 0x04000360 RID: 864
		protected List<DataSlot> m_vSpellCount;

		// Token: 0x04000361 RID: 865
		protected List<DataSlot> m_vSpellUpgradeLevel;

		// Token: 0x04000362 RID: 866
		protected List<DataSlot> m_vUnitCount;

		// Token: 0x04000363 RID: 867
		protected List<DataSlot> m_vUnitUpgradeLevel;
	}
}

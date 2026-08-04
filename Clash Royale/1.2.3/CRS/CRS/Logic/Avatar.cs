using System;
using System.Collections.Generic;
using UCS.Files;

namespace UCS.Logic
{
	// Token: 0x0200009A RID: 154
	internal class Avatar
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x00013AA0 File Offset: 0x00011CA0
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

		// Token: 0x0600043A RID: 1082 RVA: 0x00013B18 File Offset: 0x00011D18
		public static int GetDataIndex(List<DataSlot> dsl, Data d)
		{
			return dsl.FindIndex((DataSlot ds) => ds.Data == d);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00013B44 File Offset: 0x00011D44
		public List<DataSlot> GetResourceCaps()
		{
			return this.m_vResourceCaps;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013B4C File Offset: 0x00011D4C
		public List<DataSlot> GetResources()
		{
			return this.m_vResources;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00013B54 File Offset: 0x00011D54
		public List<DataSlot> GetSpells()
		{
			return this.m_vSpellCount;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013B5C File Offset: 0x00011D5C
		public List<DataSlot> GetUnits()
		{
			return this.m_vUnitCount;
		}

		// Token: 0x0400029B RID: 667
		protected List<DataSlot> m_vHeroHealth;

		// Token: 0x0400029C RID: 668
		protected List<DataSlot> m_vHeroState;

		// Token: 0x0400029D RID: 669
		protected List<DataSlot> m_vHeroUpgradeLevel;

		// Token: 0x0400029E RID: 670
		protected List<DataSlot> m_vResourceCaps;

		// Token: 0x0400029F RID: 671
		protected List<DataSlot> m_vResources;

		// Token: 0x040002A0 RID: 672
		protected List<DataSlot> m_vSpellCount;

		// Token: 0x040002A1 RID: 673
		protected List<DataSlot> m_vSpellUpgradeLevel;

		// Token: 0x040002A2 RID: 674
		protected List<DataSlot> m_vUnitCount;

		// Token: 0x040002A3 RID: 675
		protected List<DataSlot> m_vUnitUpgradeLevel;
	}
}

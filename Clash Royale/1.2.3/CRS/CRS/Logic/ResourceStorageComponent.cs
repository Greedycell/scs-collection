using System;
using System.Collections.Generic;
using UCS.Core;

namespace UCS.Logic
{
	// Token: 0x020000A6 RID: 166
	internal class ResourceStorageComponent : Component
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x0001453C File Offset: 0x0001273C
		public ResourceStorageComponent(GameObject go)
			: base(go)
		{
			this.m_vCurrentResources = new List<int>();
			this.m_vMaxResources = new List<int>();
			this.m_vStolenResources = new List<int>();
			int itemCount = ObjectManager.DataTables.GetTable(2).GetItemCount();
			for (int i = 0; i < itemCount; i++)
			{
				this.m_vCurrentResources.Add(0);
				this.m_vMaxResources.Add(0);
				this.m_vStolenResources.Add(0);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00013DA0 File Offset: 0x00011FA0
		public override int Type
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000145B2 File Offset: 0x000127B2
		public int GetCount(int resourceIndex)
		{
			return this.m_vCurrentResources[resourceIndex];
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000145C0 File Offset: 0x000127C0
		public int GetMax(int resourceIndex)
		{
			return this.m_vMaxResources[resourceIndex];
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000145CE File Offset: 0x000127CE
		public void SetMaxArray(List<int> resourceCaps)
		{
			this.m_vMaxResources = resourceCaps;
		}

		// Token: 0x040002C6 RID: 710
		private readonly List<int> m_vCurrentResources;

		// Token: 0x040002C7 RID: 711
		private readonly List<int> m_vStolenResources;

		// Token: 0x040002C8 RID: 712
		private List<int> m_vMaxResources;
	}
}

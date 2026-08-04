using System;
using System.Collections.Generic;

namespace UCS.Logic
{
	// Token: 0x020000A1 RID: 161
	internal class GameObjectFilter
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x00014308 File Offset: 0x00012508
		public void AddIgnoreObject(GameObject go)
		{
			if (this.m_vIgnoredObjects == null)
			{
				this.m_vIgnoredObjects = new List<int>();
			}
			this.m_vIgnoredObjects.Add(go.GlobalId);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000475F File Offset: 0x0000295F
		public virtual bool IsComponentFilter()
		{
			return false;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001432E File Offset: 0x0001252E
		public void RemoveAllIgnoreObjects()
		{
			if (this.m_vIgnoredObjects != null)
			{
				this.m_vIgnoredObjects.Clear();
				this.m_vIgnoredObjects = null;
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001434C File Offset: 0x0001254C
		public bool TestGameObject(GameObject go)
		{
			bool flag = true;
			if (this.m_vIgnoredObjects != null)
			{
				flag = this.m_vIgnoredObjects.IndexOf(go.GlobalId) == -1;
			}
			return flag;
		}

		// Token: 0x040002C0 RID: 704
		private List<int> m_vIgnoredObjects;
	}
}

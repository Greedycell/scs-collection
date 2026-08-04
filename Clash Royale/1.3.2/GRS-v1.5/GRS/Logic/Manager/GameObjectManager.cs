using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CRS.Logic.Manager
{
	// Token: 0x020000D3 RID: 211
	internal class GameObjectManager
	{
		// Token: 0x0600051D RID: 1309 RVA: 0x0001DB17 File Offset: 0x0001BD17
		public GameObjectManager(Level l)
		{
			this.m_vLevel = l;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000123B6 File Offset: 0x000105B6
		public void Load(JObject jsonObject)
		{
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001DB26 File Offset: 0x0001BD26
		public JObject Save()
		{
			return new JObject();
		}

		// Token: 0x0400039D RID: 925
		private readonly List<int> m_vGameObjectsIndex;

		// Token: 0x0400039E RID: 926
		private readonly Level m_vLevel;
	}
}

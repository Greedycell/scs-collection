using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace UCS.Logic
{
	// Token: 0x020000AB RID: 171
	internal class GameObjectManager
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x000147E8 File Offset: 0x000129E8
		public GameObjectManager(Level l)
		{
			this.m_vLevel = l;
			this.m_vGameObjects = new List<List<GameObject>>();
			this.m_vGameObjectsIndex = new List<int>();
			for (int i = 0; i < 7; i++)
			{
				this.m_vGameObjects.Add(new List<GameObject>());
				this.m_vGameObjectsIndex.Add(0);
			}
			this.m_vComponentManager = new ComponentManager(this.m_vLevel);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public void Load(JObject jsonObject)
		{
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public void RemoveGameObject(GameObject go)
		{
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00014851 File Offset: 0x00012A51
		public JObject Save()
		{
			return new JObject();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public void Tick()
		{
		}

		// Token: 0x040002CB RID: 715
		private readonly ComponentManager m_vComponentManager;

		// Token: 0x040002CC RID: 716
		private readonly List<List<GameObject>> m_vGameObjects;

		// Token: 0x040002CD RID: 717
		private readonly List<int> m_vGameObjectsIndex;

		// Token: 0x040002CE RID: 718
		private readonly Level m_vLevel;
	}
}

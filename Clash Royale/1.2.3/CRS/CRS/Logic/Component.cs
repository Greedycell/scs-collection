using System;
using Newtonsoft.Json.Linq;

namespace UCS.Logic
{
	// Token: 0x020000A4 RID: 164
	internal class Component
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x000073DF File Offset: 0x000055DF
		public Component()
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00014502 File Offset: 0x00012702
		public Component(GameObject go)
		{
			this.m_vIsEnabled = true;
			this.m_vParentGameObject = go;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00013CBC File Offset: 0x00011EBC
		public virtual int Type
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00014518 File Offset: 0x00012718
		public GameObject GetParent()
		{
			return this.m_vParentGameObject;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00014520 File Offset: 0x00012720
		public bool IsEnabled()
		{
			return this.m_vIsEnabled;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public virtual void Load(JObject jsonObject)
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000021A5 File Offset: 0x000003A5
		public virtual JObject Save(JObject jsonObject)
		{
			return jsonObject;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00014528 File Offset: 0x00012728
		public void SetEnabled(bool status)
		{
			this.m_vIsEnabled = status;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public virtual void Tick()
		{
		}

		// Token: 0x040002C4 RID: 708
		private readonly GameObject m_vParentGameObject;

		// Token: 0x040002C5 RID: 709
		private bool m_vIsEnabled;
	}
}

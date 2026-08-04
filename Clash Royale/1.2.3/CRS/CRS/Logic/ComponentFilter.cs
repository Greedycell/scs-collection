using System;

namespace UCS.Logic
{
	// Token: 0x020000A2 RID: 162
	internal class ComponentFilter : GameObjectFilter
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x00014379 File Offset: 0x00012579
		public ComponentFilter(int type)
		{
			this.Type = type;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000135A3 File Offset: 0x000117A3
		public override bool IsComponentFilter()
		{
			return true;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00014388 File Offset: 0x00012588
		public bool TestComponent(Component c)
		{
			GameObject parent = c.GetParent();
			return this.TestGameObject(parent);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000143A4 File Offset: 0x000125A4
		public new bool TestGameObject(GameObject go)
		{
			bool flag = false;
			if (go.GetComponent(this.Type, true) != null)
			{
				flag = base.TestGameObject(go);
			}
			return flag;
		}

		// Token: 0x040002C1 RID: 705
		public int Type;
	}
}

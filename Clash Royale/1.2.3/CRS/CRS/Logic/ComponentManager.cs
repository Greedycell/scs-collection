using System;
using System.Collections.Generic;
using System.Windows;

namespace UCS.Logic
{
	// Token: 0x020000AA RID: 170
	internal class ComponentManager
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x000145E0 File Offset: 0x000127E0
		public ComponentManager(Level l)
		{
			this.m_vComponents = new List<List<Component>>();
			for (int i = 0; i <= 10; i++)
			{
				this.m_vComponents.Add(new List<Component>());
			}
			this.m_vLevel = l;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00014622 File Offset: 0x00012822
		public void AddComponent(Component c)
		{
			this.m_vComponents[c.Type].Add(c);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001463C File Offset: 0x0001283C
		public Component GetClosestComponent(int x, int y, ComponentFilter cf)
		{
			Component component = null;
			int type = cf.Type;
			List<Component> list = this.m_vComponents[type];
			Vector vector = new Vector((double)x, (double)y);
			double num = 0.0;
			if (list.Count > 0)
			{
				foreach (Component component2 in list)
				{
					if (cf.TestComponent(component2))
					{
						GameObject parent = component2.GetParent();
						double lengthSquared = (vector - parent.GetPosition()).LengthSquared;
						if (lengthSquared < num || component == null)
						{
							num = lengthSquared;
							component = component2;
						}
					}
				}
			}
			return component;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000146F8 File Offset: 0x000128F8
		public List<Component> GetComponents(int type)
		{
			return this.m_vComponents[type];
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00014708 File Offset: 0x00012908
		public void RemoveGameObjectReferences(GameObject go)
		{
			foreach (List<Component> list in this.m_vComponents)
			{
				List<Component> list2 = new List<Component>();
				foreach (Component component in list)
				{
					if (component.GetParent() == go)
					{
						list2.Add(component);
					}
				}
				foreach (Component component2 in list2)
				{
					list.Remove(component2);
				}
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public void Tick()
		{
		}

		// Token: 0x040002C9 RID: 713
		private readonly List<List<Component>> m_vComponents;

		// Token: 0x040002CA RID: 714
		private readonly Level m_vLevel;
	}
}

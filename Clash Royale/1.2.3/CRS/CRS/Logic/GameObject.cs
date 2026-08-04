using System;
using System.Collections.Generic;
using System.Windows;
using Newtonsoft.Json.Linq;
using UCS.Files;

namespace UCS.Logic
{
	// Token: 0x020000B2 RID: 178
	internal class GameObject
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x00015740 File Offset: 0x00013940
		public GameObject(Data data, Level level)
		{
			this.m_vLevel = level;
			this.m_vData = data;
			this.m_vComponents = new List<Component>();
			for (int i = 0; i < 11; i++)
			{
				this.m_vComponents.Add(new Component());
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00013CBC File Offset: 0x00011EBC
		public virtual int ClassId
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00015789 File Offset: 0x00013989
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x00015791 File Offset: 0x00013991
		public int GlobalId { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0001579A File Offset: 0x0001399A
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x000157A2 File Offset: 0x000139A2
		public int X { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000157AB File Offset: 0x000139AB
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x000157B3 File Offset: 0x000139B3
		public int Y { get; set; }

		// Token: 0x0600051E RID: 1310 RVA: 0x000157BC File Offset: 0x000139BC
		public void AddComponent(Component c)
		{
			if (this.m_vComponents[c.Type].Type == -1)
			{
				this.m_vComponents[c.Type] = c;
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000157EC File Offset: 0x000139EC
		public Component GetComponent(int index, bool test)
		{
			Component component = null;
			if (!test || this.m_vComponents[index].IsEnabled())
			{
				component = this.m_vComponents[index];
			}
			return component;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001581F File Offset: 0x00013A1F
		public Data GetData()
		{
			return this.m_vData;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00015827 File Offset: 0x00013A27
		public Level GetLevel()
		{
			return this.m_vLevel;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001582F File Offset: 0x00013A2F
		public Vector GetPosition()
		{
			return new Vector((double)this.X, (double)this.Y);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000475F File Offset: 0x0000295F
		public virtual bool IsHero()
		{
			return false;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00015844 File Offset: 0x00013A44
		public void Load(JObject jsonObject)
		{
			this.X = jsonObject["x"].ToObject<int>();
			this.Y = jsonObject["y"].ToObject<int>();
			foreach (Component component in this.m_vComponents)
			{
				component.Load(jsonObject);
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000158C4 File Offset: 0x00013AC4
		public JObject Save(JObject jsonObject)
		{
			jsonObject.Add("x", this.X);
			jsonObject.Add("y", this.Y);
			foreach (Component component in this.m_vComponents)
			{
				component.Save(jsonObject);
			}
			return jsonObject;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00015944 File Offset: 0x00013B44
		public void SetPositionXY(int newX, int newY)
		{
			this.X = newX;
			this.Y = newY;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00015954 File Offset: 0x00013B54
		public virtual void Tick()
		{
			foreach (Component component in this.m_vComponents)
			{
				if (component.IsEnabled())
				{
					component.Tick();
				}
			}
		}

		// Token: 0x040002FB RID: 763
		private readonly List<Component> m_vComponents;

		// Token: 0x040002FC RID: 764
		private readonly Data m_vData;

		// Token: 0x040002FD RID: 765
		private readonly Level m_vLevel;
	}
}

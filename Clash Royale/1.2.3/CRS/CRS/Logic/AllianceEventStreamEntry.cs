using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x0200009E RID: 158
	internal class AllianceEventStreamEntry : StreamEntry
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00013DD0 File Offset: 0x00011FD0
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddInt32(this.m_vEventType);
			list.AddInt64(this.m_vAvatarId);
			list.AddString(this.m_vAvatarName);
			return list.ToArray();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00013E0C File Offset: 0x0001200C
		public override int GetStreamEntryType()
		{
			return 4;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00013E0F File Offset: 0x0001200F
		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			jsonObject["avatar_name"].ToObject<string>();
			jsonObject["event_type"].ToObject<int>();
			jsonObject["avatar_id"].ToObject<long>();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00013E4C File Offset: 0x0001204C
		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("avatar_name", this.m_vAvatarName);
			jsonObject.Add("event_type", this.m_vEventType);
			jsonObject.Add("avatar_id", this.m_vAvatarId);
			return jsonObject;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00013EA5 File Offset: 0x000120A5
		public void SetAvatarId(long id)
		{
			this.m_vAvatarId = id;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00013EAE File Offset: 0x000120AE
		public void SetAvatarName(string name)
		{
			this.m_vAvatarName = name;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00013EB7 File Offset: 0x000120B7
		public void SetEventType(int type)
		{
			this.m_vEventType = type;
		}

		// Token: 0x040002B1 RID: 689
		private long m_vAvatarId;

		// Token: 0x040002B2 RID: 690
		private string m_vAvatarName;

		// Token: 0x040002B3 RID: 691
		private int m_vEventType;
	}
}

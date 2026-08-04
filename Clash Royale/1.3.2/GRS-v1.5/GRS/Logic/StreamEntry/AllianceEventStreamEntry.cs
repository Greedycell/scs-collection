using System;
using System.Collections.Generic;
using CRS.Helpers;
using Newtonsoft.Json.Linq;

namespace CRS.Logic.StreamEntry
{
	// Token: 0x020000D7 RID: 215
	internal class AllianceEventStreamEntry : StreamEntry
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x0001E0D3 File Offset: 0x0001C2D3
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddInt32(this.m_vEventType);
			list.AddInt64(this.m_vAvatarId);
			list.AddString(this.m_vAvatarName);
			return list.ToArray();
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001E10F File Offset: 0x0001C30F
		public override int GetStreamEntryType()
		{
			return 4;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001E112 File Offset: 0x0001C312
		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			jsonObject["avatar_name"].ToObject<string>();
			jsonObject["event_type"].ToObject<int>();
			jsonObject["avatar_id"].ToObject<long>();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001E150 File Offset: 0x0001C350
		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("avatar_name", this.m_vAvatarName);
			jsonObject.Add("event_type", this.m_vEventType);
			jsonObject.Add("avatar_id", this.m_vAvatarId);
			return jsonObject;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001E1A9 File Offset: 0x0001C3A9
		public void SetAvatarId(long id)
		{
			this.m_vAvatarId = id;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001E1B2 File Offset: 0x0001C3B2
		public void SetAvatarName(string name)
		{
			this.m_vAvatarName = name;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001E1BB File Offset: 0x0001C3BB
		public void SetEventType(int type)
		{
			this.m_vEventType = type;
		}

		// Token: 0x040003B4 RID: 948
		private long m_vAvatarId;

		// Token: 0x040003B5 RID: 949
		private string m_vAvatarName;

		// Token: 0x040003B6 RID: 950
		private int m_vEventType;
	}
}

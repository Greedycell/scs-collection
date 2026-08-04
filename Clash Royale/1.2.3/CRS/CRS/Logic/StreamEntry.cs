using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x0200009F RID: 159
	internal class StreamEntry
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x00013EC0 File Offset: 0x000120C0
		public StreamEntry()
		{
			this.m_vMessageTime = DateTime.UtcNow;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00013EDC File Offset: 0x000120DC
		public virtual byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(this.GetStreamEntryType());
			list.AddInt32(0);
			list.AddInt32(this.m_vId);
			list.Add(3);
			list.AddInt64(this.m_vSenderId);
			list.AddInt64(this.m_vHomeId);
			list.AddString(this.m_vSenderName);
			list.AddInt32(this.m_vSenderLevel);
			list.AddInt32(this.m_vSenderLeagueId);
			list.AddInt32(this.m_vSenderRole);
			list.AddInt32(this.GetAgeSeconds());
			return list.ToArray();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00013F70 File Offset: 0x00012170
		public int GetAgeSeconds()
		{
			return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - (int)this.m_vMessageTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00013FC0 File Offset: 0x000121C0
		public long GetHomeId()
		{
			return this.m_vHomeId;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00013FC8 File Offset: 0x000121C8
		public int GetId()
		{
			return this.m_vId;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00013FD0 File Offset: 0x000121D0
		public long GetSenderId()
		{
			return this.m_vSenderId;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00013FD8 File Offset: 0x000121D8
		public int GetSenderLeagueId()
		{
			return this.m_vSenderLeagueId;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00013FE0 File Offset: 0x000121E0
		public int GetSenderLevel()
		{
			return this.m_vSenderLevel;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00013FE8 File Offset: 0x000121E8
		public string GetSenderName()
		{
			return this.m_vSenderName;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00013FF0 File Offset: 0x000121F0
		public int GetSenderRole()
		{
			return this.m_vSenderRole;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00013FF8 File Offset: 0x000121F8
		public virtual int GetStreamEntryType()
		{
			return this.m_vType;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00014000 File Offset: 0x00012200
		public virtual void Load(JObject jsonObject)
		{
			this.m_vType = jsonObject["type"].ToObject<int>();
			this.m_vId = jsonObject["id"].ToObject<int>();
			this.m_vSenderId = jsonObject["sender_id"].ToObject<long>();
			this.m_vHomeId = jsonObject["home_id"].ToObject<long>();
			this.m_vSenderLevel = jsonObject["sender_level"].ToObject<int>();
			this.m_vSenderName = jsonObject["sender_name"].ToObject<string>();
			this.m_vSenderLeagueId = jsonObject["sender_leagueId"].ToObject<int>();
			this.m_vSenderRole = jsonObject["sender_role"].ToObject<int>();
			this.m_vMessageTime = jsonObject["message_time"].ToObject<DateTime>();
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000140D4 File Offset: 0x000122D4
		public virtual JObject Save(JObject jsonObject)
		{
			jsonObject.Add("type", this.GetStreamEntryType());
			jsonObject.Add("id", this.m_vId);
			jsonObject.Add("sender_id", this.m_vSenderId);
			jsonObject.Add("home_id", this.m_vHomeId);
			jsonObject.Add("sender_level", this.m_vSenderLevel);
			jsonObject.Add("sender_name", this.m_vSenderName);
			jsonObject.Add("sender_leagueId", this.m_vSenderLeagueId);
			jsonObject.Add("sender_role", this.m_vSenderRole);
			jsonObject.Add("message_time", this.m_vMessageTime);
			return jsonObject;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000141A8 File Offset: 0x000123A8
		public void SetAvatar(ClientAvatar avatar)
		{
			this.m_vSenderId = avatar.GetId();
			this.m_vHomeId = avatar.GetId();
			this.m_vSenderName = avatar.GetAvatarName();
			this.m_vSenderLevel = avatar.GetAvatarLevel();
			this.m_vSenderRole = avatar.GetAllianceRole();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000141E6 File Offset: 0x000123E6
		public void SetHomeId(long id)
		{
			this.m_vHomeId = id;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000141EF File Offset: 0x000123EF
		public void SetId(int id)
		{
			this.m_vId = id;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000141F8 File Offset: 0x000123F8
		public void SetSenderId(long id)
		{
			this.m_vSenderId = id;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00014201 File Offset: 0x00012401
		public void SetSenderLeagueId(int leagueId)
		{
			this.m_vSenderLeagueId = leagueId;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001420A File Offset: 0x0001240A
		public void SetSenderLevel(int level)
		{
			this.m_vSenderLevel = level;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00014213 File Offset: 0x00012413
		public void SetSenderName(string name)
		{
			this.m_vSenderName = name;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001421C File Offset: 0x0001241C
		public void SetSenderRole(int role)
		{
			this.m_vSenderRole = role;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00014225 File Offset: 0x00012425
		public void SetType(int type)
		{
			this.m_vType = type;
		}

		// Token: 0x040002B4 RID: 692
		private long m_vHomeId;

		// Token: 0x040002B5 RID: 693
		private int m_vId;

		// Token: 0x040002B6 RID: 694
		private DateTime m_vMessageTime;

		// Token: 0x040002B7 RID: 695
		private long m_vSenderId;

		// Token: 0x040002B8 RID: 696
		private int m_vSenderLeagueId;

		// Token: 0x040002B9 RID: 697
		private int m_vSenderLevel;

		// Token: 0x040002BA RID: 698
		private string m_vSenderName;

		// Token: 0x040002BB RID: 699
		private int m_vSenderRole;

		// Token: 0x040002BC RID: 700
		private int m_vType = -1;
	}
}

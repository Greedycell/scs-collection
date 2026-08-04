using System;
using System.Collections.Generic;
using CRS.Helpers;
using CRS.PacketProcessing;
using Newtonsoft.Json.Linq;

namespace CRS.Logic.StreamEntry
{
	// Token: 0x020000D8 RID: 216
	internal class StreamEntry
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x0001E1C4 File Offset: 0x0001C3C4
		public StreamEntry()
		{
			this.m_vMessageTime = DateTime.UtcNow;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001E1E0 File Offset: 0x0001C3E0
		public virtual byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.Add(0);
			list.AddRange(Message.AddVInt(this.m_vSenderId));
			list.Add(0);
			list.AddRange(Message.AddVInt(this.m_vSenderId));
			list.AddString(this.m_vSenderName);
			list.Add(8);
			list.Add((byte)this.m_vSenderRole);
			list.Add(30);
			list.Add(0);
			return list.ToArray();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001E258 File Offset: 0x0001C458
		public int GetAgeSeconds()
		{
			return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - (int)this.m_vMessageTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001E2A8 File Offset: 0x0001C4A8
		public long GetHomeId()
		{
			return this.m_vHomeId;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001E2B0 File Offset: 0x0001C4B0
		public int GetId()
		{
			return this.m_vId;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001E2B8 File Offset: 0x0001C4B8
		public long GetSenderId()
		{
			return this.m_vSenderId;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001E2C0 File Offset: 0x0001C4C0
		public int GetSenderLeagueId()
		{
			return this.m_vSenderLeagueId;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		public int GetSenderLevel()
		{
			return this.m_vSenderLevel;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001E2D0 File Offset: 0x0001C4D0
		public string GetSenderName()
		{
			return this.m_vSenderName;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001E2D8 File Offset: 0x0001C4D8
		public int GetSenderRole()
		{
			return this.m_vSenderRole;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001E2E0 File Offset: 0x0001C4E0
		public virtual int GetStreamEntryType()
		{
			return this.m_vType;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001E2E8 File Offset: 0x0001C4E8
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

		// Token: 0x0600054D RID: 1357 RVA: 0x0001E3BC File Offset: 0x0001C5BC
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

		// Token: 0x0600054E RID: 1358 RVA: 0x0001E490 File Offset: 0x0001C690
		public void SetAvatar(ClientAvatar avatar)
		{
			this.m_vSenderId = avatar.GetId();
			this.m_vHomeId = avatar.GetId();
			this.m_vSenderName = avatar.GetAvatarName();
			this.m_vSenderLevel = avatar.GetAvatarLevel();
			this.m_vSenderRole = avatar.GetAllianceRole();
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001E4CE File Offset: 0x0001C6CE
		public void SetHomeId(long id)
		{
			this.m_vHomeId = id;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001E4D7 File Offset: 0x0001C6D7
		public void SetId(int id)
		{
			this.m_vId = id;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
		public void SetSenderId(long id)
		{
			this.m_vSenderId = id;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001E4E9 File Offset: 0x0001C6E9
		public void SetSenderLeagueId(int leagueId)
		{
			this.m_vSenderLeagueId = leagueId;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001E4F2 File Offset: 0x0001C6F2
		public void SetSenderLevel(int level)
		{
			this.m_vSenderLevel = level;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001E4FB File Offset: 0x0001C6FB
		public void SetSenderName(string name)
		{
			this.m_vSenderName = name;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001E504 File Offset: 0x0001C704
		public void SetSenderRole(int role)
		{
			this.m_vSenderRole = role;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001E50D File Offset: 0x0001C70D
		public void SetType(int type)
		{
			this.m_vType = type;
		}

		// Token: 0x040003B7 RID: 951
		private long m_vHomeId;

		// Token: 0x040003B8 RID: 952
		private int m_vId;

		// Token: 0x040003B9 RID: 953
		private DateTime m_vMessageTime;

		// Token: 0x040003BA RID: 954
		private long m_vSenderId;

		// Token: 0x040003BB RID: 955
		private int m_vSenderLeagueId;

		// Token: 0x040003BC RID: 956
		private int m_vSenderLevel;

		// Token: 0x040003BD RID: 957
		private string m_vSenderName;

		// Token: 0x040003BE RID: 958
		private int m_vSenderRole;

		// Token: 0x040003BF RID: 959
		private int m_vType = -1;
	}
}

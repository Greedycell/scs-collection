using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x0200009C RID: 156
	internal class AvatarStreamEntry
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x00013BD0 File Offset: 0x00011DD0
		public AvatarStreamEntry()
		{
			this.m_vCreationTime = DateTime.UtcNow;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00013BE4 File Offset: 0x00011DE4
		public virtual byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(this.GetStreamEntryType());
			list.AddInt32(0);
			list.AddInt32(this.m_vId);
			list.AddInt64(this.m_vSenderId);
			list.AddString(this.m_vSenderName);
			list.AddInt32(this.m_vSenderLevel);
			list.AddInt32(this.m_vSenderLeagueId);
			return list.ToArray();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00013C4C File Offset: 0x00011E4C
		public int GetAgeSeconds()
		{
			return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - (int)this.m_vCreationTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00013C9C File Offset: 0x00011E9C
		public int GetId()
		{
			return this.m_vId;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00013CA4 File Offset: 0x00011EA4
		public long GetSenderAvatarId()
		{
			return this.m_vSenderId;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00013CAC File Offset: 0x00011EAC
		public int GetSenderLevel()
		{
			return this.m_vSenderLevel;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00013CB4 File Offset: 0x00011EB4
		public string GetSenderName()
		{
			return this.m_vSenderName;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00013CBC File Offset: 0x00011EBC
		public virtual int GetStreamEntryType()
		{
			return -1;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00013CBF File Offset: 0x00011EBF
		public byte IsNew()
		{
			return this.m_vIsNew;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00013CC7 File Offset: 0x00011EC7
		public void SetAvatar(ClientAvatar avatar)
		{
			this.m_vSenderId = avatar.GetId();
			this.m_vSenderName = avatar.GetAvatarName();
			this.m_vSenderLevel = avatar.GetAvatarLevel();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00013CED File Offset: 0x00011EED
		public void SetId(int id)
		{
			this.m_vId = id;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00013CF6 File Offset: 0x00011EF6
		public void SetIsNew(byte isNew)
		{
			this.m_vIsNew = isNew;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00013CFF File Offset: 0x00011EFF
		public void SetSenderAvatarId(long id)
		{
			this.m_vSenderId = id;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00013D08 File Offset: 0x00011F08
		public void SetSenderLeagueId(int id)
		{
			this.m_vSenderLeagueId = id;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00013D11 File Offset: 0x00011F11
		public void SetSenderLevel(int level)
		{
			this.m_vSenderLevel = level;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00013D1A File Offset: 0x00011F1A
		public void SetSenderName(string name)
		{
			this.m_vSenderName = name;
		}

		// Token: 0x040002A5 RID: 677
		private DateTime m_vCreationTime;

		// Token: 0x040002A6 RID: 678
		private int m_vId;

		// Token: 0x040002A7 RID: 679
		private byte m_vIsNew;

		// Token: 0x040002A8 RID: 680
		private long m_vSenderId;

		// Token: 0x040002A9 RID: 681
		private int m_vSenderLeagueId;

		// Token: 0x040002AA RID: 682
		private int m_vSenderLevel;

		// Token: 0x040002AB RID: 683
		private string m_vSenderName;
	}
}

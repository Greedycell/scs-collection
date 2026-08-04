using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UCS.Core;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x02000099 RID: 153
	internal class AllianceMemberEntry
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x00013818 File Offset: 0x00011A18
		public AllianceMemberEntry(long avatarId)
		{
			this.m_vAvatarId = avatarId;
			this.m_vIsNewMember = 0;
			this.m_vOrder = 1;
			this.m_vPreviousOrder = 1;
			this.m_vRole = 1;
			this.m_vDonatedTroops = 200;
			this.m_vReceivedTroops = 100;
			this.m_vWarCooldown = 0;
			this.m_vWarOptInStatus = 1;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00013888 File Offset: 0x00011A88
		public static void Decode(byte[] avatarData)
		{
			using (new BinaryReader(new MemoryStream(avatarData)))
			{
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000138C0 File Offset: 0x00011AC0
		public byte[] Encode()
		{
			List<byte> list = new List<byte>();
			Level player = ResourcesManager.GetPlayer(this.m_vAvatarId, false);
			list.AddInt64(this.m_vAvatarId);
			list.AddString(player.GetPlayerAvatar().GetAvatarName());
			list.AddInt32(this.m_vRole);
			list.AddInt32(player.GetPlayerAvatar().GetAvatarLevel());
			list.AddInt32(player.GetPlayerAvatar().GetArenaId());
			list.AddInt32(player.GetPlayerAvatar().GetScore());
			list.AddInt32(this.m_vDonatedTroops);
			list.AddInt32(this.m_vReceivedTroops);
			list.AddInt32(this.m_vOrder);
			list.AddInt32(this.m_vPreviousOrder);
			list.Add(this.m_vIsNewMember);
			list.AddInt32(this.m_vWarCooldown);
			list.AddInt32(this.m_vWarOptInStatus);
			list.Add(1);
			list.AddInt64(this.m_vAvatarId);
			return list.ToArray();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000139A7 File Offset: 0x00011BA7
		public long GetAvatarId()
		{
			return this.m_vAvatarId;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000139AF File Offset: 0x00011BAF
		public static int GetDonations()
		{
			return 150;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000139B6 File Offset: 0x00011BB6
		public int GetOrder()
		{
			return this.m_vOrder;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000139BE File Offset: 0x00011BBE
		public int GetPreviousOrder()
		{
			return this.m_vPreviousOrder;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000139C6 File Offset: 0x00011BC6
		public int GetRole()
		{
			return this.m_vRole;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000139D0 File Offset: 0x00011BD0
		public bool HasLowerRoleThan(int role)
		{
			bool flag = true;
			if (role < this.m_vRoleTable.Length && this.m_vRole < this.m_vRoleTable.Length && this.m_vRoleTable[this.m_vRole] >= this.m_vRoleTable[role])
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00013A14 File Offset: 0x00011C14
		public byte IsNewMember()
		{
			return this.m_vIsNewMember;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013A1C File Offset: 0x00011C1C
		public void Load(JObject jsonObject)
		{
			this.m_vAvatarId = jsonObject["avatar_id"].ToObject<long>();
			this.m_vRole = jsonObject["role"].ToObject<int>();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00013A4A File Offset: 0x00011C4A
		public JObject Save(JObject jsonObject)
		{
			jsonObject.Add("avatar_id", this.m_vAvatarId);
			jsonObject.Add("role", this.m_vRole);
			return jsonObject;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013A79 File Offset: 0x00011C79
		public void SetAvatarId(long id)
		{
			this.m_vAvatarId = id;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00013A82 File Offset: 0x00011C82
		public void SetOrder(int order)
		{
			this.m_vOrder = order;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00013A8B File Offset: 0x00011C8B
		public void SetPreviousOrder(int order)
		{
			this.m_vPreviousOrder = order;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00013A94 File Offset: 0x00011C94
		public void SetRole(int role)
		{
			this.m_vRole = role;
		}

		// Token: 0x04000291 RID: 657
		private readonly int m_vDonatedTroops;

		// Token: 0x04000292 RID: 658
		private readonly byte m_vIsNewMember;

		// Token: 0x04000293 RID: 659
		private readonly int m_vReceivedTroops;

		// Token: 0x04000294 RID: 660
		private readonly int[] m_vRoleTable = new int[] { 1, 1, 4, 2, 3 };

		// Token: 0x04000295 RID: 661
		private readonly int m_vWarCooldown;

		// Token: 0x04000296 RID: 662
		private readonly int m_vWarOptInStatus;

		// Token: 0x04000297 RID: 663
		private long m_vAvatarId;

		// Token: 0x04000298 RID: 664
		private int m_vOrder;

		// Token: 0x04000299 RID: 665
		private int m_vPreviousOrder;

		// Token: 0x0400029A RID: 666
		private int m_vRole;
	}
}

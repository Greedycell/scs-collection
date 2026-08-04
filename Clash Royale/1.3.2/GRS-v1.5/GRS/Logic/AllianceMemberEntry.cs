using System;
using System.Collections.Generic;
using System.IO;
using CRS.Core;
using CRS.Helpers;
using CRS.PacketProcessing;
using Newtonsoft.Json.Linq;

namespace CRS.Logic
{
	// Token: 0x020000CB RID: 203
	internal class AllianceMemberEntry
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
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

		// Token: 0x06000491 RID: 1169 RVA: 0x0001C258 File Offset: 0x0001A458
		public static void Decode(byte[] avatarData)
		{
			using (new BinaryReader(new MemoryStream(avatarData)))
			{
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001C290 File Offset: 0x0001A490
		public byte[] Encode()
		{
			List<byte> list = new List<byte>();
			Level player = ResourcesManager.GetPlayer(this.m_vAvatarId, false);
			list.AddInt64(this.m_vAvatarId);
			list.AddString(player.GetPlayerAvatar().GetAvatarName());
			list.Add(54);
			list.Add(8);
			list.Add((byte)this.m_vRole);
			list.Add((byte)player.GetPlayerAvatar().GetAvatarLevel());
			list.AddRange(Message.AddVInt(player.GetPlayerAvatar().GetScore()));
			list.AddRange(Message.AddVInt(0));
			return list.ToArray();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001C321 File Offset: 0x0001A521
		public long GetAvatarId()
		{
			return this.m_vAvatarId;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001C329 File Offset: 0x0001A529
		public static int GetDonations()
		{
			return 150;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001C330 File Offset: 0x0001A530
		public int GetOrder()
		{
			return this.m_vOrder;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001C338 File Offset: 0x0001A538
		public int GetPreviousOrder()
		{
			return this.m_vPreviousOrder;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001C340 File Offset: 0x0001A540
		public int GetRole()
		{
			return this.m_vRole;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001C348 File Offset: 0x0001A548
		public bool HasLowerRoleThan(int role)
		{
			bool flag = true;
			if (role < this.m_vRoleTable.Length && this.m_vRole < this.m_vRoleTable.Length && this.m_vRoleTable[this.m_vRole] >= this.m_vRoleTable[role])
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001C38C File Offset: 0x0001A58C
		public byte IsNewMember()
		{
			return this.m_vIsNewMember;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001C394 File Offset: 0x0001A594
		public void Load(JObject jsonObject)
		{
			this.m_vAvatarId = jsonObject["avatar_id"].ToObject<long>();
			this.m_vRole = jsonObject["role"].ToObject<int>();
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001C3C2 File Offset: 0x0001A5C2
		public JObject Save(JObject jsonObject)
		{
			jsonObject.Add("avatar_id", this.m_vAvatarId);
			jsonObject.Add("role", this.m_vRole);
			return jsonObject;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001C3F1 File Offset: 0x0001A5F1
		public void SetAvatarId(long id)
		{
			this.m_vAvatarId = id;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001C3FA File Offset: 0x0001A5FA
		public void SetOrder(int order)
		{
			this.m_vOrder = order;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001C403 File Offset: 0x0001A603
		public void SetPreviousOrder(int order)
		{
			this.m_vPreviousOrder = order;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001C40C File Offset: 0x0001A60C
		public void SetRole(int role)
		{
			this.m_vRole = role;
		}

		// Token: 0x04000351 RID: 849
		private readonly int m_vDonatedTroops;

		// Token: 0x04000352 RID: 850
		private readonly byte m_vIsNewMember;

		// Token: 0x04000353 RID: 851
		private readonly int m_vReceivedTroops;

		// Token: 0x04000354 RID: 852
		private readonly int[] m_vRoleTable = new int[] { 1, 1, 4, 2, 3 };

		// Token: 0x04000355 RID: 853
		private readonly int m_vWarCooldown;

		// Token: 0x04000356 RID: 854
		private readonly int m_vWarOptInStatus;

		// Token: 0x04000357 RID: 855
		private long m_vAvatarId;

		// Token: 0x04000358 RID: 856
		private int m_vOrder;

		// Token: 0x04000359 RID: 857
		private int m_vPreviousOrder;

		// Token: 0x0400035A RID: 858
		private int m_vRole;
	}
}

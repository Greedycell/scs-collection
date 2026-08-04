using System;
using System.Collections.Generic;
using CRS.Core;
using CRS.Helpers;
using CRS.PacketProcessing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CRS.Logic
{
	// Token: 0x020000CF RID: 207
	internal class ClientAvatar : Avatar
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x0001C690 File Offset: 0x0001A890
		public ClientAvatar()
		{
			this.Achievements = new List<DataSlot>();
			this.AchievementsUnlocked = new List<DataSlot>();
			this.AllianceUnits = new List<DataSlot>();
			this.NpcStars = new List<DataSlot>();
			this.NpcLootedGold = new List<DataSlot>();
			this.NpcLootedElixir = new List<DataSlot>();
			this.Chests = new List<ClientAvatar.CrownChests>();
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001C724 File Offset: 0x0001A924
		public ClientAvatar(long id, string token)
			: this()
		{
			Random random = new Random();
			this.LastUpdate = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
			this.Login = id.ToString() + (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
			this.m_vId = id;
			this.m_vToken = token;
			this.m_vCurrentHomeId = id;
			this.m_vScore = random.Next(3000, 5000);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0001C7C9 File Offset: 0x0001A9C9
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0001C7D1 File Offset: 0x0001A9D1
		public List<DataSlot> Achievements { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0001C7DA File Offset: 0x0001A9DA
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0001C7E2 File Offset: 0x0001A9E2
		public List<DataSlot> AchievementsUnlocked { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001C7EB File Offset: 0x0001A9EB
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0001C7F3 File Offset: 0x0001A9F3
		public List<DataSlot> AllianceUnits { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001C7FC File Offset: 0x0001A9FC
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0001C804 File Offset: 0x0001AA04
		public List<ClientAvatar.CrownChests> Chests { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001C80D File Offset: 0x0001AA0D
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0001C815 File Offset: 0x0001AA15
		public int LastUpdate { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0001C81E File Offset: 0x0001AA1E
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x0001C826 File Offset: 0x0001AA26
		public string Login { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0001C82F File Offset: 0x0001AA2F
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x0001C837 File Offset: 0x0001AA37
		public List<DataSlot> NpcLootedElixir { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0001C840 File Offset: 0x0001AA40
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x0001C848 File Offset: 0x0001AA48
		public List<DataSlot> NpcLootedGold { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001C851 File Offset: 0x0001AA51
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x0001C859 File Offset: 0x0001AA59
		public List<DataSlot> NpcStars { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001C862 File Offset: 0x0001AA62
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0001C86A File Offset: 0x0001AA6A
		public uint Region { get; set; }

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001C873 File Offset: 0x0001AA73
		public byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt64(this.GetId());
			list.AddRange(this.EncodeProfile());
			return list.ToArray();
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001C898 File Offset: 0x0001AA98
		public byte[] EncodeProfile()
		{
			List<byte> list = new List<byte>();
			list.AddRange(new byte[]
			{
				19, 1, 176, 206, 210, 1, 128, 248, 210, 1,
				181, 158, 227, 244, 10, 0, 3, 8, 128, 234,
				229, 24, 129, 234, 229, 24, 141, 234, 229, 24,
				129, 252, 217, 26, 128, 252, 217, 26, 131, 234,
				229, 24, 142, 234, 229, 24, 130, 234, 229, 24,
				8, 128, 234, 229, 24, 129, 234, 229, 24, 141,
				234, 229, 24, 129, 252, 217, 26, 128, 252, 217,
				26, 131, 234, 229, 24, 0, 0, 8, 128, 234,
				229, 24, 129, 234, 229, 24, 141, 234, 229, 24,
				129, 252, 217, 26, 128, 252, 217, 26, 131, 234,
				229, 24, 0, 0, byte.MaxValue
			});
			list.AddRange(new byte[]
			{
				26, 0, 0, 0, 1, 0, 11, 0, 26, 1,
				0, 0, 1, 0, 11, 0, 26, 2, 0, 0,
				1, 0, 11, 0, 26, 3, 0, 0, 1, 0,
				11, 0, 26, 4, 0, 0, 1, 0, 11, 0,
				26, 5, 0, 0, 1, 0, 11, 0, 26, 6,
				0, 0, 1, 0, 11, 0, 26, 7, 0, 0,
				1, 0, 11, 0, 46, 26, 8, 0, 0, 1,
				0, 11, 0, 26, 9, 0, 0, 1, 0, 11,
				0, 26, 10, 0, 0, 1, 0, 11, 0, 26,
				11, 0, 0, 1, 0, 11, 0, 26, 12, 0,
				0, 1, 0, 11, 0, 26, 13, 0, 0, 1,
				0, 11, 0, 26, 14, 0, 0, 1, 0, 11,
				0, 26, 15, 0, 0, 1, 0, 11, 0, 26,
				16, 0, 0, 1, 0, 11, 0, 26, 17, 0,
				0, 1, 0, 11, 0, 26, 18, 0, 0, 1,
				0, 11, 0, 26, 19, 0, 0, 1, 0, 11,
				0, 26, 20, 0, 0, 1, 0, 11, 0, 26,
				21, 0, 0, 1, 0, 11, 0, 26, 22, 0,
				0, 1, 0, 11, 0, 26, 23, 0, 0, 1,
				0, 11, 0, 26, 24, 0, 0, 1, 0, 11,
				0, 26, 25, 0, 0, 1, 0, 11, 0, 26,
				26, 0, 0, 1, 0, 11, 0, 26, 27, 0,
				0, 1, 0, 11, 0, 26, 28, 0, 0, 1,
				0, 11, 0, 26, 29, 0, 0, 1, 0, 11,
				0, 26, 31, 0, 0, 1, 0, 11, 0, 26,
				32, 0, 0, 1, 0, 11, 0, 26, 33, 0,
				0, 1, 0, 11, 0, 27, 0, 0, 0, 1,
				0, 11, 0, 27, 1, 0, 0, 1, 0, 11,
				0, 27, 2, 0, 0, 1, 0, 11, 0, 27,
				3, 0, 0, 1, 0, 11, 0, 27, 4, 0,
				0, 1, 0, 11, 0, 27, 5, 0, 0, 1,
				0, 11, 0, 27, 6, 0, 0, 1, 0, 11,
				0, 27, 7, 0, 0, 1, 0, 11, 0, 27,
				8, 0, 0, 1, 0, 11, 0, 27, 9, 0,
				0, 1, 0, 11, 0, 27, 10, 0, 0, 1,
				0, 11, 0, 28, 0, 0, 0, 1, 0, 11,
				0, 28, 1, 0, 0, 1, 0, 11, 0, 28,
				2, 0, 0, 1, 0, 11, 0, 28, 3, 0,
				0, 1, 0, 11, 0, 28, 4, 0, 0, 1,
				0, 11, 0, 28, 5, 0, 0, 1, 0, 11,
				0, 28, 6, 0, 0, 1, 0, 11, 0, 28,
				7, 0, 0, 1, 0, 11, 0, 28, 8, 0,
				0, 1, 0, 11, 0, 28, 9, 0, 0, 1,
				0, 11, 0
			});
			list.Add(0);
			list.Add(0);
			int num = 4;
			list.Add((byte)num);
			for (int i = 0; i < num; i++)
			{
				list.Add(1);
				list.Add(19);
				list.Add(61);
				list.Add(0);
				list.Add(187);
				list.Add((byte)i);
				list.Add(1);
				list.Add(0);
			}
			list.AddRange(new byte[]
			{
				128, 148, 35, 128, 148, 35, 179, 237, 249, 243,
				10, 0, 0, 127, 1, 19, 7, 1, 2, 0,
				127, 0, 0, 0, 0, 0, 156, 224, 209, 1,
				176, 246, 210, 1, 142, 203, 130, 244, 10, 156,
				224, 209, 1, 176, 246, 210, 1, 142, 203, 130,
				244, 10, 0, 0, 0, 127, 1, 0, 0, 0,
				0, 0, 0, 0, 2
			});
			list.Add((byte)this.GetAvatarLevel());
			list.Add(54);
			list.Add((byte)this.GetArenaId());
			list.AddRange(new byte[]
			{
				183, 244, 153, 137, 12, 0, 7, 188, 226, 14,
				188, 226, 14, 159, 221, 241, 243, 10
			});
			list.Add(6);
			list.AddRange(new byte[]
			{
				26, 31, 0, 27, 10, 0, 26, 25, 0, 26,
				29, 0, 26, 32, 0, 26, 33, 0
			});
			list.AddRange(new byte[]
			{
				0, 0, 127, 0, 0, 127, 0, 0, 127, 9,
				12, 188, 25, 2, 130, 26, 5, 0, 0, 0,
				0, 0, 1, 26, 24, 1, 8
			});
			list.Add(0);
			list.AddRange(Message.AddVInt(this.GetId()));
			list.Add(0);
			list.AddRange(Message.AddVInt(this.GetId()));
			list.Add(0);
			list.AddRange(Message.AddVInt(this.GetId()));
			list.AddString(this.GetAvatarName());
			list.AddRange(new byte[] { 0, 54 });
			list.Add((byte)this.GetArenaId());
			list.AddRange(Message.AddVInt(this.GetScore()));
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.AddRange(Message.AddVInt(this.GetAvatarLevel()));
			list.AddRange(Message.AddVInt(this.GetScore()));
			list.AddRange(Message.AddVInt(this.GetScore()));
			list.Add(6);
			list.Add(6);
			list.Add(5);
			list.Add(1);
			list.AddRange(Message.AddVInt(500000));
			list.Add(5);
			list.Add(2);
			list.AddRange(Message.AddVInt(500000));
			list.Add(5);
			list.Add(3);
			list.Add((byte)num);
			list.Add(5);
			list.Add(4);
			list.Add(7);
			list.Add(5);
			list.Add(5);
			list.AddRange(Message.AddVInt(500000));
			list.Add(5);
			list.Add(13);
			list.AddRange(Message.AddVInt(500000));
			list.AddRange(Helpers.Helpers.HexaToBytes("00033C07063C08063C090600"));
			list.Add(7);
			list.Add(5);
			list.Add(6);
			list.AddRange(Message.AddVInt(5000));
			list.Add(5);
			list.Add(7);
			list.AddRange(Message.AddVInt(5000));
			list.Add(5);
			list.Add(8);
			list.Add(53);
			list.Add(5);
			list.Add(9);
			list.Add(26);
			list.Add(5);
			list.Add(10);
			list.AddRange(Message.AddVInt(5000));
			list.Add(5);
			list.Add(11);
			list.AddRange(Message.AddVInt(5000));
			list.Add(5);
			list.Add(12);
			list.AddRange(Message.AddVInt(5000));
			list.Add(52);
			list.AddRange(new byte[]
			{
				26, 0, 0, 26, 1, 0, 26, 2, 0, 26,
				3, 0, 26, 4, 0, 26, 5, 0, 26, 6,
				0, 26, 7, 0, 26, 8, 0, 26, 9, 0,
				26, 10, 0, 26, 11, 0, 26, 12, 0, 26,
				13, 0, 26, 14, 0, 26, 15, 0, 26, 16,
				0, 26, 17, 0, 26, 18, 0, 26, 19, 0,
				26, 20, 0, 26, 21, 0, 26, 22, 0, 26,
				23, 0, 26, 24, 0, 26, 25, 0, 26, 26,
				0, 26, 27, 0, 26, 28, 0, 26, 29, 0,
				26, 31, 0, 26, 32, 0, 27, 0, 0, 27,
				1, 0, 27, 2, 0, 27, 3, 0, 27, 4,
				0, 27, 5, 0, 27, 6, 0, 27, 7, 0,
				27, 8, 0, 27, 9, 0, 27, 10, 0, 28,
				0, 0, 28, 1, 0, 28, 2, 0, 28, 3,
				0, 28, 4, 0, 28, 5, 0, 28, 6, 0,
				28, 7, 0, 28, 8, 0
			});
			list.AddRange(Message.AddVInt(this.GetDiamonds()));
			list.Add(10);
			list.AddRange(Message.AddVInt(this.GetExperience()));
			list.AddRange(Message.AddVInt(this.GetAvatarLevel()));
			list.AddRange(Helpers.Helpers.HexaToBytes("9885"));
			if (this.GetAllianceId() > 0L)
			{
				Alliance alliance = ObjectManager.GetAlliance(this.GetAllianceId());
				list.Add(1);
				list.Add(9);
				list.Add(0);
				list.AddRange(Message.AddVInt(alliance.GetAllianceId()));
				list.AddString(alliance.GetAllianceName());
				list.Add(16);
				list.AddRange(Message.AddVInt(alliance.GetAllianceBadgeData()));
				list.AddRange(Helpers.Helpers.HexaToBytes("91189F0BAC0A7F1601F188839406"));
			}
			else
			{
				list.Add(0);
				list.Add(this.GetNameSet());
				list.AddRange(Helpers.Helpers.HexaToBytes("000000000600E1F1FBF302"));
			}
			return list.ToArray();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001CD3D File Offset: 0x0001AF3D
		public void AddDiamonds(int diamondCount)
		{
			this.m_vCurrentGems += diamondCount;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001CD4D File Offset: 0x0001AF4D
		public List<ClientAvatar.CrownChests> GetChests()
		{
			return this.Chests;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001CD55 File Offset: 0x0001AF55
		public long GetAllianceId()
		{
			return this.m_vAllianceId;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001CD5D File Offset: 0x0001AF5D
		public AllianceMemberEntry GetAllianceMemberEntry()
		{
			Alliance alliance = ObjectManager.GetAlliance(this.m_vAllianceId);
			if (alliance == null)
			{
				return null;
			}
			return alliance.GetAllianceMember(this.m_vId);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001CD7C File Offset: 0x0001AF7C
		public int GetAllianceRole()
		{
			AllianceMemberEntry allianceMemberEntry = this.GetAllianceMemberEntry();
			if (allianceMemberEntry != null)
			{
				return allianceMemberEntry.GetRole();
			}
			return -1;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001CD9B File Offset: 0x0001AF9B
		public int GetAvatarLevel()
		{
			return this.m_vAvatarLevel;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001CDA3 File Offset: 0x0001AFA3
		public string GetAvatarName()
		{
			return this.m_vAvatarName;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001CDAB File Offset: 0x0001AFAB
		public int GetExperience()
		{
			return this.m_vExperience;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001CDB3 File Offset: 0x0001AFB3
		public long GetCurrentHomeId()
		{
			return this.m_vCurrentHomeId;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001CDBB File Offset: 0x0001AFBB
		public int GetDiamonds()
		{
			return this.m_vCurrentGems;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001CDC3 File Offset: 0x0001AFC3
		public long GetId()
		{
			return this.m_vId;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001CDCB File Offset: 0x0001AFCB
		public int GetArenaId()
		{
			return this.m_vArenaId;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001CDD3 File Offset: 0x0001AFD3
		public int GetScore()
		{
			return this.m_vScore;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001CDDC File Offset: 0x0001AFDC
		public int GetSecondsFromLastUpdate()
		{
			return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - this.LastUpdate;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001CE12 File Offset: 0x0001B012
		public string GetUserToken()
		{
			return this.m_vToken;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001CE1A File Offset: 0x0001B01A
		public bool HasEnoughDiamonds(int diamondCount)
		{
			return this.m_vCurrentGems >= diamondCount;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001CE28 File Offset: 0x0001B028
		public void LoadFromJSON(string jsonString)
		{
			JObject jobject = JObject.Parse(jsonString);
			this.m_vId = jobject["avatar_id"].ToObject<long>();
			this.m_vToken = jobject["token"].ToObject<string>();
			this.m_vCurrentHomeId = jobject["current_home_id"].ToObject<long>();
			this.m_vAllianceId = jobject["alliance_id"].ToObject<long>();
			this.m_vAvatarName = jobject["avatar_name"].ToObject<string>();
			this.m_vAvatarLevel = jobject["avatar_level"].ToObject<int>();
			this.m_vExperience = jobject["experience"].ToObject<int>();
			this.m_vCurrentGems = jobject["current_gems"].ToObject<int>();
			this.SetScore(jobject["score"].ToObject<int>());
			this.m_vNameChangingLeft = jobject["nameChangesLeft"].ToObject<byte>();
			this.m_vNameChosenByUser = jobject["nameChosenByUser"].ToObject<byte>();
			foreach (JToken jtoken in ((JArray)jobject["resources"]))
			{
				JObject jobject2 = (JObject)jtoken;
				DataSlot dataSlot = new DataSlot(null, 0);
				dataSlot.Load(jobject2);
				base.GetResources().Add(dataSlot);
			}
			foreach (JToken jtoken2 in ((JArray)jobject["decks"]))
			{
				JObject jobject3 = (JObject)jtoken2;
				DataSlot dataSlot2 = new DataSlot(null, 0);
				dataSlot2.Load(jobject3);
				this.m_vUnitCount.Add(dataSlot2);
			}
			this.m_vTutorielStep = jobject["tutorial_step"].ToObject<uint>();
			foreach (JToken jtoken3 in ((JArray)jobject["achievements_progress"]))
			{
				JObject jobject4 = (JObject)jtoken3;
				DataSlot dataSlot3 = new DataSlot(null, 0);
				dataSlot3.Load(jobject4);
				this.Achievements.Add(dataSlot3);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001D070 File Offset: 0x0001B270
		public void SetAllianceRole(int a)
		{
			AllianceMemberEntry allianceMemberEntry = this.GetAllianceMemberEntry();
			if (allianceMemberEntry == null)
			{
				return;
			}
			allianceMemberEntry.SetRole(a);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001D084 File Offset: 0x0001B284
		public string SaveToJSON()
		{
			JObject jobject = new JObject();
			jobject.Add("avatar_id", this.m_vId);
			jobject.Add("token", this.m_vToken);
			jobject.Add("current_home_id", this.m_vCurrentHomeId);
			jobject.Add("alliance_id", this.m_vAllianceId);
			jobject.Add("avatar_name", this.m_vAvatarName);
			jobject.Add("avatar_level", this.m_vAvatarLevel);
			jobject.Add("experience", this.m_vExperience);
			jobject.Add("current_gems", this.m_vCurrentGems);
			jobject.Add("score", this.m_vScore);
			jobject.Add("nameChangesLeft", this.m_vNameChangingLeft);
			jobject.Add("nameChosenByUser", (ushort)this.m_vNameChosenByUser);
			JArray jarray = new JArray();
			foreach (DataSlot dataSlot in base.GetResources())
			{
				jarray.Add(dataSlot.Save(new JObject()));
			}
			jobject.Add("resources", jarray);
			JArray jarray2 = new JArray();
			foreach (DataSlot dataSlot2 in base.GetUnits())
			{
				jarray2.Add(dataSlot2.Save(new JObject()));
			}
			jobject.Add("decks", jarray2);
			jobject.Add("tutorial_step", this.m_vTutorielStep);
			JArray jarray3 = new JArray();
			foreach (DataSlot dataSlot3 in this.Achievements)
			{
				jarray3.Add(dataSlot3.Save(new JObject()));
			}
			jobject.Add("achievements_progress", jarray3);
			return JsonConvert.SerializeObject(jobject);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		public void SetAllianceId(long id)
		{
			this.m_vAllianceId = id;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001D2D5 File Offset: 0x0001B4D5
		public void SetDiamonds(int count)
		{
			this.m_vCurrentGems = count;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001D2DE File Offset: 0x0001B4DE
		public void SetArenaId(int id)
		{
			this.m_vArenaId = id;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001D2E7 File Offset: 0x0001B4E7
		public void SetScore(int newScore)
		{
			this.m_vScore = newScore;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001D2F0 File Offset: 0x0001B4F0
		public void SetName(string name)
		{
			this.m_vAvatarName = name;
			this.m_vNameChosenByUser = 1;
			this.m_vNameChangingLeft = 1;
			this.m_vTutorielStep = 13U;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001D30F File Offset: 0x0001B50F
		public byte GetNameSet()
		{
			return this.m_vNameChosenByUser;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001D317 File Offset: 0x0001B517
		public void SetToken(string token)
		{
			this.m_vToken = token;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001D320 File Offset: 0x0001B520
		public void UseDiamonds(int diamondCount)
		{
			this.m_vCurrentGems -= diamondCount;
		}

		// Token: 0x0400036C RID: 876
		private long m_vAllianceId;

		// Token: 0x0400036D RID: 877
		private int m_vAvatarLevel = 12;

		// Token: 0x0400036E RID: 878
		private string m_vAvatarName = "";

		// Token: 0x0400036F RID: 879
		private int m_vCurrentGems = 1250000;

		// Token: 0x04000370 RID: 880
		private long m_vCurrentHomeId;

		// Token: 0x04000371 RID: 881
		private int m_vExperience;

		// Token: 0x04000372 RID: 882
		private long m_vId;

		// Token: 0x04000373 RID: 883
		private int m_vLeagueId;

		// Token: 0x04000374 RID: 884
		private int m_vLoses;

		// Token: 0x04000375 RID: 885
		private byte m_vNameChangingLeft = 2;

		// Token: 0x04000376 RID: 886
		private byte m_vNameChosenByUser;

		// Token: 0x04000377 RID: 887
		private string m_vRegion;

		// Token: 0x04000378 RID: 888
		private int m_vScore;

		// Token: 0x04000379 RID: 889
		private string m_vToken;

		// Token: 0x0400037A RID: 890
		private int m_vWins;

		// Token: 0x0400037B RID: 891
		private int m_vDonated;

		// Token: 0x0400037C RID: 892
		private int m_vReceived;

		// Token: 0x0400037D RID: 893
		private uint m_vTutorielStep = 10U;

		// Token: 0x0400037E RID: 894
		private int m_vArenaId = 7;

		// Token: 0x0200010C RID: 268
		public class CrownChests
		{
			// Token: 0x170000AD RID: 173
			// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001DCD9 File Offset: 0x0001BED9
			public byte ressources
			{
				get
				{
					return 5;
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001E10F File Offset: 0x0001C30F
			public byte stars
			{
				get
				{
					return 4;
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x06000645 RID: 1605 RVA: 0x00021B7A File Offset: 0x0001FD7A
			public byte crown
			{
				get
				{
					return 16;
				}
			}
		}
	}
}

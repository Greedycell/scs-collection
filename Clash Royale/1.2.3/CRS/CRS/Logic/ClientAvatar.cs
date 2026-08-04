using System;
using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UCS.Core;
using UCS.Helpers;
using UCS.PacketProcessing;

namespace UCS.Logic
{
	// Token: 0x020000AD RID: 173
	internal class ClientAvatar : Avatar
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x00014990 File Offset: 0x00012B90
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

		// Token: 0x060004C2 RID: 1218 RVA: 0x000149F0 File Offset: 0x00012BF0
		public ClientAvatar(long id)
			: this()
		{
			Random random = new Random();
			this.LastUpdate = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
			this.Login = id.ToString() + (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
			this.m_vId = id;
			this.m_vToken = null;
			this.m_vCurrentHomeId = id;
			this.m_vnameChosenByUser = 0;
			this.m_vNameChangingLeft = 2;
			this.m_vAvatarLevel = 1;
			this.m_vAllianceId = 0L;
			this.m_vExperience = 0;
			this.EndShieldTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + (double)Convert.ToInt32(ConfigurationManager.AppSettings["startingShieldTime"]));
			this.m_vCurrentGems = Convert.ToInt32(ConfigurationManager.AppSettings["startingGems"]);
			if (ConfigurationManager.AppSettings["startingTrophies"] == "random")
			{
				this.m_vScore = random.Next(1500, 4800);
			}
			else
			{
				this.m_vScore = Convert.ToInt32(ConfigurationManager.AppSettings["startingTrophies"]);
			}
			this.TutorialStepsCount = 10U;
			this.m_vAvatarName = "";
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00014B5B File Offset: 0x00012D5B
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00014B63 File Offset: 0x00012D63
		public List<DataSlot> Achievements { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00014B6C File Offset: 0x00012D6C
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00014B74 File Offset: 0x00012D74
		public List<DataSlot> AchievementsUnlocked { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00014B7D File Offset: 0x00012D7D
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00014B85 File Offset: 0x00012D85
		public List<DataSlot> AllianceUnits { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00014B8E File Offset: 0x00012D8E
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x00014B96 File Offset: 0x00012D96
		public List<ClientAvatar.CrownChests> Chests { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00014B9F File Offset: 0x00012D9F
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00014BA7 File Offset: 0x00012DA7
		public int EndShieldTime { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00014BB0 File Offset: 0x00012DB0
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00014BB8 File Offset: 0x00012DB8
		public int LastUpdate { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00014BC1 File Offset: 0x00012DC1
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00014BC9 File Offset: 0x00012DC9
		public string Login { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00014BD2 File Offset: 0x00012DD2
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00014BDA File Offset: 0x00012DDA
		public List<DataSlot> NpcLootedElixir { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00014BE3 File Offset: 0x00012DE3
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00014BEB File Offset: 0x00012DEB
		public List<DataSlot> NpcLootedGold { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00014BF4 File Offset: 0x00012DF4
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x00014BFC File Offset: 0x00012DFC
		public List<DataSlot> NpcStars { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00014C08 File Offset: 0x00012E08
		public int RemainingShieldTime
		{
			get
			{
				int num = this.EndShieldTime - (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
				if (num <= 0)
				{
					return 0;
				}
				return num;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00014C46 File Offset: 0x00012E46
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00014C4E File Offset: 0x00012E4E
		public uint TutorialStepsCount { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00014C57 File Offset: 0x00012E57
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00014C5F File Offset: 0x00012E5F
		public uint Region { get; set; }

		// Token: 0x060004DC RID: 1244 RVA: 0x00014C68 File Offset: 0x00012E68
		public byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt64(this.GetId());
			list.AddRange(new byte[]
			{
				1, 1, 132, 165, 29, 145, 159, 42, 155, 158,
				235, 241, 10, 0, 3, 8, 128, 234, 229, 24,
				129, 234, 229, 24, 141, 234, 229, 24, 129, 252,
				217, 26, 128, 252, 217, 26, 131, 234, 229, 24,
				142, 234, 229, 24, 140, 234, 229, 24, 8, 128,
				234, 229, 24, 129, 234, 229, 24, 141, 234, 229,
				24, 129, 252, 217, 26, 128, 252, 217, 26, 131,
				234, 229, 24, 0, 0, 8, 128, 234, 229, 24,
				129, 234, 229, 24, 141, 234, 229, 24, 129, 252,
				217, 26, 128, 252, 217, 26, 131, 234, 229, 24,
				0, 0, byte.MaxValue
			});
			list.AddRange(new byte[]
			{
				26, 0, 0, 0, 0, 0, 0, 0, 26, 0,
				0, 0, 0, 0, 0, 0, 26, 0, 0, 0,
				0, 0, 0, 0, 26, 0, 0, 0, 0, 0,
				0, 0, 26, 0, 0, 0, 0, 0, 0, 0,
				26, 0, 0, 0, 0, 0, 0, 0, 26, 0,
				0, 134, 173, 155, 0, 0, 0, 0, 0, 26,
				0, 0, 183, 173, 155, 0, 1, 0, 0, 0,
				1, 26, 0, 12, 156, 174, 155, 23, 1
			});
			list.AddRange(new byte[]
			{
				0, 0, 2, 0, 0, 4, 7, 19, 6, 0,
				1, 6, 0, 0, 152, 137, 35, 128, 148, 35,
				156, 195, 235, 240, 10, 0, 0, 127, 0, 0,
				0, 0, 0, 127, 0, 0, 0, 127, 0, 0,
				0, 0, 0, 0, 0, 0, 2, 2, 54, 1,
				251, 241, 245
			});
			list.AddRange(new byte[]
			{
				193, 3, 3, 3, 148, 186, 34, 148, 186, 34,
				249, 191, 236, 240, 10, 3
			});
			list.AddRange(new byte[] { 28, 2, 0, 28, 0, 0, 26, 27, 0 });
			list.AddRange(new byte[]
			{
				0, 0, 127, 0, 0, 127, 1, 0, 0, 2,
				163, 23, 12, 1, 170, 2, 0, 0, 0, 0,
				0, 38, 128, 215, 176, 1, 38, 128, 215, 176,
				1, 38, 128, 215, 177, 1
			});
			list.AddString(this.GetAvatarName());
			list.Add(this.GetNameSet());
			list.AddRange(new byte[] { 0, 54, 2 });
			list.AddRange(Message.AddVInt(this.GetScore()));
			list.Add(1);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.AddRange(Message.AddVInt(this.GetAvatarLevel()));
			list.AddRange(Message.AddVInt(this.GetScore()));
			list.AddRange(Message.AddVInt(this.GetScore()));
			list.Add(4);
			list.Add(6);
			list.Add(5);
			list.Add(5);
			list.Add(1);
			list.AddRange(Message.AddVInt(500000));
			list.Add(5);
			list.Add(2);
			list.Add(7);
			list.Add(5);
			list.Add(3);
			list.Add(10);
			List<ClientAvatar.CrownChests> chests = this.GetChests();
			if (chests.Count > 0)
			{
				using (List<ClientAvatar.CrownChests>.Enumerator enumerator = chests.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClientAvatar.CrownChests crownChests = enumerator.Current;
						list.Add(crownChests.ressources);
						list.Add(crownChests.stars);
						list.Add(crownChests.crown);
					}
					goto IL_0207;
				}
			}
			list.Add(5);
			list.Add(4);
			list.Add(0);
			IL_0207:
			list.Add(5);
			list.Add(5);
			list.AddRange(Message.AddVInt(500000));
			list.AddRange(new byte[]
			{
				0, 7, 60, 7, 9, 60, 8, 9, 60, 9,
				9, 60, 4, 1, 60, 5, 1, 60, 6, 1,
				60, 10, 1, 1, 60, 10, 1, 1, 5, 8,
				9, 7
			});
			list.AddRange(new byte[]
			{
				26, 0, 11, 26, 1, 8, 26, 3, 9, 26,
				13, 13, 26, 14, 6, 28, 0, 2, 26, 12,
				4
			});
			list.AddRange(Message.AddVInt(this.GetDiamonds()));
			list.Add(10);
			list.AddRange(Message.AddVInt(this.GetExperience()));
			list.Add((byte)this.GetAvatarLevel());
			list.Add(1);
			if (this.GetAllianceId() != 0L)
			{
				Alliance alliance = ObjectManager.GetAlliance(this.GetAllianceId());
				list.Add(1);
				list.AddRange(Message.AddVInt(1));
				list.AddRange(Message.AddVInt(1));
				list.AddString(alliance.GetAllianceName());
				list.Add(0);
				list.Add(1);
			}
			else
			{
				list.Add(0);
			}
			return list.ToArray();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00014F78 File Offset: 0x00013178
		public void AddDiamonds(int diamondCount)
		{
			this.m_vCurrentGems += diamondCount;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00014F88 File Offset: 0x00013188
		public List<ClientAvatar.CrownChests> GetChests()
		{
			return this.Chests;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00014F90 File Offset: 0x00013190
		public long GetAllianceId()
		{
			return this.m_vAllianceId;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00014F98 File Offset: 0x00013198
		public AllianceMemberEntry GetAllianceMemberEntry()
		{
			Alliance alliance = ObjectManager.GetAlliance(this.m_vAllianceId);
			if (alliance != null)
			{
				return alliance.GetAllianceMember(this.m_vId);
			}
			return null;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00014FC4 File Offset: 0x000131C4
		public int GetAllianceRole()
		{
			AllianceMemberEntry allianceMemberEntry = this.GetAllianceMemberEntry();
			if (allianceMemberEntry != null)
			{
				return allianceMemberEntry.GetRole();
			}
			return -1;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00014FE3 File Offset: 0x000131E3
		public int GetAvatarLevel()
		{
			return this.m_vAvatarLevel;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00014FEB File Offset: 0x000131EB
		public string GetAvatarName()
		{
			return this.m_vAvatarName;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00014FF3 File Offset: 0x000131F3
		public int GetExperience()
		{
			return this.m_vExperience;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00014FFB File Offset: 0x000131FB
		public long GetCurrentHomeId()
		{
			return this.m_vCurrentHomeId;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00015003 File Offset: 0x00013203
		public int GetDiamonds()
		{
			return this.m_vCurrentGems;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001500B File Offset: 0x0001320B
		public long GetId()
		{
			return this.m_vId;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00015013 File Offset: 0x00013213
		public int GetArenaId()
		{
			return this.m_vArenaId;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001501B File Offset: 0x0001321B
		public int GetScore()
		{
			return this.m_vScore;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00015024 File Offset: 0x00013224
		public int GetSecondsFromLastUpdate()
		{
			return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - this.LastUpdate;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001505A File Offset: 0x0001325A
		public string GetUserToken()
		{
			return this.m_vToken;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00015062 File Offset: 0x00013262
		public bool HasEnoughDiamonds(int diamondCount)
		{
			return this.m_vCurrentGems >= diamondCount;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00015070 File Offset: 0x00013270
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
			this.m_vnameChosenByUser = jobject["nameChosenByUser"].ToObject<byte>();
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
			this.TutorialStepsCount = jobject["tutorial_step"].ToObject<uint>();
			foreach (JToken jtoken3 in ((JArray)jobject["achievements_progress"]))
			{
				JObject jobject4 = (JObject)jtoken3;
				DataSlot dataSlot3 = new DataSlot(null, 0);
				dataSlot3.Load(jobject4);
				this.Achievements.Add(dataSlot3);
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000152B8 File Offset: 0x000134B8
		public void SetAllianceRole(int a)
		{
			AllianceMemberEntry allianceMemberEntry = this.GetAllianceMemberEntry();
			if (allianceMemberEntry != null)
			{
				allianceMemberEntry.SetRole(a);
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000152D8 File Offset: 0x000134D8
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
			jobject.Add("nameChosenByUser", (ushort)this.m_vnameChosenByUser);
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
			jobject.Add("tutorial_step", this.TutorialStepsCount);
			JArray jarray3 = new JArray();
			foreach (DataSlot dataSlot3 in this.Achievements)
			{
				jarray3.Add(dataSlot3.Save(new JObject()));
			}
			jobject.Add("achievements_progress", jarray3);
			return JsonConvert.SerializeObject(jobject);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00015520 File Offset: 0x00013720
		public void SetAllianceId(long id)
		{
			this.m_vAllianceId = id;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00015529 File Offset: 0x00013729
		public void SetDiamonds(int count)
		{
			this.m_vCurrentGems = count;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00015532 File Offset: 0x00013732
		public void SetArenaId(int id)
		{
			this.m_vArenaId = id;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001553B File Offset: 0x0001373B
		public void SetScore(int newScore)
		{
			this.m_vScore = newScore;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00015544 File Offset: 0x00013744
		public void SetName(string name)
		{
			this.m_vAvatarName = name;
			this.m_vnameChosenByUser = 1;
			this.m_vNameChangingLeft = 1;
			this.TutorialStepsCount = 13U;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00015563 File Offset: 0x00013763
		public byte GetNameSet()
		{
			return this.m_vnameChosenByUser;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001556B File Offset: 0x0001376B
		public void SetToken(string token)
		{
			this.m_vToken = token;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00015574 File Offset: 0x00013774
		public void UseDiamonds(int diamondCount)
		{
			this.m_vCurrentGems -= diamondCount;
		}

		// Token: 0x040002D6 RID: 726
		private long m_vAllianceId;

		// Token: 0x040002D7 RID: 727
		private int m_vAvatarLevel;

		// Token: 0x040002D8 RID: 728
		private string m_vAvatarName;

		// Token: 0x040002D9 RID: 729
		private int m_vCurrentGems;

		// Token: 0x040002DA RID: 730
		private long m_vCurrentHomeId;

		// Token: 0x040002DB RID: 731
		private int m_vExperience;

		// Token: 0x040002DC RID: 732
		private long m_vId;

		// Token: 0x040002DD RID: 733
		private int m_vArenaId;

		// Token: 0x040002DE RID: 734
		private byte m_vNameChangingLeft;

		// Token: 0x040002DF RID: 735
		private byte m_vnameChosenByUser;

		// Token: 0x040002E0 RID: 736
		private int m_vScore;

		// Token: 0x040002E1 RID: 737
		private string m_vToken;

		// Token: 0x0200012C RID: 300
		public class CrownChests
		{
			// Token: 0x170000EE RID: 238
			// (get) Token: 0x0600071C RID: 1820 RVA: 0x00013325 File Offset: 0x00011525
			public byte ressources
			{
				get
				{
					return 5;
				}
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x0600071D RID: 1821 RVA: 0x00013E0C File Offset: 0x0001200C
			public byte stars
			{
				get
				{
					return 4;
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x0600071E RID: 1822 RVA: 0x000188CC File Offset: 0x00016ACC
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

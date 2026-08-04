using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x020000B4 RID: 180
	internal class Alliance
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x00015A26 File Offset: 0x00013C26
		public Alliance()
		{
			this.m_vChatMessages = new List<StreamEntry>();
			this.m_vAllianceMembers = new Dictionary<long, AllianceMemberEntry>();
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00015A44 File Offset: 0x00013C44
		public Alliance(long id)
		{
			Random random = new Random();
			this.m_vAllianceId = id;
			this.m_vAllianceName = "Default";
			this.m_vAllianceDescription = "Default";
			this.m_vAllianceBadgeData = 0;
			this.m_vAllianceType = 0;
			this.m_vRequiredScore = 0;
			this.m_vWarFrequency = 0;
			this.m_vAllianceOrigin = 32000006;
			this.m_vScore = 0;
			this.m_vAllianceExperience = random.Next(1, 5000);
			this.m_vAllianceLevel = random.Next(1, 10);
			this.m_vWonWars = random.Next(0, 300);
			this.m_vLostWars = random.Next(0, 300);
			this.m_vDrawWars = random.Next(0, 300);
			this.m_vChatMessages = new List<StreamEntry>();
			this.m_vAllianceMembers = new Dictionary<long, AllianceMemberEntry>();
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00015B15 File Offset: 0x00013D15
		public void AddAllianceMember(AllianceMemberEntry entry)
		{
			this.m_vAllianceMembers.Add(entry.GetAvatarId(), entry);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00015B29 File Offset: 0x00013D29
		public void AddChatMessage(StreamEntry message)
		{
			while (this.m_vChatMessages.Count >= 30)
			{
				this.m_vChatMessages.RemoveAt(0);
			}
			this.m_vChatMessages.Add(message);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00015B54 File Offset: 0x00013D54
		public byte[] EncodeFullEntry()
		{
			List<byte> list = new List<byte>();
			list.AddInt64(this.m_vAllianceId);
			list.AddString(this.m_vAllianceName);
			list.AddInt32(this.m_vAllianceBadgeData);
			list.AddInt32(this.m_vAllianceType);
			list.AddInt32(this.m_vAllianceMembers.Count);
			list.AddInt32(this.m_vScore);
			list.AddInt32(this.m_vRequiredScore);
			list.AddInt32(this.m_vWonWars);
			list.AddInt32(this.m_vLostWars);
			list.AddInt32(this.m_vDrawWars);
			list.AddInt32(2000001);
			list.AddInt32(this.m_vWarFrequency);
			list.AddInt32(this.m_vAllianceOrigin);
			list.AddInt32(this.m_vAllianceExperience);
			list.AddInt32(this.m_vAllianceLevel);
			list.AddInt32(0);
			return list.ToArray();
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00015C2C File Offset: 0x00013E2C
		public byte[] EncodeHeader()
		{
			List<byte> list = new List<byte>();
			list.AddInt64(this.m_vAllianceId);
			list.AddString(this.m_vAllianceName);
			list.AddInt32(this.m_vAllianceBadgeData);
			list.Add(0);
			list.AddInt32(this.m_vAllianceLevel);
			list.AddInt32(1);
			list.AddInt32(-1);
			return list.ToArray();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00015C88 File Offset: 0x00013E88
		public static byte[] EncodeMembers()
		{
			return new List<byte>().ToArray();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00015C94 File Offset: 0x00013E94
		public int GetAllianceBadgeData()
		{
			return this.m_vAllianceBadgeData;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00015C9C File Offset: 0x00013E9C
		public string GetAllianceDescription()
		{
			return this.m_vAllianceDescription;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00015CA4 File Offset: 0x00013EA4
		public int GetAllianceExperience()
		{
			return this.m_vAllianceExperience;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00015CAC File Offset: 0x00013EAC
		public long GetAllianceId()
		{
			return this.m_vAllianceId;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00015CB4 File Offset: 0x00013EB4
		public int GetAllianceLevel()
		{
			return this.m_vAllianceLevel;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00015CBC File Offset: 0x00013EBC
		public AllianceMemberEntry GetAllianceMember(long avatarId)
		{
			return this.m_vAllianceMembers[avatarId];
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00015CCA File Offset: 0x00013ECA
		public List<AllianceMemberEntry> GetAllianceMembers()
		{
			return this.m_vAllianceMembers.Values.ToList<AllianceMemberEntry>();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00015CDC File Offset: 0x00013EDC
		public string GetAllianceName()
		{
			return this.m_vAllianceName;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00015CE4 File Offset: 0x00013EE4
		public int GetAllianceOrigin()
		{
			return this.m_vAllianceOrigin;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00015CEC File Offset: 0x00013EEC
		public int GetAllianceType()
		{
			return this.m_vAllianceType;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00015CF4 File Offset: 0x00013EF4
		public List<StreamEntry> GetChatMessages()
		{
			return this.m_vChatMessages;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00015CFC File Offset: 0x00013EFC
		public int GetRequiredScore()
		{
			return this.m_vRequiredScore;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00015D04 File Offset: 0x00013F04
		public int GetScore()
		{
			return this.m_vScore;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00015D0C File Offset: 0x00013F0C
		public int GetWarFrequency()
		{
			return this.m_vWarFrequency;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00015D14 File Offset: 0x00013F14
		public int GetWarScore()
		{
			return this.m_vWonWars;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00015D1C File Offset: 0x00013F1C
		public bool IsAllianceFull()
		{
			return this.m_vAllianceMembers.Count >= 50;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00015D30 File Offset: 0x00013F30
		public void LoadFromJSON(string jsonString)
		{
			JObject jobject = JObject.Parse(jsonString);
			this.m_vAllianceId = jobject["alliance_id"].ToObject<long>();
			this.m_vAllianceName = jobject["alliance_name"].ToObject<string>();
			this.m_vAllianceBadgeData = jobject["alliance_badge"].ToObject<int>();
			this.m_vAllianceType = jobject["alliance_type"].ToObject<int>();
			if (jobject["required_score"] != null)
			{
				this.m_vRequiredScore = jobject["required_score"].ToObject<int>();
			}
			this.m_vAllianceDescription = jobject["description"].ToObject<string>();
			this.m_vAllianceExperience = jobject["alliance_experience"].ToObject<int>();
			this.m_vAllianceLevel = jobject["alliance_level"].ToObject<int>();
			if (jobject["won_wars"] != null)
			{
				this.m_vWonWars = jobject["won_wars"].ToObject<int>();
			}
			if (jobject["lost_wars"] != null)
			{
				this.m_vLostWars = jobject["lost_wars"].ToObject<int>();
			}
			if (jobject["draw_wars"] != null)
			{
				this.m_vDrawWars = jobject["draw_wars"].ToObject<int>();
			}
			if (jobject["war_frequency"] != null)
			{
				this.m_vWarFrequency = jobject["war_frequency"].ToObject<int>();
			}
			if (jobject["alliance_origin"] != null)
			{
				this.m_vAllianceOrigin = jobject["alliance_origin"].ToObject<int>();
			}
			foreach (JToken jtoken in ((JArray)jobject["members"]))
			{
				JObject jobject2 = (JObject)jtoken;
				long num = jobject2["avatar_id"].ToObject<long>();
				AllianceMemberEntry allianceMemberEntry = new AllianceMemberEntry(num);
				Level level = new Level(num);
				this.m_vScore += level.GetPlayerAvatar().GetScore();
				allianceMemberEntry.Load(jobject2);
				this.m_vAllianceMembers.Add(num, allianceMemberEntry);
			}
			this.m_vScore /= 2;
			JArray jarray = (JArray)jobject["chatMessages"];
			if (jarray != null)
			{
				foreach (JToken jtoken2 in jarray)
				{
					JObject jobject3 = (JObject)jtoken2;
					StreamEntry streamEntry = new StreamEntry();
					switch (jobject3["type"].ToObject<int>())
					{
					case 1:
						streamEntry = new TroopRequestStreamEntry();
						break;
					case 2:
						streamEntry = new ChatStreamEntry();
						break;
					case 3:
						streamEntry = new InvitationStreamEntry();
						break;
					case 4:
						streamEntry = new AllianceEventStreamEntry();
						break;
					case 5:
						streamEntry = new ShareStreamEntry();
						break;
					}
					streamEntry.Load(jobject3);
					this.m_vChatMessages.Add(streamEntry);
				}
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00016028 File Offset: 0x00014228
		public void RemoveMember(long avatarId)
		{
			this.m_vAllianceMembers.Remove(avatarId);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00016038 File Offset: 0x00014238
		public string SaveToJSON()
		{
			JObject jobject = new JObject();
			jobject.Add("alliance_id", this.m_vAllianceId);
			jobject.Add("alliance_name", this.m_vAllianceName);
			jobject.Add("alliance_badge", this.m_vAllianceBadgeData);
			jobject.Add("alliance_type", this.m_vAllianceType);
			jobject.Add("score", this.m_vScore);
			jobject.Add("required_score", this.m_vRequiredScore);
			jobject.Add("description", this.m_vAllianceDescription);
			jobject.Add("alliance_experience", this.m_vAllianceExperience);
			jobject.Add("alliance_level", this.m_vAllianceLevel);
			jobject.Add("won_wars", this.m_vWonWars);
			jobject.Add("lost_wars", this.m_vLostWars);
			jobject.Add("draw_wars", this.m_vDrawWars);
			jobject.Add("war_frequency", this.m_vWarFrequency);
			jobject.Add("alliance_origin", this.m_vAllianceOrigin);
			JArray jarray = new JArray();
			foreach (AllianceMemberEntry allianceMemberEntry in this.m_vAllianceMembers.Values)
			{
				JObject jobject2 = new JObject();
				allianceMemberEntry.Save(jobject2);
				jarray.Add(jobject2);
			}
			jobject.Add("members", jarray);
			JArray jarray2 = new JArray();
			foreach (StreamEntry streamEntry in this.m_vChatMessages)
			{
				JObject jobject3 = new JObject();
				streamEntry.Save(jobject3);
				jarray2.Add(jobject3);
			}
			jobject.Add("chatMessages", jarray2);
			return JsonConvert.SerializeObject(jobject);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00016258 File Offset: 0x00014458
		public void SetAllianceBadgeData(int data)
		{
			this.m_vAllianceBadgeData = data;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00016261 File Offset: 0x00014461
		public void SetAllianceDescription(string description)
		{
			this.m_vAllianceDescription = description;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001626A File Offset: 0x0001446A
		public void SetAllianceLevel(int level)
		{
			this.m_vAllianceLevel = level;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00016273 File Offset: 0x00014473
		public void SetAllianceName(string name)
		{
			this.m_vAllianceName = name;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001627C File Offset: 0x0001447C
		public void SetAllianceOrigin(int origin)
		{
			this.m_vAllianceOrigin = origin;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00016285 File Offset: 0x00014485
		public void SetAllianceType(int status)
		{
			this.m_vAllianceType = status;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001628E File Offset: 0x0001448E
		public void SetRequiredScore(int score)
		{
			this.m_vRequiredScore = score;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00016297 File Offset: 0x00014497
		public void SetWarFrequency(int frequency)
		{
			this.m_vWarFrequency = frequency;
		}

		// Token: 0x04000302 RID: 770
		private const int m_vMaxAllianceMembers = 50;

		// Token: 0x04000303 RID: 771
		private const int m_vMaxChatMessagesNumber = 30;

		// Token: 0x04000304 RID: 772
		private readonly Dictionary<long, AllianceMemberEntry> m_vAllianceMembers;

		// Token: 0x04000305 RID: 773
		private readonly List<StreamEntry> m_vChatMessages;

		// Token: 0x04000306 RID: 774
		private int m_vAllianceBadgeData;

		// Token: 0x04000307 RID: 775
		private string m_vAllianceDescription;

		// Token: 0x04000308 RID: 776
		private int m_vAllianceExperience;

		// Token: 0x04000309 RID: 777
		private long m_vAllianceId;

		// Token: 0x0400030A RID: 778
		private int m_vAllianceLevel;

		// Token: 0x0400030B RID: 779
		private string m_vAllianceName;

		// Token: 0x0400030C RID: 780
		private int m_vAllianceOrigin;

		// Token: 0x0400030D RID: 781
		private int m_vAllianceType;

		// Token: 0x0400030E RID: 782
		private int m_vDrawWars;

		// Token: 0x0400030F RID: 783
		private int m_vLostWars;

		// Token: 0x04000310 RID: 784
		private int m_vRequiredScore;

		// Token: 0x04000311 RID: 785
		private int m_vScore;

		// Token: 0x04000312 RID: 786
		private int m_vWarFrequency;

		// Token: 0x04000313 RID: 787
		private int m_vWonWars;
	}
}

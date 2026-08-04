using System;
using System.Collections.Generic;
using System.Linq;
using CRS.Core;
using CRS.Helpers;
using CRS.PacketProcessing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StreamEntryType = CRS.Logic.StreamEntry.StreamEntry;
using CRS.Logic.StreamEntry;

namespace CRS.Logic
{
	// Token: 0x020000D2 RID: 210
	internal class Alliance
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x0001D464 File Offset: 0x0001B664
		public Alliance()
		{
			this.m_vChatMessages = new List<StreamEntryType>();
			this.m_vAllianceMembers = new Dictionary<long, AllianceMemberEntry>();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001D484 File Offset: 0x0001B684
		public Alliance(long id)
		{
			new Random();
			this.m_vAllianceId = id;
			this.m_vAllianceName = "Default";
			this.m_vAllianceDescription = "Default";
			this.m_vAllianceBadgeData = 26;
			this.m_vAllianceType = 0;
			this.m_vRequiredScore = 0;
			this.m_vAllianceOrigin = 32000001;
			this.m_vScore = 0;
			this.m_vChatMessages = new List<StreamEntryType>();
			this.m_vAllianceMembers = new Dictionary<long, AllianceMemberEntry>();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001D4F8 File Offset: 0x0001B6F8
		public void AddAllianceMember(AllianceMemberEntry entry)
		{
			this.m_vAllianceMembers.Add(entry.GetAvatarId(), entry);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001D50C File Offset: 0x0001B70C
		public void AddChatMessage(StreamEntryType message)
		{
			while (this.m_vChatMessages.Count >= 2)
			{
				this.m_vChatMessages.RemoveAt(0);
			}
			this.m_vChatMessages.Add(message);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001D536 File Offset: 0x0001B736
		public byte[] EncodeFullEntry()
		{
			List<byte> list = new List<byte>();
			list.AddRange(this.EncodeJoinableAlliance());
			list.AddString(this.m_vAllianceDescription);
			return list.ToArray();
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001D55C File Offset: 0x0001B75C
		public byte[] EncodeJoinableAlliance()
		{
			List<byte> list = new List<byte>();
			list.AddInt64(this.m_vAllianceId);
			list.AddString(this.m_vAllianceName);
			list.Add(16);
			list.AddRange(Message.AddVInt(this.m_vAllianceBadgeData));
			list.Add(this.m_vAllianceType);
			list.Add((byte)this.m_vAllianceMembers.Count);
			list.AddRange(Message.AddVInt(this.m_vScore));
			list.AddRange(Message.AddVInt(this.m_vRequiredScore));
			list.Add(0);
			list.AddRange(Helpers.Helpers.HexaToBytes("00008001"));
			list.Add(0);
			list.AddRange(Helpers.Helpers.HexaToBytes("94250101399701"));
			return list.ToArray();
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001D612 File Offset: 0x0001B812
		public byte[] EncodeHeader()
		{
			List<byte> list = new List<byte>();
			list.AddInt64(this.m_vAllianceId);
			list.AddString(this.m_vAllianceName);
			list.Add(16);
			list.AddRange(Message.AddVInt(this.m_vAllianceBadgeData));
			return list.ToArray();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001D64F File Offset: 0x0001B84F
		public int GetAllianceBadgeData()
		{
			return this.m_vAllianceBadgeData;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001D657 File Offset: 0x0001B857
		public string GetAllianceDescription()
		{
			return this.m_vAllianceDescription;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001D65F File Offset: 0x0001B85F
		public long GetAllianceId()
		{
			return this.m_vAllianceId;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001D667 File Offset: 0x0001B867
		public AllianceMemberEntry GetAllianceMember(long avatarId)
		{
			return this.m_vAllianceMembers[avatarId];
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001D675 File Offset: 0x0001B875
		public List<AllianceMemberEntry> GetAllianceMembers()
		{
			return this.m_vAllianceMembers.Values.ToList<AllianceMemberEntry>();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001D687 File Offset: 0x0001B887
		public string GetAllianceName()
		{
			return this.m_vAllianceName;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001D68F File Offset: 0x0001B88F
		public int GetAllianceOrigin()
		{
			return this.m_vAllianceOrigin;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001D697 File Offset: 0x0001B897
		public byte GetAllianceType()
		{
			return this.m_vAllianceType;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001D69F File Offset: 0x0001B89F
		public List<StreamEntryType> GetChatMessages()
		{
			return this.m_vChatMessages;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001D6A7 File Offset: 0x0001B8A7
		public int GetRequiredScore()
		{
			return this.m_vRequiredScore;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001D6AF File Offset: 0x0001B8AF
		public int GetScore()
		{
			return this.m_vScore;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001D6B7 File Offset: 0x0001B8B7
		public int GetWarFrequency()
		{
			return this.m_vWarFrequency;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001D6BF File Offset: 0x0001B8BF
		public bool IsAllianceFull()
		{
			return this.m_vAllianceMembers.Count >= 50;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		public void LoadFromJSON(string jsonString)
		{
			JObject jobject = JObject.Parse(jsonString);
			this.m_vAllianceId = jobject["alliance_id"].ToObject<long>();
			this.m_vAllianceName = jobject["alliance_name"].ToObject<string>();
			this.m_vAllianceBadgeData = jobject["alliance_badge"].ToObject<int>();
			this.m_vAllianceType = jobject["alliance_type"].ToObject<byte>();
			this.m_vRequiredScore = jobject["required_score"].ToObject<int>();
			this.m_vAllianceDescription = jobject["description"].ToObject<string>();
			this.m_vAllianceOrigin = jobject["alliance_origin"].ToObject<int>();
			foreach (JToken jtoken in ((JArray)jobject["members"]))
			{
				JObject jobject2 = (JObject)jtoken;
				long num = jobject2["avatar_id"].ToObject<long>();
				Level player = ResourcesManager.GetPlayer(num, false);
				AllianceMemberEntry allianceMemberEntry = new AllianceMemberEntry(num);
				this.m_vScore += player.GetPlayerAvatar().GetScore();
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
					StreamEntryType streamEntry = new StreamEntryType();
					if (jobject3["type"].ToObject<int>() == 1)
					{
						streamEntry = new TroopRequestStreamEntry();
					}
					else if (jobject3["type"].ToObject<int>() == 2)
					{
						streamEntry = new ChatStreamEntry();
					}
					else if (jobject3["type"].ToObject<int>() == 3)
					{
						streamEntry = new InvitationStreamEntry();
					}
					else if (jobject3["type"].ToObject<int>() == 4)
					{
						streamEntry = new AllianceEventStreamEntry();
					}
					else if (jobject3["type"].ToObject<int>() == 5)
					{
						streamEntry = new ShareStreamEntry();
					}
					streamEntry.Load(jobject3);
					this.m_vChatMessages.Add(streamEntry);
				}
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001D92C File Offset: 0x0001BB2C
		public void RemoveMember(long avatarId)
		{
			this.m_vAllianceMembers.Remove(avatarId);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001D93C File Offset: 0x0001BB3C
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
			foreach (StreamEntryType streamEntry in this.m_vChatMessages)
			{
				JObject jobject3 = new JObject();
				streamEntry.Save(jobject3);
				jarray2.Add(jobject3);
			}
			jobject.Add("chatMessages", jarray2);
			return JsonConvert.SerializeObject(jobject);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001DAD8 File Offset: 0x0001BCD8
		public void SetAllianceBadgeData(int data)
		{
			this.m_vAllianceBadgeData = data;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001DAE1 File Offset: 0x0001BCE1
		public void SetAllianceDescription(string description)
		{
			this.m_vAllianceDescription = description;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001DAEA File Offset: 0x0001BCEA
		public void SetAllianceName(string name)
		{
			this.m_vAllianceName = name;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001DAF3 File Offset: 0x0001BCF3
		public void SetAllianceOrigin(int origin)
		{
			this.m_vAllianceOrigin = origin;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001DAFC File Offset: 0x0001BCFC
		public void SetAllianceType(byte status)
		{
			this.m_vAllianceType = status;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001DB05 File Offset: 0x0001BD05
		public void SetRequiredScore(int score)
		{
			this.m_vRequiredScore = score;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001DB0E File Offset: 0x0001BD0E
		public void SetWarFrequency(int frequency)
		{
			this.m_vWarFrequency = frequency;
		}

		// Token: 0x04000390 RID: 912
		private const int m_vMaxAllianceMembers = 50;

		// Token: 0x04000391 RID: 913
		private const int m_vMaxChatMessagesNumber = 2;

		// Token: 0x04000392 RID: 914
		private readonly Dictionary<long, AllianceMemberEntry> m_vAllianceMembers;

		// Token: 0x04000393 RID: 915
		private readonly List<StreamEntryType> m_vChatMessages;

		// Token: 0x04000394 RID: 916
		private int m_vAllianceBadgeData;

		// Token: 0x04000395 RID: 917
		private string m_vAllianceDescription;

		// Token: 0x04000396 RID: 918
		private long m_vAllianceId;

		// Token: 0x04000397 RID: 919
		private string m_vAllianceName;

		// Token: 0x04000398 RID: 920
		private int m_vAllianceOrigin;

		// Token: 0x04000399 RID: 921
		private byte m_vAllianceType;

		// Token: 0x0400039A RID: 922
		private int m_vRequiredScore;

		// Token: 0x0400039B RID: 923
		private int m_vScore;

		// Token: 0x0400039C RID: 924
		private int m_vWarFrequency;
	}
}

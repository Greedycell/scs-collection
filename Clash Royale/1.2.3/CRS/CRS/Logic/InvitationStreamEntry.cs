using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x02000094 RID: 148
	internal class InvitationStreamEntry : StreamEntry
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x00013179 File Offset: 0x00011379
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddString(InvitationStreamEntry.Message);
			list.AddString(InvitationStreamEntry.Judge);
			list.AddInt32(InvitationStreamEntry.State);
			return list.ToArray();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000131B2 File Offset: 0x000113B2
		public override int GetStreamEntryType()
		{
			return 3;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000131B8 File Offset: 0x000113B8
		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			InvitationStreamEntry.Message = jsonObject["message"].ToObject<string>();
			InvitationStreamEntry.Judge = jsonObject["judge"].ToObject<string>();
			InvitationStreamEntry.State = jsonObject["state"].ToObject<int>();
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001320C File Offset: 0x0001140C
		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("message", InvitationStreamEntry.Message);
			jsonObject.Add("judge", InvitationStreamEntry.Judge);
			jsonObject.Add("state", InvitationStreamEntry.State);
			return jsonObject;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00013262 File Offset: 0x00011462
		public void SetJudgeName(string name)
		{
			InvitationStreamEntry.Judge = name;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001326A File Offset: 0x0001146A
		public void SetMessage(string message)
		{
			InvitationStreamEntry.Message = message;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00013272 File Offset: 0x00011472
		public void SetState(int status)
		{
			InvitationStreamEntry.State = status;
		}

		// Token: 0x04000278 RID: 632
		public static string Message = "Hello, i want to join your clan.";

		// Token: 0x04000279 RID: 633
		public static string Judge;

		// Token: 0x0400027A RID: 634
		public static int State = 3;
	}
}

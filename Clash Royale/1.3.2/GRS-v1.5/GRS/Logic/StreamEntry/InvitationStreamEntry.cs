using System;
using System.Collections.Generic;
using CRS.Helpers;
using Newtonsoft.Json.Linq;

namespace CRS.Logic.StreamEntry
{
	// Token: 0x020000D4 RID: 212
	internal class InvitationStreamEntry : StreamEntry
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x0001DB2D File Offset: 0x0001BD2D
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddString(InvitationStreamEntry.Message);
			list.AddString(InvitationStreamEntry.Judge);
			list.AddInt32(InvitationStreamEntry.State);
			return list.ToArray();
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001DB66 File Offset: 0x0001BD66
		public override int GetStreamEntryType()
		{
			return 3;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001DB6C File Offset: 0x0001BD6C
		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			InvitationStreamEntry.Message = jsonObject["message"].ToObject<string>();
			InvitationStreamEntry.Judge = jsonObject["judge"].ToObject<string>();
			InvitationStreamEntry.State = jsonObject["state"].ToObject<int>();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001DBC0 File Offset: 0x0001BDC0
		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("message", InvitationStreamEntry.Message);
			jsonObject.Add("judge", InvitationStreamEntry.Judge);
			jsonObject.Add("state", InvitationStreamEntry.State);
			return jsonObject;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001DC16 File Offset: 0x0001BE16
		public void SetJudgeName(string name)
		{
			InvitationStreamEntry.Judge = name;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001DC1E File Offset: 0x0001BE1E
		public void SetMessage(string message)
		{
			InvitationStreamEntry.Message = message;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001DC26 File Offset: 0x0001BE26
		public void SetState(int status)
		{
			InvitationStreamEntry.State = status;
		}

		// Token: 0x0400039F RID: 927
		public static string Message = "Hello, i want to join your clan.";

		// Token: 0x040003A0 RID: 928
		public static string Judge;

		// Token: 0x040003A1 RID: 929
		public static int State = 3;
	}
}

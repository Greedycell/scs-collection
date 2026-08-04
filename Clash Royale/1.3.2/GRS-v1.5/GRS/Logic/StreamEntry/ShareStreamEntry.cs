using System;
using System.Collections.Generic;
using CRS.Helpers;
using Newtonsoft.Json.Linq;

namespace CRS.Logic.StreamEntry
{
	// Token: 0x020000D5 RID: 213
	internal class ShareStreamEntry : StreamEntry
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x0001DC48 File Offset: 0x0001BE48
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddInt32(ShareStreamEntry.Unknown1);
			list.AddInt32(ShareStreamEntry.Unknown2);
			list.AddInt32(ShareStreamEntry.Unknown3);
			list.Add(ShareStreamEntry.Unknown4);
			list.AddString(ShareStreamEntry.Message);
			list.AddString(ShareStreamEntry.EnemyName);
			list.AddString(ShareStreamEntry.ReplayJson);
			list.AddInt32(ShareStreamEntry.Unknown5);
			list.AddInt32(ShareStreamEntry.Unknown6);
			list.AddInt32(ShareStreamEntry.Unknown7);
			return list.ToArray();
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001DCD9 File Offset: 0x0001BED9
		public override int GetStreamEntryType()
		{
			return 5;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001DCDC File Offset: 0x0001BEDC
		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			ShareStreamEntry.Unknown1 = jsonObject["unknown1"].ToObject<int>();
			ShareStreamEntry.Unknown2 = jsonObject["unknown2"].ToObject<int>();
			ShareStreamEntry.Unknown3 = jsonObject["unknown3"].ToObject<int>();
			ShareStreamEntry.Unknown4 = jsonObject["unknown4"].ToObject<byte>();
			ShareStreamEntry.Message = jsonObject["message"].ToObject<string>();
			ShareStreamEntry.EnemyName = jsonObject["enemy"].ToObject<string>();
			ShareStreamEntry.ReplayJson = jsonObject["replay"].ToObject<string>();
			ShareStreamEntry.Unknown5 = jsonObject["unknown5"].ToObject<int>();
			ShareStreamEntry.Unknown6 = jsonObject["unknown6"].ToObject<int>();
			ShareStreamEntry.Unknown7 = jsonObject["unknown7"].ToObject<int>();
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("unknown1", ShareStreamEntry.Unknown1);
			jsonObject.Add("unknown2", ShareStreamEntry.Unknown2);
			jsonObject.Add("unknown3", ShareStreamEntry.Unknown3);
			jsonObject.Add("unknown4", ShareStreamEntry.Unknown4);
			jsonObject.Add("message", ShareStreamEntry.Message);
			jsonObject.Add("enemy", ShareStreamEntry.EnemyName);
			jsonObject.Add("replay", ShareStreamEntry.ReplayJson);
			jsonObject.Add("unknown5", ShareStreamEntry.Unknown5);
			jsonObject.Add("unknown6", ShareStreamEntry.Unknown6);
			jsonObject.Add("unknown7", ShareStreamEntry.Unknown7);
			return jsonObject;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001DEAD File Offset: 0x0001C0AD
		public void SetEnemyName(string name)
		{
			ShareStreamEntry.EnemyName = name;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001DEB5 File Offset: 0x0001C0B5
		public void SetMessage(string message)
		{
			ShareStreamEntry.Message = message;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001DEBD File Offset: 0x0001C0BD
		public void SetReplayjson(string json)
		{
			ShareStreamEntry.ReplayJson = json;
		}

		// Token: 0x040003A2 RID: 930
		public static int Unknown1;

		// Token: 0x040003A3 RID: 931
		public static int Unknown2;

		// Token: 0x040003A4 RID: 932
		public static int Unknown3;

		// Token: 0x040003A5 RID: 933
		public static byte Unknown4;

		// Token: 0x040003A6 RID: 934
		public static string Message = "Look this battle !";

		// Token: 0x040003A7 RID: 935
		public static string EnemyName = "UltraPowa";

		// Token: 0x040003A8 RID: 936
		public static string ReplayJson;

		// Token: 0x040003A9 RID: 937
		public static int Unknown5;

		// Token: 0x040003AA RID: 938
		public static int Unknown6;

		// Token: 0x040003AB RID: 939
		public static int Unknown7;
	}
}

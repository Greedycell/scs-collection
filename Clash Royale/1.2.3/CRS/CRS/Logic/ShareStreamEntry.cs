using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x02000095 RID: 149
	internal class ShareStreamEntry : StreamEntry
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00013294 File Offset: 0x00011494
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

		// Token: 0x06000410 RID: 1040 RVA: 0x00013325 File Offset: 0x00011525
		public override int GetStreamEntryType()
		{
			return 5;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00013328 File Offset: 0x00011528
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

		// Token: 0x06000412 RID: 1042 RVA: 0x00013410 File Offset: 0x00011610
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

		// Token: 0x06000413 RID: 1043 RVA: 0x000134F9 File Offset: 0x000116F9
		public void SetEnemyName(string name)
		{
			ShareStreamEntry.EnemyName = name;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00013501 File Offset: 0x00011701
		public void SetMessage(string message)
		{
			ShareStreamEntry.Message = message;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00013509 File Offset: 0x00011709
		public void SetReplayjson(string json)
		{
			ShareStreamEntry.ReplayJson = json;
		}

		// Token: 0x0400027B RID: 635
		public static int Unknown1;

		// Token: 0x0400027C RID: 636
		public static int Unknown2;

		// Token: 0x0400027D RID: 637
		public static int Unknown3;

		// Token: 0x0400027E RID: 638
		public static byte Unknown4;

		// Token: 0x0400027F RID: 639
		public static string Message = "Look this battle !";

		// Token: 0x04000280 RID: 640
		public static string EnemyName = "UltraPowa";

		// Token: 0x04000281 RID: 641
		public static string ReplayJson;

		// Token: 0x04000282 RID: 642
		public static int Unknown5;

		// Token: 0x04000283 RID: 643
		public static int Unknown6;

		// Token: 0x04000284 RID: 644
		public static int Unknown7;
	}
}

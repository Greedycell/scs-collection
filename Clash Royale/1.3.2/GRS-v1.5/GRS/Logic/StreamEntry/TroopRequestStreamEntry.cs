using System;
using System.Collections.Generic;
using CRS.Helpers;
using Newtonsoft.Json.Linq;

namespace CRS.Logic.StreamEntry
{
	// Token: 0x020000D6 RID: 214
	internal class TroopRequestStreamEntry : StreamEntry
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x0001DEDC File Offset: 0x0001C0DC
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddInt32(TroopRequestStreamEntry.Unknown1);
			list.AddInt32(TroopRequestStreamEntry.Unknown2);
			list.AddInt32(TroopRequestStreamEntry.Unknown3);
			list.AddInt32(TroopRequestStreamEntry.Unknown4);
			list.AddInt32(TroopRequestStreamEntry.Unknown5);
			list.AddDataSlots(new List<DataSlot>());
			list.AddString(TroopRequestStreamEntry.Message);
			list.AddDataSlots(new List<DataSlot>());
			return list.ToArray();
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001238F File Offset: 0x0001058F
		public override int GetStreamEntryType()
		{
			return 1;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001DF58 File Offset: 0x0001C158
		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			TroopRequestStreamEntry.Unknown1 = jsonObject["unknown1"].ToObject<int>();
			TroopRequestStreamEntry.Unknown2 = jsonObject["unknown2"].ToObject<int>();
			TroopRequestStreamEntry.Unknown3 = jsonObject["unknown3"].ToObject<int>();
			TroopRequestStreamEntry.Unknown4 = jsonObject["unknown4"].ToObject<int>();
			TroopRequestStreamEntry.Unknown5 = jsonObject["unknown5"].ToObject<int>();
			TroopRequestStreamEntry.Message = jsonObject["message"].ToObject<string>();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001DFEC File Offset: 0x0001C1EC
		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("unknown1", TroopRequestStreamEntry.Unknown1);
			jsonObject.Add("unknown2", TroopRequestStreamEntry.Unknown2);
			jsonObject.Add("unknown3", TroopRequestStreamEntry.Unknown3);
			jsonObject.Add("unknown4", TroopRequestStreamEntry.Unknown4);
			jsonObject.Add("unknown5", TroopRequestStreamEntry.Unknown5);
			JObject jobject = jsonObject;
			string text = "donations";
			JArray jarray = new JArray();
			jarray.Add(300000);
			jarray.Add(0);
			jobject.Add(text, jarray);
			jsonObject.Add("message", TroopRequestStreamEntry.Message);
			jsonObject.Add("tdonations", new JArray());
			return jsonObject;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001E0BD File Offset: 0x0001C2BD
		public void SetMessage(string msg)
		{
			TroopRequestStreamEntry.Message = msg;
		}

		// Token: 0x040003AC RID: 940
		public static int Unknown1;

		// Token: 0x040003AD RID: 941
		public static int Unknown2 = 2;

		// Token: 0x040003AE RID: 942
		public static int Unknown3;

		// Token: 0x040003AF RID: 943
		public static int Unknown4 = 2;

		// Token: 0x040003B0 RID: 944
		public static int Unknown5;

		// Token: 0x040003B1 RID: 945
		public static string Message;

		// Token: 0x040003B2 RID: 946
		public static DataSlot AllianceDonation;

		// Token: 0x040003B3 RID: 947
		public static DataSlot UnitComponent;
	}
}

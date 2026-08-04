using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x02000096 RID: 150
	internal class TroopRequestStreamEntry : StreamEntry
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x00013528 File Offset: 0x00011728
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

		// Token: 0x06000419 RID: 1049 RVA: 0x000135A3 File Offset: 0x000117A3
		public override int GetStreamEntryType()
		{
			return 1;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000135A8 File Offset: 0x000117A8
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

		// Token: 0x0600041B RID: 1051 RVA: 0x0001363C File Offset: 0x0001183C
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

		// Token: 0x0600041C RID: 1052 RVA: 0x0001370D File Offset: 0x0001190D
		public void SetMessage(string msg)
		{
			TroopRequestStreamEntry.Message = msg;
		}

		// Token: 0x04000285 RID: 645
		public static int Unknown1;

		// Token: 0x04000286 RID: 646
		public static int Unknown2 = 2;

		// Token: 0x04000287 RID: 647
		public static int Unknown3;

		// Token: 0x04000288 RID: 648
		public static int Unknown4 = 2;

		// Token: 0x04000289 RID: 649
		public static int Unknown5;

		// Token: 0x0400028A RID: 650
		public static string Message;

		// Token: 0x0400028B RID: 651
		public static DataSlot AllianceDonation;

		// Token: 0x0400028C RID: 652
		public static DataSlot UnitComponent;
	}
}

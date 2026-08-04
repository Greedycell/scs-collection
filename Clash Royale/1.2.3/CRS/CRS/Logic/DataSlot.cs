using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UCS.Core;
using UCS.Files;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x020000AE RID: 174
	internal class DataSlot
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x00015584 File Offset: 0x00013784
		public DataSlot(Data d, int value)
		{
			this.Data = d;
			this.Value = value;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001559A File Offset: 0x0001379A
		public void Decode(BinaryReader br)
		{
			this.Data = br.ReadDataReference();
			this.Value = br.ReadInt32WithEndian();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000155B4 File Offset: 0x000137B4
		public byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(this.Data.GetGlobalID());
			list.AddInt32(this.Value);
			return list.ToArray();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000155DD File Offset: 0x000137DD
		public void Load(JObject jsonObject)
		{
			this.Data = ObjectManager.DataTables.GetDataById(jsonObject["global_id"].ToObject<int>());
			this.Value = jsonObject["value"].ToObject<int>();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00015615 File Offset: 0x00013815
		public JObject Save(JObject jsonObject)
		{
			jsonObject.Add("global_id", this.Data.GetGlobalID());
			jsonObject.Add("value", this.Value);
			return jsonObject;
		}

		// Token: 0x040002EE RID: 750
		public Data Data;

		// Token: 0x040002EF RID: 751
		public int Value;
	}
}

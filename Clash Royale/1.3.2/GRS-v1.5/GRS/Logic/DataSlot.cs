using System;
using System.Collections.Generic;
using System.IO;
using CRS.Core;
using CRS.Files.Logic;
using CRS.Helpers;
using Newtonsoft.Json.Linq;

namespace CRS.Logic
{
	// Token: 0x020000D0 RID: 208
	internal class DataSlot
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x0001D330 File Offset: 0x0001B530
		public DataSlot(Data d, int value)
		{
			this.Data = d;
			this.Value = value;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001D346 File Offset: 0x0001B546
		public void Decode(BinaryReader br)
		{
			this.Data = br.ReadDataReference();
			this.Value = br.ReadInt32WithEndian();
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001D360 File Offset: 0x0001B560
		public byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(this.Data.GetGlobalID());
			list.AddInt32(this.Value);
			return list.ToArray();
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001D389 File Offset: 0x0001B589
		public void Load(JObject jsonObject)
		{
			this.Data = ObjectManager.DataTables.GetDataById(jsonObject["global_id"].ToObject<int>());
			this.Value = jsonObject["value"].ToObject<int>();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001D3C1 File Offset: 0x0001B5C1
		public JObject Save(JObject jsonObject)
		{
			jsonObject.Add("global_id", this.Data.GetGlobalID());
			jsonObject.Add("value", this.Value);
			return jsonObject;
		}

		// Token: 0x04000389 RID: 905
		public Data Data;

		// Token: 0x0400038A RID: 906
		public int Value;
	}
}

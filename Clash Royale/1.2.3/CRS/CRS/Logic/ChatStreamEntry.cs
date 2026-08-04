using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x020000B3 RID: 179
	internal class ChatStreamEntry : StreamEntry
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x000159B0 File Offset: 0x00013BB0
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddString(this.m_vMessage);
			return list.ToArray();
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000159D4 File Offset: 0x00013BD4
		public string GetMessage()
		{
			return this.m_vMessage;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000145DA File Offset: 0x000127DA
		public override int GetStreamEntryType()
		{
			return 2;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000159DC File Offset: 0x00013BDC
		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			this.m_vMessage = jsonObject["message"].ToObject<string>();
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000159FB File Offset: 0x00013BFB
		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("message", this.m_vMessage);
			return jsonObject;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00015A1D File Offset: 0x00013C1D
		public void SetMessage(string message)
		{
			this.m_vMessage = message;
		}

		// Token: 0x04000301 RID: 769
		private string m_vMessage;
	}
}

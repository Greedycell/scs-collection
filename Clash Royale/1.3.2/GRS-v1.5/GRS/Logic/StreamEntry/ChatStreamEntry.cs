using System;
using System.Collections.Generic;
using CRS.Helpers;
using Newtonsoft.Json.Linq;

namespace CRS.Logic.StreamEntry
{
	// Token: 0x020000D9 RID: 217
	internal class ChatStreamEntry : StreamEntry
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x0001E516 File Offset: 0x0001C716
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddString(this.m_vMessage);
			return list.ToArray();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001E53A File Offset: 0x0001C73A
		public string GetMessage()
		{
			return this.m_vMessage;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001E542 File Offset: 0x0001C742
		public override int GetStreamEntryType()
		{
			return 2;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001E545 File Offset: 0x0001C745
		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			this.m_vMessage = jsonObject["message"].ToObject<string>();
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001E564 File Offset: 0x0001C764
		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("message", this.m_vMessage);
			return jsonObject;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001E586 File Offset: 0x0001C786
		public void SetMessage(string message)
		{
			this.m_vMessage = message;
		}

		// Token: 0x040003C0 RID: 960
		private string m_vMessage;
	}
}

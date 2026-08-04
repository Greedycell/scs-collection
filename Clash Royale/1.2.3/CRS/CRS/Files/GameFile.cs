using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UCS.Files
{
	// Token: 0x02000030 RID: 48
	internal class GameFile
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000D570 File Offset: 0x0000B770
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000D578 File Offset: 0x0000B778
		public string file { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000D581 File Offset: 0x0000B781
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000D589 File Offset: 0x0000B789
		public string sha { get; set; }

		// Token: 0x060001BA RID: 442 RVA: 0x0000D592 File Offset: 0x0000B792
		public void Load(JObject jsonObject)
		{
			this.sha = jsonObject["sha"].ToObject<string>();
			this.file = jsonObject["file"].ToObject<string>();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		public string SaveToJson(JObject fingerPrint)
		{
			fingerPrint.Add("sha", this.sha);
			fingerPrint.Add("file", this.file);
			return JsonConvert.SerializeObject(fingerPrint);
		}
	}
}

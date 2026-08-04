using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CRS.Files
{
	// Token: 0x02000008 RID: 8
	internal class GameFile
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002820 File Offset: 0x00000A20
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002828 File Offset: 0x00000A28
		public string file { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002831 File Offset: 0x00000A31
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002839 File Offset: 0x00000A39
		public string sha { get; set; }

		// Token: 0x0600002A RID: 42 RVA: 0x00002842 File Offset: 0x00000A42
		public void Load(JObject jsonObject)
		{
			this.sha = jsonObject["sha"].ToObject<string>();
			this.file = jsonObject["file"].ToObject<string>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002870 File Offset: 0x00000A70
		public string SaveToJson(JObject fingerPrint)
		{
			fingerPrint.Add("sha", this.sha);
			fingerPrint.Add("file", this.file);
			return JsonConvert.SerializeObject(fingerPrint);
		}
	}
}

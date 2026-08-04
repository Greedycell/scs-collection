using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CRS.Files
{
	// Token: 0x02000007 RID: 7
	internal class FingerPrint
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002620 File Offset: 0x00000820
		public FingerPrint(string filePath)
		{
			this.files = new List<GameFile>();
			string text = null;
			if (File.Exists(filePath))
			{
				using (StreamReader streamReader = new StreamReader(filePath))
				{
					text = streamReader.ReadToEnd();
				}
				this.LoadFromJson(text);
				Console.WriteLine("[GRS]    The fingerprint has been loaded");
				return;
			}
			Console.WriteLine("[GRS]    LoadFingerPrint: error! tried to load FingerPrint without file, run gen_patch first");
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002690 File Offset: 0x00000890
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002698 File Offset: 0x00000898
		public List<GameFile> files { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000026A1 File Offset: 0x000008A1
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000026A9 File Offset: 0x000008A9
		public string sha { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000026B2 File Offset: 0x000008B2
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000026BA File Offset: 0x000008BA
		public string version { get; set; }

		// Token: 0x06000024 RID: 36 RVA: 0x000026C4 File Offset: 0x000008C4
		public void LoadFromJson(string jsonString)
		{
			JObject jobject = JObject.Parse(jsonString);
			foreach (JToken jtoken in ((JArray)jobject["files"]))
			{
				JObject jobject2 = (JObject)jtoken;
				GameFile gameFile = new GameFile();
				gameFile.Load(jobject2);
				this.files.Add(gameFile);
			}
			this.sha = jobject["sha"].ToObject<string>();
			this.version = jobject["version"].ToObject<string>();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002768 File Offset: 0x00000968
		public string SaveToJson()
		{
			JObject jobject = new JObject();
			JArray jarray = new JArray();
			foreach (GameFile gameFile in this.files)
			{
				JObject jobject2 = new JObject();
				gameFile.SaveToJson(jobject2);
				jarray.Add(jobject2);
			}
			jobject.Add("files", jarray);
			jobject.Add("sha", this.sha);
			jobject.Add("version", this.version);
			return JsonConvert.SerializeObject(jobject).Replace("/", "\\/");
		}
	}
}

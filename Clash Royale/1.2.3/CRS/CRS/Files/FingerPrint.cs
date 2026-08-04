using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UCS.Files
{
	// Token: 0x0200002F RID: 47
	internal class FingerPrint
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000D370 File Offset: 0x0000B570
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
				Console.WriteLine("[UCS]    ObjectManager: fingerprint loaded");
				return;
			}
			Console.WriteLine("[UCS]    LoadFingerPrint: error! tried to load FingerPrint without file, run gen_patch first");
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000D3E8 File Offset: 0x0000B5E8
		public List<GameFile> files { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000D3F1 File Offset: 0x0000B5F1
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000D3F9 File Offset: 0x0000B5F9
		public string sha { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000D402 File Offset: 0x0000B602
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000D40A File Offset: 0x0000B60A
		public string version { get; set; }

		// Token: 0x060001B4 RID: 436 RVA: 0x0000D414 File Offset: 0x0000B614
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

		// Token: 0x060001B5 RID: 437 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
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

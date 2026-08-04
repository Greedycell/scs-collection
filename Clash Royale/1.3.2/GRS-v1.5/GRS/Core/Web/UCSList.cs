using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading;

namespace CRS.Core.Web
{
	// Token: 0x020000E9 RID: 233
	internal class UCSList
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x00020DB8 File Offset: 0x0001EFB8
		public UCSList()
		{
			if (!string.IsNullOrEmpty(UCSList.APIKey) && UCSList.APIKey.Length == 25)
			{
				UCSList.T = new Thread(new ThreadStart(delegate
				{
					for (;;)
					{
						UCSList.SendData();
						Thread.Sleep(60000);
					}
				}));
				UCSList.T.Start();
				return;
			}
			Console.WriteLine("[GRS]     UCSList API is disabled - Visit www.ultrapowa.xyz for more info.");
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00020E23 File Offset: 0x0001F023
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x00020E2A File Offset: 0x0001F02A
		private static Thread T { get; set; }

		// Token: 0x060005D4 RID: 1492 RVA: 0x00020E32 File Offset: 0x0001F032
		public static int CheckStatus()
		{
			if (false)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00020E3C File Offset: 0x0001F03C
		public static void SendData()
		{
			string text = UCSList.Http.Post(UCSList.UCSPanel, new NameValueCollection
			{
				{
					"ApiKey",
					UCSList.APIKey
				},
				{
					"OnlinePlayers",
					Convert.ToString(ResourcesManager.GetOnlinePlayers().Count)
				},
				{
					"Status",
					Convert.ToString(UCSList.Status)
				}
			}).Remove(0, 1);
			if (text == "OK")
			{
				Console.WriteLine("[GRS]    UCS Sent data successfully.");
				return;
			}
			Console.WriteLine("[GRS]    UCSList Server answer uncorrectly : " + text);
		}

		// Token: 0x040003E7 RID: 999
		private static readonly string APIKey = "";

		// Token: 0x040003E8 RID: 1000
		private static readonly int Status = UCSList.CheckStatus();

		// Token: 0x040003E9 RID: 1001
		private static readonly string UCSPanel = "https://www.ultrapowa.xyz/api/";

		// Token: 0x0200011A RID: 282
		public static class Http
		{
			// Token: 0x0600066E RID: 1646 RVA: 0x00021E68 File Offset: 0x00020068
			public static string Post(string uri, NameValueCollection pairs)
			{
				byte[] array = null;
				using (WebClient webClient = new WebClient())
				{
					array = webClient.UploadValues(uri, pairs);
				}
				return Encoding.UTF8.GetString(array);
			}
		}
	}
}

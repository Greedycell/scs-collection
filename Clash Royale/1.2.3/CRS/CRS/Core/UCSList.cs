using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading;
namespace UCS.Core
{
	// Token: 0x020000BA RID: 186
	internal class UCSList
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x00016D54 File Offset: 0x00014F54
		public UCSList()
		{
			if (!string.IsNullOrEmpty(UCSList.APIKey) && UCSList.APIKey.Length == 25)
			{
				UCSList.T = new Thread(new ThreadStart(delegate
				{
					for (; ; )
					{
						UCSList.SendData();
						Thread.Sleep(60000);
					}
				}));
				UCSList.T.Start();
				return;
			}
			Console.WriteLine("[UCS]     UCSList API is disabled - Visit [www.ultrapowa.xyz](https://www.ultrapowa.xyz) for more info.");
		}
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00016DBF File Offset: 0x00014FBF
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00016DC6 File Offset: 0x00014FC6
		private static Thread T { get; set; }
		// Token: 0x0600057D RID: 1405 RVA: 0x00016DCE File Offset: 0x00014FCE
		public static int CheckStatus()
		{
			if (Convert.ToBoolean(ConfigurationManager.AppSettings["maintenanceMode"]))
			{
				return 2;
			}
			return 1;
		}
		// Token: 0x0600057E RID: 1406 RVA: 0x00016DEC File Offset: 0x00014FEC
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
				Console.WriteLine("[UCS]    UCS Sent data successfully.");
				return;
			}
			Console.WriteLine("[UCS]    UCSList Server answer uncorrectly : " + text);
		}
		// Token: 0x04000322 RID: 802
		private static readonly string APIKey = ConfigurationManager.AppSettings["UCSList - APIKey"];
		// Token: 0x04000323 RID: 803
		private static readonly int Status = UCSList.CheckStatus();
		// Token: 0x04000324 RID: 804
		private static readonly string UCSPanel = "https://www.ultrapowa.xyz/api/";
		// Token: 0x0200012E RID: 302
		public static class Http
		{
			// Token: 0x06000720 RID: 1824 RVA: 0x000188D0 File Offset: 0x00016AD0
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
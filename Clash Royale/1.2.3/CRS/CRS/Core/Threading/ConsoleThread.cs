using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;
using UCS.Helpers;

namespace UCS.Core.Threading
{
	// Token: 0x020000C0 RID: 192
	internal class ConsoleThread
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001829D File Offset: 0x0001649D
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x000182A4 File Offset: 0x000164A4
		private static Thread T { get; set; }

		// Token: 0x060005B2 RID: 1458 RVA: 0x000182AC File Offset: 0x000164AC
		public static void Start()
		{
			ConsoleThread.T = new Thread(new ThreadStart(delegate
			{
				if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"))) Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"));

				Console.Title = "Clash Royale Server v" + Assembly.GetExecutingAssembly().GetName().Version;
				Console.WriteLine("\n             ######  ########      ######  ######## ########  ##     ## ######## ########  \r\n            ##    ## ##     ##    ##    ## ##       ##     ## ##     ## ##       ##     ## \r\n            ##       ##     ##    ##       ##       ##     ## ##     ## ##       ##     ## \r\n            ##       ########      ######  ######   ########  ##     ## ######   ########  \r\n            ##       ##   ##            ## ##       ##   ##    ##   ##  ##       ##   ##   \r\n            ##    ## ##    ##     ##    ## ##       ##    ##    ## ##   ##       ##    ##  \r\n             ######  ##     ##     ######  ######## ##     ##    ###    ######## ##     ## \n                  ");
				Console.WriteLine("[CRS]    -> This Program is made by the CRS Team !");
				Console.WriteLine("[CRS]    -> You can find the source at www.github.com/BerkanYildiz/UCR");
				Console.WriteLine("[CRS]    -> Don't forget to visit our git daily for news update !");
				Console.WriteLine("[CRS]    -> CRS is now starting...");
				Console.WriteLine();
				Debugger.SetLogLevel(int.Parse(ConfigurationManager.AppSettings["loggingLevel"]));
				Logger.SetLogLevel(int.Parse(ConfigurationManager.AppSettings["loggingLevel"]));
				MemoryThread.Start();
				NetworkThread.Start();
				while ((ConsoleThread.Command = Console.ReadLine()) != null)
				{
					CommandParser.Parse(ConsoleThread.Command);
				}
			}));
			ConsoleThread.T.Start();
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000182E1 File Offset: 0x000164E1
		public static void Stop()
		{
			if (ConsoleThread.T.ThreadState == ThreadState.Running)
			{
				ConsoleThread.T.Abort();
			}
		}

		// Token: 0x0400033D RID: 829
		private static string Command;
	}
}

using System;
using System.Threading;

namespace CRS.Core.Threading
{
	// Token: 0x020000E3 RID: 227
	internal class ConsoleThread
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x000206DC File Offset: 0x0001E8DC
		public ConsoleThread()
		{
			new Thread(new ThreadStart(delegate
			{
				Console.Title = "GobelinLand Clash Server v1.5.0 - © 2016 - Players -> " + ConsoleThread.OP;
				Console.WriteLine("\r\n                  ________      ___.          .__  .__       .____                       .___\r\n                 /  _____/  ____\\_ |__   ____ |  | |__| ____ |    |   _____    ____    __| _/\r\n                /   \\  ___ /  _ \\| __ \\_/ __ \\|  | |  |/    \\|    |   \\__  \\  /    \\  / __ | \r\n                \\    \\_\\  (  <_> ) \\_\\ \\  ___/|  |_|  |   |  \\    |___ / __ \\|   |  \\/ /_/ | \r\n                 \\______  /\\____/|___  /\\___  >____/__|___|  /_______ (____  /___|  /\\____ | \r\n                        \\/           \\/     \\/             \\/        \\/    \\/     \\/      \\/ \r\n                ");
				Console.WriteLine("[GRS]    -> This program is edited by the GobelinLand's team.");
				Console.WriteLine("[GRS]    -> Don't forget to visit www.gobelinland.fr daily for the latest news and updates!");
				Console.WriteLine("[GRS]    -> GCS is now starting...");
				Console.WriteLine();
				new MemoryThread();
				new NetworkThread();
			})).Start();
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0002070D File Offset: 0x0001E90D
		public static void TitleU()
		{
			Console.Title = "GobelinLand Royale Server v1.5.0 - © 2016 - Players -> " + (ConsoleThread.OP++ + 1);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00020732 File Offset: 0x0001E932
		public static void TitleD()
		{
			Console.Title = "GobelinLand Royale Server v1.5.0 - © 2016 - Players -> " + (ConsoleThread.OP-- - 1);
		}

		// Token: 0x040003DD RID: 989
		public static int OP;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using CRS.Files;
using CRS.Files.CSV;
using CRS.Files.Logic;
using CRS.Logic;

namespace CRS.Core
{
	// Token: 0x020000DF RID: 223
	internal class ObjectManager : IDisposable
	{
		// Token: 0x06000593 RID: 1427 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		public ObjectManager()
		{
			this.m_vTimerCanceled = false;
			ObjectManager.m_vDatabase = new DatabaseManager();
			ObjectManager.NpcLevels = new Dictionary<int, string>();
			ObjectManager.DataTables = new DataTables();
			ObjectManager.m_vAlliances = new Dictionary<long, Alliance>();
			ObjectManager.FingerPrint = new FingerPrint("Gamefiles/fingerprint.json");
			using (StreamReader streamReader = new StreamReader("Gamefiles/starting_home.json"))
			{
				ObjectManager.m_vHomeDefault = streamReader.ReadToEnd();
			}
			ObjectManager.m_vAvatarSeed = ObjectManager.m_vDatabase.GetMaxPlayerId() + 1L;
			ObjectManager.m_vAllianceSeed = ObjectManager.m_vDatabase.GetMaxAllianceId() + 1L;
			ObjectManager.GetAllAlliancesFromDB();
			Timer timer = new Timer(new TimerCallback(this.Save), null, 30000, 30000);
			this.TimerReference = timer;
			Console.WriteLine("[GRS]    Database Sync started successfully");
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001FD88 File Offset: 0x0001DF88
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0001FD8F File Offset: 0x0001DF8F
		public static DataTables DataTables { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001FD97 File Offset: 0x0001DF97
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0001FD9E File Offset: 0x0001DF9E
		public static FingerPrint FingerPrint { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001FDA6 File Offset: 0x0001DFA6
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x0001FDAD File Offset: 0x0001DFAD
		public static Dictionary<int, string> NpcLevels { get; set; }

		// Token: 0x0600059A RID: 1434 RVA: 0x0001FDB5 File Offset: 0x0001DFB5
		public void Dispose()
		{
			if (this.TimerReference != null)
			{
				this.TimerReference.Dispose();
				this.TimerReference = null;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001FDD1 File Offset: 0x0001DFD1
		private void Save(object state)
		{
			ObjectManager.m_vDatabase.Save(ResourcesManager.GetInMemoryLevels());
			ObjectManager.m_vDatabase.Save(ObjectManager.m_vAlliances.Values.ToList<Alliance>());
			if (this.m_vTimerCanceled)
			{
				this.TimerReference.Dispose();
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001FE10 File Offset: 0x0001E010
		public static Alliance CreateAlliance(long seed)
		{
			DatabaseManager vDatabase = ObjectManager.m_vDatabase;
			Alliance alliance;
			lock (vDatabase)
			{
				if (seed == 0L)
				{
					seed = ObjectManager.m_vAllianceSeed;
				}
				alliance = new Alliance(seed);
				ObjectManager.m_vAllianceSeed += 1L;
			}
			ObjectManager.m_vDatabase.CreateAlliance(alliance);
			ObjectManager.m_vAlliances.Add(alliance.GetAllianceId(), alliance);
			return alliance;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001FE84 File Offset: 0x0001E084
		public static Level CreateAvatar(long seed, string token)
		{
			DatabaseManager vDatabase = ObjectManager.m_vDatabase;
			Level level;
			lock (vDatabase)
			{
				if (seed == 0L)
				{
					seed = ObjectManager.m_vAvatarSeed;
				}
				level = new Level(seed, token);
				ObjectManager.m_vAvatarSeed += 1L;
			}
			level.LoadFromJSON(ObjectManager.m_vHomeDefault);
			ObjectManager.m_vDatabase.CreateAccount(level);
			return level;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001FEF4 File Offset: 0x0001E0F4
		public static void GetAllAlliancesFromDB()
		{
			foreach (Alliance alliance in ObjectManager.m_vDatabase.GetAllAlliances())
			{
				if (!ObjectManager.m_vAlliances.ContainsKey(alliance.GetAllianceId()))
				{
					ObjectManager.m_vAlliances.Add(alliance.GetAllianceId(), alliance);
				}
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001FF68 File Offset: 0x0001E168
		public static Alliance GetAlliance(long allianceId)
		{
			Alliance alliance;
			if (ObjectManager.m_vAlliances.ContainsKey(allianceId))
			{
				alliance = ObjectManager.m_vAlliances[allianceId];
			}
			else
			{
				alliance = ObjectManager.m_vDatabase.GetAlliance(allianceId);
				if (alliance != null)
				{
					ObjectManager.m_vAlliances.Add(alliance.GetAllianceId(), alliance);
				}
			}
			return alliance;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001FFB1 File Offset: 0x0001E1B1
		public static List<Alliance> GetInMemoryAlliances()
		{
			return ObjectManager.m_vAlliances.Values.ToList<Alliance>();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001FFC4 File Offset: 0x0001E1C4
		public static Level GetRandomOnlinePlayer()
		{
			int num = new Random().Next(0, ResourcesManager.GetInMemoryLevels().Count);
			return ResourcesManager.GetInMemoryLevels().ElementAt(num);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001FFF4 File Offset: 0x0001E1F4
		public static Level GetRandomPlayerFromAll()
		{
			int num = new Random().Next(0, ResourcesManager.GetAllPlayerIds().Count);
			return ResourcesManager.GetPlayer(ResourcesManager.GetAllPlayerIds()[num], false);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00020028 File Offset: 0x0001E228
		public static void LoadGameFiles()
		{
			List<Tuple<string, string, int>> list = new List<Tuple<string, string, int>>();
			list.Add(new Tuple<string, string, int>("Achievements", "Gamefiles/logic/achievements.csv", 22));
			list.Add(new Tuple<string, string, int>("Buildings", "Gamefiles/logic/buildings.csv", 0));
			list.Add(new Tuple<string, string, int>("Characters", "Gamefiles/logic/characters.csv", 3));
			list.Add(new Tuple<string, string, int>("Decos", "Gamefiles/logic/decos.csv", 17));
			list.Add(new Tuple<string, string, int>("Experience Levels", "Gamefiles/logic/experience_levels.csv", 10));
			list.Add(new Tuple<string, string, int>("Globals", "Gamefiles/logic/globals.csv", 13));
			list.Add(new Tuple<string, string, int>("Heroes", "Gamefiles/logic/heroes.csv", 27));
			list.Add(new Tuple<string, string, int>("Leagues", "Gamefiles/logic/leagues.csv", 12));
			list.Add(new Tuple<string, string, int>("NPCs", "Gamefiles/logic/npcs.csv", 16));
			list.Add(new Tuple<string, string, int>("Spells", "Gamefiles/logic/spells.csv", 25));
			list.Add(new Tuple<string, string, int>("Townhall Levels", "Gamefiles/logic/townhall_levels.csv", 14));
			list.Add(new Tuple<string, string, int>("Traps", "Gamefiles/logic/traps.csv", 11));
			list.Add(new Tuple<string, string, int>("Resources", "Gamefiles/logic/resources.csv", 2));
			Console.WriteLine("[GRS]    Loading server gamefiles & data...");
			foreach (Tuple<string, string, int> tuple in list)
			{
				Console.Write("             ->  " + tuple.Item1);
				ObjectManager.DataTables.InitDataTable(new CSVTable(tuple.Item2), tuple.Item3);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(" done");
				Console.ResetColor();
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000201EC File Offset: 0x0001E3EC
		public static void LoadNpcLevels()
		{
			Console.Write("\n[GRS]    Loading Npc levels... ");
			for (int i = 0; i < 50; i++)
			{
				using (StreamReader streamReader = new StreamReader("Gamefiles/pve/level" + (i + 1) + ".json"))
				{
					ObjectManager.NpcLevels.Add(i + 17000000, streamReader.ReadToEnd());
				}
			}
			Console.WriteLine("done");
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0002026C File Offset: 0x0001E46C
		public static void RemoveInMemoryAlliance(long id)
		{
			ObjectManager.m_vAlliances.Remove(id);
		}

		// Token: 0x040003CA RID: 970
		private static Dictionary<long, Alliance> m_vAlliances;

		// Token: 0x040003CB RID: 971
		private static long m_vAllianceSeed;

		// Token: 0x040003CC RID: 972
		private static long m_vAvatarSeed;

		// Token: 0x040003CD RID: 973
		public static long m_vDonationSeed;

		// Token: 0x040003CE RID: 974
		private static string[] m_vBannedIPs;

		// Token: 0x040003CF RID: 975
		private static DatabaseManager m_vDatabase;

		// Token: 0x040003D0 RID: 976
		private static string m_vHomeDefault;

		// Token: 0x040003D1 RID: 977
		public bool m_vTimerCanceled;

		// Token: 0x040003D2 RID: 978
		public Timer TimerReference;
	}
}

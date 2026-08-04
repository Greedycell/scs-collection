using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using UCS.Files;
using UCS.Logic;

namespace UCS.Core
{
	// Token: 0x020000BF RID: 191
	internal class ObjectManager : IDisposable
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00017E48 File Offset: 0x00016048
		public ObjectManager()
		{
			this.m_vTimerCanceled = false;
			ObjectManager.m_vDatabase = new DatabaseManager();
			ObjectManager.NpcLevels = new Dictionary<int, string>();
			ObjectManager.DataTables = new DataTables();
			ObjectManager.m_vAlliances = new Dictionary<long, Alliance>();
			ObjectManager.LoadFingerPrint();
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string startingHomePath = Path.Combine(baseDirectory, "Gamefiles", "starting_home.json");
			using (StreamReader streamReader = new StreamReader(startingHomePath))
			{
				ObjectManager.m_vHomeDefault = streamReader.ReadToEnd();
			}
			ObjectManager.m_vAvatarSeed = ObjectManager.m_vDatabase.GetMaxPlayerId() + 1L;
			ObjectManager.m_vAllianceSeed = ObjectManager.m_vDatabase.GetMaxAllianceId() + 1L;
			ObjectManager.LoadGameFiles();
			ObjectManager.GetAllAlliancesFromDB();
			global::System.Threading.Timer timer = new global::System.Threading.Timer(new TimerCallback(this.Save), null, 30000, 15000);
			this.TimerReference = timer;
			Console.WriteLine("[CRS]    Database Sync started successfully");
			ObjectManager.m_vRandomSeed = new Random();
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00017F28 File Offset: 0x00016128
		private void Save(object state)
		{
			ObjectManager.m_vDatabase.Save(ResourcesManager.GetInMemoryLevels());
			ObjectManager.m_vDatabase.Save(ObjectManager.m_vAlliances.Values.ToList<Alliance>());
			if (this.m_vTimerCanceled)
			{
				this.TimerReference.Dispose();
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00017F68 File Offset: 0x00016168
		public static Alliance CreateAlliance(long seed)
		{
			object vDatabaseLock = ObjectManager.m_vDatabaseLock;
			Alliance alliance;
			lock (vDatabaseLock)
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

		// Token: 0x0600059F RID: 1439 RVA: 0x00017FDC File Offset: 0x000161DC
		public static Level CreateAvatar(long seed)
		{
			object vDatabaseLock = ObjectManager.m_vDatabaseLock;
			Level level;
			lock (vDatabaseLock)
			{
				if (seed == 0L)
				{
					seed = ObjectManager.m_vAvatarSeed;
				}
				level = new Level(seed);
				ObjectManager.m_vAvatarSeed += 1L;
			}
			level.LoadFromJSON(ObjectManager.m_vHomeDefault);
			ObjectManager.m_vDatabase.CreateAccount(level);
			return level;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001804C File Offset: 0x0001624C
		public void Dispose()
		{
			if (this.TimerReference != null)
			{
				this.TimerReference.Dispose();
				this.TimerReference = null;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00018068 File Offset: 0x00016268
		public static void GetAllAlliancesFromDB()
		{
			for (int i = 0; i < ObjectManager.m_vDatabase.GetAllAlliances().Count; i++)
			{
				if (!ObjectManager.m_vAlliances.ContainsKey(ObjectManager.m_vDatabase.GetAllAlliances()[i].GetAllianceId()))
				{
					ObjectManager.m_vAlliances.Add(ObjectManager.m_vDatabase.GetAllAlliances()[i].GetAllianceId(), ObjectManager.m_vDatabase.GetAllAlliances()[i]);
				}
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000180E0 File Offset: 0x000162E0
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

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001812B File Offset: 0x0001632B
		public static List<Alliance> GetInMemoryAlliances()
		{
			List<Alliance> list = new List<Alliance>();
			list.AddRange(ObjectManager.m_vAlliances.Values);
			return list;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00018144 File Offset: 0x00016344
		public static Level GetRandomOnlinePlayer()
		{
			int num = ObjectManager.m_vRandomSeed.Next(0, ResourcesManager.GetInMemoryLevels().Count);
			return ResourcesManager.GetInMemoryLevels().ElementAt(num);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00018174 File Offset: 0x00016374
		public static Level GetRandomPlayerFromAll()
		{
			int num = ObjectManager.m_vRandomSeed.Next(0, ResourcesManager.GetAllPlayerIds().Count);
			return ResourcesManager.GetPlayer(ResourcesManager.GetAllPlayerIds()[num], false);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000181A8 File Offset: 0x000163A8
		public static void LoadFingerPrint()
		{
			if (Convert.ToBoolean(ConfigurationManager.AppSettings["useCustomPatch"]))
			{
				string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
				string fingerprintPath = Path.Combine(baseDirectory, "Gamefiles", "fingerprint.json");
				ObjectManager.FingerPrint = new FingerPrint(fingerprintPath);
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000181D0 File Offset: 0x000163D0
		public static void LoadGameFiles()
		{
			List<Tuple<string, string, int>> list = new List<Tuple<string, string, int>>();
			Console.WriteLine("[CRS]    Loading server gamefiles & data...");
			for (int i = 0; i < list.Count; i++)
			{
				Console.Write("             ->  " + list[i].Item1);
				ObjectManager.DataTables.InitDataTable(new CSVTable(list[i].Item2), list[i].Item3);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(" done");
				Console.ResetColor();
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00018256 File Offset: 0x00016456
		public static void RemoveInMemoryAlliance(long id)
		{
			ObjectManager.m_vAlliances.Remove(id);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00018264 File Offset: 0x00016464
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x0001826B File Offset: 0x0001646B
		public static DataTables DataTables { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00018273 File Offset: 0x00016473
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x0001827A File Offset: 0x0001647A
		public static FingerPrint FingerPrint { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00018282 File Offset: 0x00016482
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00018289 File Offset: 0x00016489
		public static Dictionary<int, string> NpcLevels { get; set; }

		// Token: 0x04000331 RID: 817
		private static Dictionary<long, Alliance> m_vAlliances;

		// Token: 0x04000332 RID: 818
		private static long m_vAllianceSeed;

		// Token: 0x04000333 RID: 819
		private static long m_vAvatarSeed;

		// Token: 0x04000334 RID: 820
		private static DatabaseManager m_vDatabase;

		// Token: 0x04000335 RID: 821
		private static string m_vHomeDefault;

		// Token: 0x04000336 RID: 822
		private static Random m_vRandomSeed;

		// Token: 0x04000337 RID: 823
		public bool m_vTimerCanceled;

		// Token: 0x04000338 RID: 824
		public global::System.Threading.Timer TimerReference;

		// Token: 0x0400033C RID: 828
		private static readonly object m_vDatabaseLock = new object();
	}
}

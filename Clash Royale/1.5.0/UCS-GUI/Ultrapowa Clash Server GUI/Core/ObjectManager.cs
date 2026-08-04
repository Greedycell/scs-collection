namespace CRS.Core
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using GameFiles;

    using Logic;

    using Settings;

    #endregion

    internal class ObjectManager : IDisposable
    {
        // public static Dictionary<int,string> NpcLevels {get;set;}
        internal static readonly List<Tuple<string, string, int>> GameFiles = new List<Tuple<string, string, int>>();

        private static readonly object m_vDatabaseLock = new object();

        private static Dictionary<long, Clan> m_vAlliances;

        private static long m_vAllianceSeed;

        private static long m_vAvatarSeed;

        private static DatabaseManager m_vDatabase;

        private static string m_vHomeDefault;

        private static Random m_vRandomSeed;

        private bool m_vTimerCanceled;

        private Timer TimerReference;

        // private static ConcurrentDictionary<long, Level> m_vInMemoryPlayers { get; set; }
        public ObjectManager()
        {
            this.m_vTimerCanceled = false;
            m_vDatabase = new DatabaseManager();

            // NpcLevels = new Dictionary<int, string>();
            DataTables = new DataTables();
            m_vAlliances = new Dictionary<long, Clan>();

            if (Convert.ToBoolean(Constants.Patching))
            {
                LoadFingerPrint();
            }

            using (StreamReader sr = new StreamReader(@"gamefiles/level/starting_home_backup.json"))
            {
                m_vHomeDefault = sr.ReadToEnd();
            }

            m_vAvatarSeed = m_vDatabase.GetMaxPlayerId() + 1;
            m_vAllianceSeed = m_vDatabase.GetMaxAllianceId() + 1;
            LoadGameFiles();

            // LoadNpcLevels();
            TimerCallback TimerDelegate = new TimerCallback(this.Save);
            Timer TimerItem = new Timer(TimerDelegate, null, 60000, 60000);
            this.TimerReference = TimerItem;

            Console.WriteLine("Database Sync started");
            m_vRandomSeed = new Random();
        }

        ////Todo Cleanup
        ////Remove disc clients
        ////Remove InMemoryPlayers after a certain time

        ////public static ConcurrentDictionary<Client, Level> OnlinePlayers { get; set; }
        ////public static ConcurrentDictionary<Level, Client> OnlineClients { get; set; }
        ////public static ConcurrentDictionary<Socket, Client> Clients { get; set; }
        public static DataTables DataTables { get; set; }

        public static FingerPrint FingerPrint { get; set; }

        public static Clan CreateAlliance(long seed)
        {
            Clan clan;
            lock (m_vDatabaseLock)
            {
                if (seed == 0) seed = m_vAllianceSeed;
                clan = new Clan(seed);
                m_vAllianceSeed++;
            }

            m_vDatabase.CreateAlliance(clan);
            m_vAlliances.Add(clan.GetAllianceId(), clan);
            return clan;
        }

        public static Level CreateAvatar(long seed, string token)
        {
            Level pl;
            if (seed == 0)
            {
                seed = m_vAvatarSeed;
            }
            pl = new Level(seed, token);
            m_vAvatarSeed++;
            pl.LoadFromJSON(m_vHomeDefault);
            m_vDatabase.CreateAccount(pl);
            return pl;
        }

        public static Clan GetAlliance(long allianceId)
        {
            Clan alliance = null;
            if (m_vAlliances.ContainsKey(allianceId))
            {
                alliance = m_vAlliances[allianceId];
            }
            else
            {
                alliance = m_vDatabase.GetAlliance(allianceId);
                if (alliance != null)
                {
                    m_vAlliances.Add(alliance.GetAllianceId(), alliance);
                }
            }

            return alliance;
        }

        public static List<Clan> GetInMemoryAlliances()
        {
            List<Clan> alliances = new List<Clan>();
            alliances.AddRange(m_vAlliances.Values);
            return alliances;
        }
        public static void RemoveInMemoryAlliance(long id)
        {
            m_vAlliances.Remove(id);
        }
        public static Level GetRandomPlayer()
        {
            int index = m_vRandomSeed.Next(0, ResourcesManager.GetInMemoryLevels().Count); // accès concurrent KO
            return ResourcesManager.GetInMemoryLevels().ElementAt(index);
        }

        public static void LoadFingerPrint()
        {
            FingerPrint = new FingerPrint(@"gamefiles/fingerprint.json");
        }

        // public static void LoadNpcLevels()
        // {
        // Console.WriteLine("");
        // Console.Write("Loading Npc levels... ");
        // for(int i=0;i<50;i++)
        // {
        // using(StreamReader sr = new StreamReader(@"gamefiles/pve/level" + ( i + 1) + ".json"))
        // {
        // NpcLevels.Add(i, sr.ReadToEnd());
        // }
        // }
        // Console.WriteLine("done");

        // }
        public static void LoadGameFiles()
        {
            // List<Tuple<string, string, int>> gameFiles = new List<Tuple<string, string, int>>();
            GameFiles.Add(new Tuple<string, string, int>("Achievements", @"Gamefiles/csv_logic/achievements.csv", 1));
            GameFiles.Add(new Tuple<string, string, int>("Alliance Badges", @"Gamefiles/csv_logic/alliance_badges.csv",
                                                         2));
            GameFiles.Add(new Tuple<string, string, int>("Alliance Roles", @"Gamefiles/csv_logic/alliance_roles.csv", 3));
            GameFiles.Add(new Tuple<string, string, int>("Area Effect Objects",
                                                         @"Gamefiles/csv_logic/area_effect_objects.csv", 4));
            GameFiles.Add(new Tuple<string, string, int>("Arenas", @"Gamefiles/csv_logic/arenas.csv", 5));
            GameFiles.Add(new Tuple<string, string, int>("Buildings", @"Gamefiles/csv_logic/buildings.csv", 6));
            GameFiles.Add(new Tuple<string, string, int>("Character Buffs", @"Gamefiles/csv_logic/character_buffs.csv",
                                                         7));
            GameFiles.Add(new Tuple<string, string, int>("Characters", @"Gamefiles/csv_logic/characters.csv", 8));
            GameFiles.Add(new Tuple<string, string, int>("Chest Order", @"Gamefiles/csv_logic/chest_order.csv", 9));
            GameFiles.Add(new Tuple<string, string, int>("Content Tests", @"Gamefiles/csv_logic/content_tests.csv", 10));
            GameFiles.Add(new Tuple<string, string, int>("Damage Types", @"Gamefiles/csv_logic/damage_types.csv", 11));
            GameFiles.Add(new Tuple<string, string, int>("Decos", @"Gamefiles/csv_logic/decos.csv", 12));
            GameFiles.Add(new Tuple<string, string, int>("Exp Levels", @"Gamefiles/csv_logic/exp_levels.csv", 13));
            GameFiles.Add(new Tuple<string, string, int>("Gamble Chests", @"Gamefiles/csv_logic/gamble_chests.csv", 14));
            GameFiles.Add(new Tuple<string, string, int>("Globals", @"Gamefiles/csv_logic/globals.csv", 15));
            GameFiles.Add(new Tuple<string, string, int>("Locales", @"Gamefiles/csv_logic/locales.csv", 16));
            GameFiles.Add(new Tuple<string, string, int>("Locations", @"Gamefiles/csv_logic/locations.csv", 17));
            GameFiles.Add(new Tuple<string, string, int>("NPCs", @"Gamefiles/csv_logic/npcs.csv", 18));
            GameFiles.Add(new Tuple<string, string, int>("Predefined Decks", @"Gamefiles/csv_logic/predefined_decks.csv",
                                                         19));
            GameFiles.Add(new Tuple<string, string, int>("Projectiles", @"Gamefiles/csv_logic/projectiles.csv", 20));
            GameFiles.Add(new Tuple<string, string, int>("Rarities", @"Gamefiles/csv_logic/rarities.csv", 21));
            GameFiles.Add(new Tuple<string, string, int>("Regions", @"Gamefiles/csv_logic/regions.csv", 22));
            GameFiles.Add(new Tuple<string, string, int>("Resource Packs", @"Gamefiles/csv_logic/resource_packs.csv", 23));
            GameFiles.Add(new Tuple<string, string, int>("Resources", @"Gamefiles/csv_logic/resources.csv", 24));
            GameFiles.Add(new Tuple<string, string, int>("Shop", @"Gamefiles/csv_logic/shop.csv", 25));
            GameFiles.Add(new Tuple<string, string, int>("Spawn Points", @"Gamefiles/csv_logic/spawn_points.csv", 26));
            GameFiles.Add(new Tuple<string, string, int>("Spells Sets", @"Gamefiles/csv_logic/spell_sets.csv", 27));
            GameFiles.Add(new Tuple<string, string, int>("Spells Buildings", @"Gamefiles/csv_logic/spells_buildings.csv",
                                                         28));
            GameFiles.Add(new Tuple<string, string, int>("Spells Characters",
                                                         @"Gamefiles/csv_logic/spells_characters.csv", 29));
            GameFiles.Add(new Tuple<string, string, int>("Spells Other", @"Gamefiles/csv_logic/spells_other.csv", 30));
            GameFiles.Add(new Tuple<string, string, int>("Survival Modes", @"Gamefiles/csv_logic/survival_modes.csv", 31));
            GameFiles.Add(new Tuple<string, string, int>("Taunts", @"Gamefiles/csv_logic/taunts.csv", 32));
            GameFiles.Add(new Tuple<string, string, int>("Tournament Tiers", @"Gamefiles/csv_logic/tournament_tiers.csv",
                                                         33));
            GameFiles.Add(new Tuple<string, string, int>("Treasure Chests", @"Gamefiles/csv_logic/treasure_chests.csv",
                                                         34));
            GameFiles.Add(new Tuple<string, string, int>("Tutorials Home", @"Gamefiles/csv_logic/tutorials_home.csv", 35));
            GameFiles.Add(new Tuple<string, string, int>("Tutorials Npc", @"Gamefiles/csv_logic/tutorials_npc.csv", 36));
            DataTables = new DataTables();
            Console.WriteLine("Loading server data...");
            foreach (var data in GameFiles)
            {
                Console.Write("\t" + data.Item1);
                DataTables.InitDataTable(new CSVTable(data.Item2), data.Item3);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" done");
                Console.ResetColor();
            }
        }

        public void Dispose()
        {
            GameFiles.Clear();
            DataTables.Dispose();
        }

        private void Save(object state)
        {
            m_vDatabase.Save(ResourcesManager.GetInMemoryLevels());
            m_vDatabase.Save(m_vAlliances.Values.ToList());
            if (this.m_vTimerCanceled)
            {
                this.TimerReference.Dispose();
            }
        }
    }
}
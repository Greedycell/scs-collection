namespace UCS.Files
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using UCS.Files.CSV_Reader;

    #endregion Usings

    /// <summary>
    /// This class is used to load, read and write Gamefiles.
    /// </summary>
    internal class CSV : IDisposable
    {
        /// <summary>
        /// The list of gamefiles, which can be load, and their path.
        /// </summary>
        public static readonly List<Tuple<string, string, int>> Gamefiles   = new List<Tuple<string, string, int>>();

        /// <summary>
        /// An array of initialized and readed .csv.
        /// </summary>
        public static Gamefiles Tables = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSV"/> class.
        /// </summary>
        public CSV()
        {
            Gamefiles.Add(new Tuple<string, string, int>("Achievements", @"Gamefiles/csv_logic/achievements.csv", 1));
            Gamefiles.Add(new Tuple<string, string, int>("Alliance Badges", @"Gamefiles/csv_logic/alliance_badges.csv", 2));
            Gamefiles.Add(new Tuple<string, string, int>("Alliance Roles", @"Gamefiles/csv_logic/alliance_roles.csv", 3));
            Gamefiles.Add(new Tuple<string, string, int>("Area Effect Objects", @"Gamefiles/csv_logic/area_effect_objects.csv", 4));
            Gamefiles.Add(new Tuple<string, string, int>("Arenas", @"Gamefiles/csv_logic/arenas.csv", 5));
            Gamefiles.Add(new Tuple<string, string, int>("Buildings", @"Gamefiles/csv_logic/buildings.csv", 6));
            Gamefiles.Add(new Tuple<string, string, int>("Character Buffs", @"Gamefiles/csv_logic/character_buffs.csv", 7));
            Gamefiles.Add(new Tuple<string, string, int>("Characters", @"Gamefiles/csv_logic/characters.csv", 8));
            Gamefiles.Add(new Tuple<string, string, int>("Chest Order", @"Gamefiles/csv_logic/chest_order.csv", 9));

            Gamefiles.Add(new Tuple<string, string, int>("Content Tests", @"Gamefiles/csv_logic/content_tests.csv", 10));
            Gamefiles.Add(new Tuple<string, string, int>("Damage Types", @"Gamefiles/csv_logic/damage_types.csv", 11));
            Gamefiles.Add(new Tuple<string, string, int>("Decos", @"Gamefiles/csv_logic/decos.csv", 12));
            Gamefiles.Add(new Tuple<string, string, int>("Exp Levels", @"Gamefiles/csv_logic/exp_levels.csv", 13));
            Gamefiles.Add(new Tuple<string, string, int>("Gamble Chests", @"Gamefiles/csv_logic/gamble_chests.csv", 14));
            Gamefiles.Add(new Tuple<string, string, int>("Globals", @"Gamefiles/csv_logic/globals.csv", 15));
            Gamefiles.Add(new Tuple<string, string, int>("Locales", @"Gamefiles/csv_logic/locales.csv", 16));
            Gamefiles.Add(new Tuple<string, string, int>("Locations", @"Gamefiles/csv_logic/locations.csv", 17));
            Gamefiles.Add(new Tuple<string, string, int>("NPCs", @"Gamefiles/csv_logic/npcs.csv", 18));
            Gamefiles.Add(new Tuple<string, string, int>("Predefined Decks", @"Gamefiles/csv_logic/predefined_decks.csv", 19));

            Gamefiles.Add(new Tuple<string, string, int>("Projectiles", @"Gamefiles/csv_logic/projectiles.csv", 20));
            Gamefiles.Add(new Tuple<string, string, int>("Rarities", @"Gamefiles/csv_logic/rarities.csv", 21));
            Gamefiles.Add(new Tuple<string, string, int>("Regions", @"Gamefiles/csv_logic/regions.csv", 22));
            Gamefiles.Add(new Tuple<string, string, int>("Resource Packs", @"Gamefiles/csv_logic/resource_packs.csv", 23));
            Gamefiles.Add(new Tuple<string, string, int>("Resources", @"Gamefiles/csv_logic/resources.csv", 24));
            Gamefiles.Add(new Tuple<string, string, int>("Shop", @"Gamefiles/csv_logic/shop.csv", 25));
            Gamefiles.Add(new Tuple<string, string, int>("Spawn Points", @"Gamefiles/csv_logic/spawn_points.csv", 26));
            Gamefiles.Add(new Tuple<string, string, int>("Spells Sets", @"Gamefiles/csv_logic/spell_sets.csv", 27));
            Gamefiles.Add(new Tuple<string, string, int>("Spells Buildings", @"Gamefiles/csv_logic/spells_buildings.csv", 28));
            Gamefiles.Add(new Tuple<string, string, int>("Spells Characters", @"Gamefiles/csv_logic/spells_characters.csv", 29));

            Gamefiles.Add(new Tuple<string, string, int>("Spells Other", @"Gamefiles/csv_logic/spells_other.csv", 30));
            Gamefiles.Add(new Tuple<string, string, int>("Survival Modes", @"Gamefiles/csv_logic/survival_modes.csv", 31));
            Gamefiles.Add(new Tuple<string, string, int>("Taunts", @"Gamefiles/csv_logic/taunts.csv", 32));
            Gamefiles.Add(new Tuple<string, string, int>("Tournament Tiers", @"Gamefiles/csv_logic/tournament_tiers.csv", 33));
            Gamefiles.Add(new Tuple<string, string, int>("Treasure Chests", @"Gamefiles/csv_logic/treasure_chests.csv", 34));
            Gamefiles.Add(new Tuple<string, string, int>("Tutorials Home", @"Gamefiles/csv_logic/tutorials_home.csv", 35));
            Gamefiles.Add(new Tuple<string, string, int>("Tutorials Npc", @"Gamefiles/csv_logic/tutorials_npc.csv", 36));

            Tables = new Gamefiles();

            Console.WriteLine("Gamefiles..");

            Parallel.ForEach(Gamefiles, _File =>
            {
                if (_File.Item3 < 10)
                {
                    Console.WriteLine("    -> " + _File.Item1 + ".");
                }
                
                Tables.Initialize(new Table(_File.Item2), _File.Item3);
            });

            Console.WriteLine("    -> " + (Gamefiles.Count - 10) + " more...");
            Console.WriteLine();
            Console.WriteLine(Gamefiles.Count + " CSV Files, loaded and stored in memory.\n");
        }

        public void Dispose()
        {
            Gamefiles.Clear();
            Tables.Dispose();
        }
    }
}
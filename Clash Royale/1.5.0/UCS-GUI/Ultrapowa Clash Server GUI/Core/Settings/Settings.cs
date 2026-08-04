namespace CRS.Core.Settings
{
    internal class Settings
    {
        public static bool Maintenance { get; set; } = false;

        public static int MaintenanceDuration { get; set; } = 60;

        public static bool ShuttingDown { get; set; } = false;

        public static bool Debug { get; set; } = true;

        public static int StartingGems { get; set; } = 50000;

        public static int StartingGold { get; set; } = 20000;

        public static int StartingCards { get; set; } = 0;

        public static int StartingTrophies { get; set; } = 0;

        public static int StartingLevel { get; set; } = 1;

        public static int StartingExperience { get; set; } = 0;
    }
}
namespace CRS.Core.Settings {
    /// <summary>
    /// <see cref="Constants"/> class is a class containing constants variables, used to customize
    /// the program.
    /// </summary>
    internal class Constants {
        public static readonly string[] AuthorizedIP ={
            "178.33.6.244",
            "178.33.6.245",
            "178.33.6.246",
            "178.33.6.247",
            "178.32.9.216",
            "149.202.90.189"
        };

        public static readonly string[] Hosts ={
            "http://b46f744d64acd2191eda-3720c0374d47e9a0dd52be4d281c260f.r11.cf2.rackcdn.com/", // CoC
            "http://df70a89d32075567ba62-1e50fe9ed7ef652688e6e5fff773074c.r40.cf1.rackcdn.com/", // BB
            "http://7166046b142482e67b30-2a63f4436c967aa7d355061bd0d924a1.r65.cf1.rackcdn.com/", // CR
            "http://cc59c497466f75d53319-2fdc8ba9850fc65de50e260bf74507bd.r73.cf2.rackcdn.com/" // HD
        };

        public static readonly string[] SSLHosts ={
            "https://b46f744d64acd2191eda-3720c0374d47e9a0dd52be4d281c260f.ssl.cf2.rackcdn.com/", // CoC
            "https://df70a89d32075567ba62-1e50fe9ed7ef652688e6e5fff773074c.ssl.cf1.rackcdn.com/", // BB
            "https://99faf1e355c749a9a049-2a63f4436c967aa7d355061bd0d924a1.ssl.cf1.rackcdn.com/", // CR
            "https://cc59c497466f75d53319-2fdc8ba9850fc65de50e260bf74507bd.ssl.cf2.rackcdn.com/" // HD
        };

        internal static string Application = "ClashRoyaleSpain";

        internal static double CheckerInterval = 60000;

        internal const int MemoryInterval = 1000;

        internal static bool Local = false;

        internal static int MaxBuffer = 2048;

        internal static int MaxDevices = 1000;

        internal static int MaxPlayers = 1000;

        internal static int MaxDepth = 10;

        internal static bool Patching = true;

        internal static string PatchURL = "https://ClashRoyaleSpain.es/patch/";

        internal static string Sha = "991671286d34b4eee6d75ec2ea56863dd245c318";

        internal static string Product = "[CRS]";

        internal static string ServerAddr = "178.32.9.216";

        internal static string RedisAddr = "192.168.1.128";

        internal static int ServerPort = 9339;

        internal static int kHostConnectionBacklog = 30;

        internal static int RedisPort = 6379;

        internal static int ServerWebPort = 80;
    }
}
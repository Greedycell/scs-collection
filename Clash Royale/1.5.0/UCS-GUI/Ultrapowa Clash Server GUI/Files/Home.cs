namespace CRS.Files
{
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    internal class Home
    {
        /// <summary>
        /// The starting home JSON string.
        /// </summary>
        public static string Starting_Home = string.Empty;
        

        /// <summary>
        /// Initialize a new instance of the <see cref="Home"/> class.
        /// </summary>
        public Home()
        {
            if (Directory.Exists("Gamefiles/level/"))
            {
                if (File.Exists("Gamefiles/level/starting_home_backup.json"))
                {
                    Starting_Home = File.ReadAllText("Gamefiles/level/starting_home_backup.json", Encoding.UTF8);
                    Starting_Home = Regex.Replace(Starting_Home, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
                }
            }
        }
    }
}
namespace CRS.Core
{
    using System;
    using System.IO;

    internal class Launcher
    {
        public Launcher()
        {
            this.Folders();
            this.Files();
        }

        public void Folders()
        {
            if (!Directory.Exists("Gamefiles"))
            {
                Directory.CreateDirectory("Gamefiles");
            }

            if (!Directory.Exists("Library"))
            {
                Directory.CreateDirectory("Library");
            }

            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }

            if (!Directory.Exists("Patch"))
            {
                Directory.CreateDirectory("Patch");
            }

            if (!Directory.Exists("Utilities"))
            {
                Directory.CreateDirectory("Utilities");
            }
        }

        public void Files()
        {
            if (!File.Exists("Gamefiles/fingerprint.json"))
            {
                Console.WriteLine("    - The Fingerprint JSON files don't exist.");
            }

            if (Directory.GetFiles("Library").Length == 0)
            {
                Console.WriteLine("    - The Library folder is empty.");
            }
        }
    }
}
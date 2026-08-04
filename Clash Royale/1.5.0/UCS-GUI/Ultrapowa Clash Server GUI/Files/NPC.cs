namespace CRS.Files
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;

    #endregion Usings

    internal class NPC : IDisposable
    {
        /// <summary>
        /// A Dictionary of levels which can be loaded and used.
        /// </summary>
        private readonly Dictionary<int, string> Levels = new Dictionary<int, string>();

        /// <summary>
        /// Initialize a new instance of the <see cref="NPC"/> class.
        /// </summary>
        public NPC()
        {
            string[] Files = Directory.GetFiles(@"Gamefiles\level\", "npc*.json");

            for (int _Index = 0; _Index < Files.Length; _Index++)
            {
                this.Levels.Add(_Index, File.ReadAllText(Files[_Index]));
            }

            Console.WriteLine(Files.Length + " NPC Files, loaded and stored in memory.\n");
        }

        public void Dispose()
        {
            this.Levels.Clear();
        }
    }
}
#region

using System;
using System.IO;
using Newtonsoft.Json.Linq;

#endregion

namespace UCS.Files
{
    /// <summary>
    ///     This class is loading and reading the fingerprint file, available in Gamefiles/ folder.
    /// </summary>
    public class Fingerprint : IDisposable
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="Fingerprint" /> class.
        /// </summary>
        public Fingerprint()
        {
            Version = new string[3];
            Load();
        }

        /// <summary>
        ///     Gets or sets the fingerprint json.
        /// </summary>
        /// <value>The Fingerprint in raw.</value>
        public string Json { get; set; }

        /// <summary>
        ///     Gets or sets the fingerprint SHA.
        /// </summary>
        /// <value>The fingerprint SHA.</value>
        public string Sha { get; set; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>The version of the Fingerprint.</value>
        public string[] Version { get; set; }

        public void Dispose()
        {
            Json = null;
            Sha = null;
            Version = null;
        }

        /// <summary>
        ///     Load this instance.
        /// </summary>
        public void Load()
        {
            try
            {
                if (File.Exists(@"Gamefiles\fingerprint.json"))
                {
                    Json = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Gamefiles\fingerprint.json");
                    JObject _Json = JObject.Parse(Json);
                    Sha = _Json["sha"].ToObject<string>();
                    string _Version = _Json["version"].ToObject<string>();
                    Version = _Version.Split('.');
                }
                else
                {
                    Console.WriteLine("The Fingerprint cannot be loaded, the file does not exist.", ConsoleColor.Red);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured while parsing the fingerprint.", ConsoleColor.Red);
            }
        }
    }
}
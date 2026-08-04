namespace CRS.GameFiles
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;

    using CRS.Core;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    #endregion

    internal class FingerPrint
    {
        public FingerPrint(string filePath)
        {
            this.files = new List<GameFile>();
            string fpstring = null;

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    fpstring = sr.ReadToEnd();
                }

                this.LoadFromJson(fpstring);
                Console.WriteLine("ObjectManager: fingerprint loaded");
            }
            else
            {
                Debug.Write("LoadFingerPrint: error! tried to load FingerPrint without file, run gen_patch first");
            }
        }

        public List<GameFile> files { get; set; }

        public string sha { get; set; }

        public static string version { get; set; }

        public void LoadFromJson(string jsonString)
        {
            JObject jsonObject = JObject.Parse(jsonString);

            JArray jsonFilesArray = (JArray)jsonObject["files"];
            foreach (JObject jsonFile in jsonFilesArray)
            {
                GameFile gf = new GameFile();
                gf.Load(jsonFile);
                this.files.Add(gf);
            }

            this.sha = jsonObject["sha"].ToObject<string>();
            version = jsonObject["version"].ToObject<string>();
        }

        public string SaveToJson()
        {
            JObject jsonData = new JObject();

            JArray jsonFilesArray = new JArray();
            foreach (var file in this.files)
            {
                JObject jsonObject = new JObject();
                file.SaveToJson(jsonObject);
                jsonFilesArray.Add(jsonObject);
            }

            jsonData.Add("files", jsonFilesArray);
            jsonData.Add("sha", this.sha);
            jsonData.Add("version", version);

            return JsonConvert.SerializeObject(jsonData).Replace("/", @"\/");
        }
    }

    internal class GameFile
    {
        public string sha { get; set; }

        public string file { get; set; }

        public void Load(JObject jsonObject)
        {
            this.sha = jsonObject["sha"].ToObject<string>();
            this.file = jsonObject["file"].ToObject<string>();
        }

        public string SaveToJson(JObject fingerPrint)
        {
            fingerPrint.Add("sha", this.sha);
            fingerPrint.Add("file", this.file);

            return JsonConvert.SerializeObject(fingerPrint);
        }
    }
}
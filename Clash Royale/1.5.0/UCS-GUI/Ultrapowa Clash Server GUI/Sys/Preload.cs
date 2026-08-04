namespace CRS.Sys
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Windows;

    using SplashScreen = CRS.UI.SplashScreen;

    #endregion

    internal class Preload
    {
        private List<string> GameListFiles;

        private List<string> CoreFiles;

        private List<string> MissingNotReqFiles = new List<string>();

        private double localvalue;

        private double Inc;

        private string MonoLine;

        private string LogDir = Directory.GetCurrentDirectory() + @"\logs";

        public void PreloadThings()
        {
            if (ConfUCS.IsPreloaded) return;

            this.InitializeFileList();

            double minicounter = 0;

            if (ConfUCS.IsConsoleMode) Console.Write("Checking gamefiles... ");
            if(!Directory.Exists(this.LogDir)) Directory.CreateDirectory(this.LogDir);
            if (!ConfUCS.IsConsoleMode)
                SplashScreen.SS.Dispatcher.BeginInvoke(
                    (Action)delegate { SplashScreen.SS.label_txt.Content = "Verifying game files... "; });

            // Verify GameFiles
            foreach (string DataG in this.GameListFiles)
            {
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\" + DataG))
                {
                    this.MissingNotReqFiles.Add(DataG);
                }

                minicounter++;
                this.localvalue = minicounter / this.GameListFiles.Count;

                if (!ConfUCS.IsConsoleMode)
                    SplashScreen.SS.Dispatcher.BeginInvoke(
                        (Action)delegate { SplashScreen.SS.PB_Loader.Value = this.Inc + (50 * this.localvalue); });
            }

            minicounter = 0;
            this.Inc = 50;

            if (!ConfUCS.IsConsoleMode)
                SplashScreen.SS.Dispatcher.BeginInvoke(
                    (Action)delegate { SplashScreen.SS.label_txt.Content = "Verifying required data... "; });

            if (this.MissingNotReqFiles.Count != 0)
            {
                foreach (string FilesMiss in this.MissingNotReqFiles)
                {
                    this.MonoLine += FilesMiss + " || ";
                }

                string IsMoreThanOne = this.MissingNotReqFiles.Count == 1
                                           ? "There is a missing gamefile: "
                                           : "There are a missing gamefiles from the directory " + @""""
                                             + Directory.GetCurrentDirectory() + @"\ ";
                MessageBox.Show(
                    IsMoreThanOne + this.MonoLine,
                    "Missing Gamefile",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("OK!\n");
            Console.ResetColor();

            if (ConfUCS.IsConsoleMode) Console.Write("Checking corefiles... ");

            // Verify CoreFiles (DLL, Database, and so on)
            foreach (string DataG in this.CoreFiles)
            {
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\" + DataG))
                {
                    MessageBox.Show(
                        string.Format(
                            "The required file {0} in directory {1} is missing. Cannot continue.",
                            DataG,
                            Directory.GetCurrentDirectory()),
                        "Error required file",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    Thread t =
                        new Thread(
                            () =>
                                {
                                    SplashScreen.SS.Dispatcher.BeginInvoke(
                                        (Action)delegate { Application.Current.Shutdown(); });
                                });
                    t.Start(); // Goodbye application :P
                    Assembly.LoadFrom(Directory.GetCurrentDirectory() + @"\" + DataG);
                }

                minicounter++;
                this.localvalue = minicounter / this.CoreFiles.Count;

                if (!ConfUCS.IsConsoleMode)
                    SplashScreen.SS.Dispatcher.BeginInvoke(
                        (Action)delegate { SplashScreen.SS.PB_Loader.Value = this.Inc + (30 * this.localvalue); });
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("OK!\n");
            Console.ResetColor();

            if (!ConfUCS.IsConsoleMode)
            {
                SplashScreen.SS.Dispatcher.BeginInvoke(
                    (Action)delegate
                        {
                            SplashScreen.SS.label_txt.Content = "Checking update... ";
                            SplashScreen.SS.PB_Loader.Value = 80;
                        });

                UpdateChecker.Check();

                SplashScreen.SS.Dispatcher.BeginInvoke(
                    (Action)delegate
                        {
                            SplashScreen.SS.PB_Loader.Value = 100;
                            SplashScreen.SS.Close();
                        });
            }

            ConfUCS.IsPreloaded = true;
        }

        // This is a list of required files. Will check if exist, if not, will show a message and stop.
        // This prevent system crash/corruption datas.
        private void InitializeFileList()
        {
            this.GameListFiles = new List<string>
                                     {
                                         @"gamefiles\csv_client\background_decos.csv",
                                         @"gamefiles\csv_client\billing_packages.csv",
                                         @"gamefiles\csv_client\client_globals.csv",
                                         @"gamefiles\csv_client\credits.csv",
                                         @"gamefiles\csv_client\effects.csv",
                                         @"gamefiles\csv_client\event_output.csv",
                                         @"gamefiles\csv_client\health_bars.csv",
                                         @"gamefiles\csv_client\helpshift.csv",
                                         @"gamefiles\csv_client\hints.csv",
                                         @"gamefiles\csv_client\music.csv",
                                         @"gamefiles\csv_client\news.csv",
                                         @"gamefiles\csv_client\particle_emitters.csv",
                                         @"gamefiles\csv_client\sounds.csv",
                                         @"gamefiles\csv_client\texts.csv",
                                         @"gamefiles\csv_client\texts_patch.csv",
                                         @"gamefiles\level\starting_home_backup.json"

                                         // @"gamefiles\logic\achievements.csv",
                                         // @"gamefiles\logic\alliance_badge_layers.csv",
                                         // @"gamefiles\logic\alliance_badges.csv",
                                         // @"gamefiles\logic\alliance_levels.csv",
                                         // @"gamefiles\logic\alliance_portal.csv",
                                         // @"gamefiles\logic\building_classes.csv",
                                         // @"gamefiles\logic\buildings.csv",
                                         // @"gamefiles\logic\characters.csv",
                                         // @"gamefiles\logic\decos.csv",
                                         // @"gamefiles\logic\effects.csv",
                                         // @"gamefiles\logic\experience_levels.csv",
                                         // @"gamefiles\logic\globals.csv",
                                         // @"gamefiles\logic\heroes.csv",
                                         // @"gamefiles\logic\leagues.csv",
                                         // @"gamefiles\logic\locales.csv",
                                         // @"gamefiles\logic\missions.csv",
                                         // @"gamefiles\logic\npcs.csv",
                                         // @"gamefiles\logic\obstacles.csv",
                                         // @"gamefiles\logic\projectiles.csv",
                                         // @"gamefiles\logic\regions.csv",
                                         // @"gamefiles\logic\resources.csv",
                                         // @"gamefiles\logic\shields.csv",
                                         // @"gamefiles\logic\resources.csv",
                                         // @"gamefiles\logic\shields.csv",
                                         // @"gamefiles\logic\spells.csv",
                                         // @"gamefiles\logic\townhall_levels.csv",
                                         // @"gamefiles\logic\traps.csv",
                                         // @"gamefiles\logic\war.csv",

                                         // @"gamefiles\pve\level1.json",
                                         // @"gamefiles\pve\level2.json",
                                         // @"gamefiles\pve\level3.json",
                                         // @"gamefiles\pve\level4.json",
                                         // @"gamefiles\pve\level5.json",
                                         // @"gamefiles\pve\level6.json",
                                         // @"gamefiles\pve\level7.json",
                                         // @"gamefiles\pve\level8.json",
                                         // @"gamefiles\pve\level9.json",
                                         // @"gamefiles\pve\level10.json",
                                         // @"gamefiles\pve\level11.json",
                                         // @"gamefiles\pve\level12.json",
                                         // @"gamefiles\pve\level13.json",
                                         // @"gamefiles\pve\level14.json",
                                         // @"gamefiles\pve\level15.json",
                                         // @"gamefiles\pve\level16.json",
                                         // @"gamefiles\pve\level17.json",
                                         // @"gamefiles\pve\level18.json",
                                         // @"gamefiles\pve\level19.json",
                                         // @"gamefiles\pve\level20.json",
                                         // @"gamefiles\pve\level21.json",
                                         // @"gamefiles\pve\level22.json",
                                         // @"gamefiles\pve\level23.json",
                                         // @"gamefiles\pve\level24.json",
                                         // @"gamefiles\pve\level25.json",
                                         // @"gamefiles\pve\level26.json",
                                         // @"gamefiles\pve\level27.json",
                                         // @"gamefiles\pve\level28.json",
                                         // @"gamefiles\pve\level29.json",
                                         // @"gamefiles\pve\level30.json",
                                         // @"gamefiles\pve\level31.json",
                                         // @"gamefiles\pve\level32.json",
                                         // @"gamefiles\pve\level33.json",
                                         // @"gamefiles\pve\level34.json",
                                         // @"gamefiles\pve\level35.json",
                                         // @"gamefiles\pve\level36.json",
                                         // @"gamefiles\pve\level37.json",
                                         // @"gamefiles\pve\level38.json",
                                         // @"gamefiles\pve\level39.json",
                                         // @"gamefiles\pve\level40.json",
                                         // @"gamefiles\pve\level41.json",
                                         // @"gamefiles\pve\level42.json",
                                         // @"gamefiles\pve\level43.json",
                                         // @"gamefiles\pve\level44.json",
                                         // @"gamefiles\pve\level45.json",
                                         // @"gamefiles\pve\level46.json",
                                         // @"gamefiles\pve\level47.json",
                                         // @"gamefiles\pve\level48.json",
                                         // @"gamefiles\pve\level49.json",
                                         // @"gamefiles\pve\level50.json"
                                     };

            this.CoreFiles = new List<string>
                                 {
                                     @"EntityFramework.dll",

                                     //@"EntityFramework.SqlServer.dll",
                                     //@"Ionic.Zlib.dll",
                                     @"MySql.Data.dll",
                                     @"MySql.Data.Entity.EF6.dll",
                                     @"Newtonsoft.Json.dll",
                                     @"Newtonsoft.Json.xml",

                                     // @"System.Data.SQLite.dll",
                                     // @"System.Data.SQLite.EF6.dll",
                                     // @"System.Data.SQLite.Linq.dll",
                                     // @"System.Data.SQLite.xml",
                                     @"crs.config"

                                     // @"ucsdb"
                                 };
        }
    }
}
// using CRS.Core.Threading;
using System;
using System.Configuration;
using System.Windows;

using CRS.Core;
using CRS.Sys;

namespace CRS
{
    using CRS.Core.Threading;
    using CRS.UI;

    public partial class App : Application
    {
        [STAThread]
        private void Application_Startup(object sender, StartupEventArgs e)
        {
           // Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE", true);
           // Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE", true);
           // CRS.Properties.Resources.Culture = new System.Globalization.CultureInfo("de-DE",true);
            if (ConfigurationManager.AppSettings["guiMode"].ToLower() == "true")
                ConfUCS.IsConsoleMode = false;
            else
            {
                ConfUCS.IsConsoleMode = true;
                ConfUCS.IsConsoleFirst = true;
            }

            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i].ToLower() == "/gui") ConfUCS.IsConsoleMode = false;
                if (e.Args[i].ToLower() == "/default") ConfUCS.IsDefaultMode = true;
                if (e.Args[i].ToLower() == "/nodebug") ConfUCS.DebugMode = false;

                // if (e.Args[i].ToLower() == "/pirate") null;
            }

            if (!ConfUCS.IsConsoleMode)
            {
                AllocateConsole.Allocate(true);
                AllocateConsole.GetConsoleValue();
                SplashScreen SC = new UI.SplashScreen(); SC.Show();

            }
            else
            {
                AllocateConsole.Allocate();
                AllocateConsole.GetConsoleValue();

                ConsoleThread CT = new ConsoleThread();
                CT.Start();
            }
        }

    }
}

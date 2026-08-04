#region Usings

using System;
using System.Configuration;
using System.Threading;

using CRS.Helpers;
using CRS.Sys;

#endregion

namespace CRS.Core.Threading
{
    internal class ConsoleThread
    {
        /// <summary>
        /// Variable holding the thread itself
        /// </summary>
        private static Thread T { get; set; }

        /// <summary>
        /// Stops the Thread
        /// </summary>
        public static void Stop()
        {
            if (T.ThreadState == ThreadState.Running)
                T.Abort();
        }

        /// <summary>
        /// Starts the Thread
        /// </summary>
        [STAThread]
        public void Start()
        {
            T = new Thread(() =>
            {
                if (ConfUCS.IsConsoleMode)
                    Console.Title = ConfUCS.UnivTitle;
                this.CancelEvent(); // N00b proof

                /* ASCII Art centered */
                Console.WriteLine(@" 
_________  .__                   .__     
\_   ___ \ |  |  _____     ______|  |__  
/    \  \/ |  |  \__  \   /  ___/|  |  \ 
\     \____|  |__ / __ \_ \___ \ |   Y  \
 \______  /|____/(____  //____  >|___|  /
        \/            \/      \/      \/ 
__________                        .__           
\______   \  ____  ___.__._____   |  |    ____  
 |       _/ /  _ \<   |  |\__  \  |  |  _/ __ \ 
 |    |   \(  <_> )\___  | / __ \_|  |__\  ___/ 
 |____|_  / \____/ / ____|(____  /|____/ \___  >
        \/         \/          \/            \/ 
  _________               .__         
 /   _____/______ _____   |__|  ____  
 \_____  \ \____ \\__  \  |  | /    \ 
 /        \|  |_> >/ __ \_|  ||   |  \
/_______  /|   __/(____  /|__||___|  /
        \/ |__|        \/          \/ 
");

                Console.WriteLine("Starting the server...");
                Preload PT = new Preload();
                PT.PreloadThings();
                ControlTimer.StartPerformanceCounter();
                Console.WriteLine(string.Empty);
                Debugger.SetLogLevel(int.Parse(ConfigurationManager.AppSettings["loggingLevel"]));
                Logger.SetLogLevel(int.Parse(ConfigurationManager.AppSettings["loggingLevel"]));
                NetworkThread.Start();
                MemoryThread.Start();
                ConfUCS.UnivTitle = "Clash Royale Spain Server " + ConfUCS.VersionUCS + " | " + "ONLINE";
                if (ConfUCS.IsConsoleMode)
                    CommandParser.ManageConsole();
            });
            T.SetApartmentState(ApartmentState.STA);
            T.Start();
        }

        private void CancelEvent()
        {
            var exitEvent = new ManualResetEvent(false);

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                exitEvent.Set();
            };
        }
    }
}
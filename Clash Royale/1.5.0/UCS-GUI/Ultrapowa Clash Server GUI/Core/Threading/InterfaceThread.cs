using System;
using System.Threading;

namespace CRS.Core.Threading
{
    class InterfaceThread
    {
        /// <summary>
        /// Enumeration holding important information
        /// </summary>
        public static string staticName = "Interface Thread";
        public static string Description = "User GUI";
        public static string Version = "1.0.0";
        public static string Author = "ADeltaX";

        /// <summary>
        /// Variable holding the thread itself
        /// </summary>
        private static Thread T { get; set; }

        /// <summary>
        /// Starts the Thread
        /// </summary>
        [STAThread]
        public static void Start()
        {
            T = new Thread(() =>
            {
                MainWindow.RemoteWindow.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    MainWindow.RemoteWindow.Show();
                });      
            });
            T.SetApartmentState(ApartmentState.STA); // Required running in single thread mode
            T.Start();
        }

        /// <summary>
        /// Stops the Thread
        /// </summary>
        public static void Stop()
        {
            if (T.ThreadState == ThreadState.Running)
                T.Abort();
        }
    }
}

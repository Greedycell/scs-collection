namespace CRS.Core.Threading
{
    #region Usings

    using System;
    using System.Threading;

    using CRS.Network;
    using CRS.Sys;

    #endregion

    internal class NetworkThread
    {
        private static Thread T { get; set; }

        public static void Start()
        {
            T = new Thread(
                () =>
                    {
                        Gateway g = new Gateway();
                        PacketManager ph = new PacketManager();
                        MessageManager dp = new MessageManager();
                        ResourcesManager rm = new ResourcesManager();
                        ObjectManager pm = new ObjectManager();
                        dp.Start();
                        ph.Start();
                        g.Start();
                        ControlTimer.StopPerformanceCounter();
                        ControlTimer.Setup();
                        ConfUCS.IsServerOnline = true;
                        Console.WriteLine("[CRS]    Server started, let's play Clash Royale!");
                    });
            T.Start();
        }

        public static void Stop()
        {
            if (T.ThreadState == ThreadState.Running)
            {
                T.Abort();
            }
        }
    }
}
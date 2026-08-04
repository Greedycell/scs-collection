namespace CRS.Core.Threading
{
    #region Usings

    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Threading;

    using CRS.Core.Settings;
    using Timer = System.Timers.Timer;
    #endregion

    internal class MemoryThread
    {
        /// <summary>
        /// Variable holding the thread itself
        /// </summary>
        private static Thread T { get; set; }

        /// <summary>
        /// Starts the Thread
        /// </summary>
        public static void Start()
        {
            T = new Thread(() =>
            {
                var t = new Timer();
                t.Interval = Constants.MemoryInterval;
                t.Elapsed += (s, a) =>
                {
                    foreach (var p in ResourcesManager.GetConnectedClients())
                    {
                        if (!p.IsClientSocketConnected() && ResourcesManager.SocketContainsLevel(p.GetSocketHandle()))
                        {
                            p.Socket.Shutdown(SocketShutdown.Both);
                            p.Socket.Close();
                            ResourcesManager.DropClient(p.GetSocketHandle());
                        }
                        else if (!p.IsClientSocketConnected() && !ResourcesManager.SocketContainsLevel(p.GetSocketHandle()))
                        {
                            ResourcesManager.RemoveSocketFromServer(p.GetSocketHandle());
                        }
                    }

                    GC.Collect(GC.MaxGeneration);
                    GC.WaitForPendingFinalizers();
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF,
                        (UIntPtr)0xFFFFFFFF);
                };
                t.Enabled = true;
            }); T.Start();
        }

        /// <summary>
        /// Stops the Thread
        /// </summary>
        public static void Stop()
        {
            if (T.ThreadState == System.Threading.ThreadState.Running)
                T.Abort();
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process,
                                                            UIntPtr minimumWorkingSetSize,
                                                            UIntPtr maximumWorkingSetSize);
    }

    internal class PerformanceInfo
    {
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation,
                                                     [In] int Size);

        public static long GetPhysicalAvailableMemoryInMiB()
        {
            PerformanceInformation pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64(pi.PhysicalAvailable.ToInt64() * pi.PageSize.ToInt64() / 1048576);
            }
            else
            {
                return -1;
            }
        }

        public static long GetTotalMemoryInMiB()
        {
            PerformanceInformation pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64(pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / 1048576);
            }
            else
            {
                return -1;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PerformanceInformation
        {
            public int Size;

            public IntPtr CommitTotal;

            public IntPtr CommitLimit;

            public IntPtr CommitPeak;

            public IntPtr PhysicalTotal;

            public IntPtr PhysicalAvailable;

            public IntPtr SystemCache;

            public IntPtr KernelTotal;

            public IntPtr KernelPaged;

            public IntPtr KernelNonPaged;

            public IntPtr PageSize;

            public int HandlesCount;

            public int ProcessCount;

            public int ThreadCount;
        }
    }
}
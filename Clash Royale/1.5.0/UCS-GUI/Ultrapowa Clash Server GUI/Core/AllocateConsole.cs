namespace CRS.Core {
    using System;
    using System.Diagnostics;
    using System.IO;
    using Sys;

    internal static class AllocateConsole {
        public static TextWriter StandardConsole;

        public static void Allocate(bool IsHide = false) {
            IntPtr ptr = ConsoleManage.GetForegroundWindow();
            int u;
            ConsoleManage.GetWindowThreadProcessId(ptr, out u);
            Process process = Process.GetProcessById(u);
            if (process.ProcessName == "cmd") {
                ConsoleManage.AttachConsole(process.Id);
            }
            else {
                ConsoleManage.AllocConsole();
            }

            ConsoleManage.DisableConsoleExit();

            if (IsHide) ConsoleManage.HideConsole();
        }

        public static void GetConsoleValue() {
            StandardConsole = Console.Out;
        }
    }
}
using System;
using System.IO;

namespace UCS.Core
{
	// Token: 0x020000BC RID: 188
	internal static class Debugger
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x000179E1 File Offset: 0x00015BE1
		public static void SetLogLevel(int level)
		{
			Debugger.m_vLogLevel = level;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000179E9 File Offset: 0x00015BE9
		public static int GetLogLevel()
		{
			return Debugger.m_vLogLevel;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000179F0 File Offset: 0x00015BF0
		public static void WriteLine(string text, Exception ex = null, int logLevel = 4)
		{
			string text2 = text;
			if (ex != null)
			{
				text2 += ex.ToString();
			}
			if (logLevel <= Debugger.m_vLogLevel)
			{
				Console.WriteLine(text2);
				object vSyncObject = Debugger.m_vSyncObject;
				lock (vSyncObject)
				{
					Debugger.m_vTextWriter.Write(DateTime.Now.ToString("yyyyMMddHHmmss"));
					Debugger.m_vTextWriter.Write("\t");
					Debugger.m_vTextWriter.WriteLine(text2);
					if (ex != null)
					{
						Debugger.m_vTextWriter.WriteLine(ex.ToString());
					}
					Debugger.m_vTextWriter.Flush();
				}
			}
		}

		// Token: 0x04000328 RID: 808
		private static readonly object m_vSyncObject = new object();

		// Token: 0x04000329 RID: 809
		private static readonly TextWriter m_vTextWriter = InitializeLogWriter();

		// Token: 0x0400032A RID: 810
		private static int m_vLogLevel = 1;

		private static TextWriter InitializeLogWriter()
		{
			string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
			Directory.CreateDirectory(logDirectory);
			string logPath = Path.Combine(logDirectory, "debug_" + DateTime.Now.ToString("yyyyMMdd") + ".log");
			return TextWriter.Synchronized(File.AppendText(logPath));
		}
	}
}

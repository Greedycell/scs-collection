using System;
using System.IO;
using System.Text.RegularExpressions;
using UCS.PacketProcessing;

namespace UCS.Core
{
	// Token: 0x020000BD RID: 189
	internal static class Logger
	{
		// Token: 0x06000593 RID: 1427 RVA: 0x00017AE9 File Offset: 0x00015CE9
		public static void SetLogLevel(int level)
		{
			Logger.m_vLogLevel = level;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00017AF1 File Offset: 0x00015CF1
		public static int GetLogLevel()
		{
			return Logger.m_vLogLevel;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00017AF8 File Offset: 0x00015CF8
		public static void WriteLine(Message p, string prefix = null, int logLevel = 4)
		{
			if (logLevel <= Logger.m_vLogLevel)
			{
				object vSyncObject = Logger.m_vSyncObject;
				lock (vSyncObject)
				{
					Logger.m_vTextWriter.Write(DateTime.Now.ToString("yyyyMMddHHmmss"));
					Logger.m_vTextWriter.Write("; ");
					if (prefix != null)
					{
						Logger.m_vTextWriter.Write(prefix);
					}
					Logger.m_vTextWriter.Write(p.GetMessageType().ToString());
					Logger.m_vTextWriter.Write("(");
					Logger.m_vTextWriter.Write(p.GetMessageVersion().ToString());
					Logger.m_vTextWriter.Write(")");
					Logger.m_vTextWriter.Write("; ");
					Logger.m_vTextWriter.Write(p.GetLength().ToString());
					Logger.m_vTextWriter.Write("; ");
					Logger.m_vTextWriter.WriteLine(p.ToHexString());
					Logger.m_vTextWriter.WriteLine(Regex.Replace(p.ToString(), "[^\\u0020-\\u007F]", "."));
					Logger.m_vTextWriter.Flush();
				}
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00017C40 File Offset: 0x00015E40
		public static void WriteLine(string s, string prefix = null, int logLevel = 4)
		{
			if (logLevel <= Logger.m_vLogLevel)
			{
				object vSyncObject = Logger.m_vSyncObject;
				lock (vSyncObject)
				{
					Logger.m_vTextWriter.Write("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
					Logger.m_vTextWriter.Write("; ");
					if (prefix != null)
					{
						Logger.m_vTextWriter.Write(prefix);
					}
					Logger.m_vTextWriter.WriteLine(s);
					Logger.m_vTextWriter.Flush();
				}
			}
		}

		// Token: 0x0400032B RID: 811
		private static readonly object m_vSyncObject = new object();

		// Token: 0x0400032C RID: 812
		private static readonly TextWriter m_vTextWriter = InitializeLogWriter();

		// Token: 0x0400032D RID: 813
		private static int m_vLogLevel = 1;

		private static TextWriter InitializeLogWriter()
		{
			string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
			Directory.CreateDirectory(logDirectory);
			string logPath = Path.Combine(logDirectory, "data_" + DateTime.Now.ToString("yyyyMMdd") + ".log");
			return TextWriter.Synchronized(File.AppendText(logPath));
		}
	}
}

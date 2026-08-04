using System;
using System.Collections.Generic;

namespace UCS.PacketProcessing
{
	// Token: 0x0200008A RID: 138
	internal static class GameOpCommandFactory
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x0001216C File Offset: 0x0001036C
		public static object Parse(string command)
		{
			string[] array = command.Split(new char[] { ' ' });
			object obj = null;
			if (array.Length != 0 && GameOpCommandFactory.m_vCommands.ContainsKey(array[0]))
			{
				obj = GameOpCommandFactory.m_vCommands[array[0]].GetConstructor(new Type[] { typeof(string[]) }).Invoke(new object[] { array });
			}
			return obj;
		}

		// Token: 0x04000267 RID: 615
		private static readonly Dictionary<string, Type> m_vCommands = new Dictionary<string, Type>();
	}
}

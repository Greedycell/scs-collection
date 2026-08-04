using System;
using System.IO;
using System.Text;

namespace CRS.Utilities.ZLib
{
	// Token: 0x02000028 RID: 40
	internal class SharedUtils
	{
		// Token: 0x0600015D RID: 349 RVA: 0x0000ACA2 File Offset: 0x00008EA2
		public static int URShift(int number, int bits)
		{
			return (int)((uint)number >> bits);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000ACAC File Offset: 0x00008EAC
		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			if (target.Length == 0)
			{
				return 0;
			}
			char[] array = new char[target.Length];
			int num = sourceTextReader.Read(array, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = (byte)array[i];
			}
			return num;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000ACED File Offset: 0x00008EED
		internal static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000ACFA File Offset: 0x00008EFA
		internal static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}
	}
}

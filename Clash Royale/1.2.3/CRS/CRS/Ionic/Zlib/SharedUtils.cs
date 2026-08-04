using System;
using System.IO;
using System.Text;
namespace Ionic.Zlib
{
	// Token: 0x02000015 RID: 21
	internal class SharedUtils
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00009516 File Offset: 0x00007716
		public static int URShift(int number, int bits)
		{
			return (int)((uint)number >> bits);
		}
		// Token: 0x060000C2 RID: 194 RVA: 0x00009520 File Offset: 0x00007720
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
		// Token: 0x060000C3 RID: 195 RVA: 0x00009561 File Offset: 0x00007761
		internal static byte[] ToByteArray(string sourceString)
		{
			return System.Text.Encoding.UTF8.GetBytes(sourceString);
		}
		// Token: 0x060000C4 RID: 196 RVA: 0x0000956E File Offset: 0x0000776E
		internal static char[] ToCharArray(byte[] byteArray)
		{
			return System.Text.Encoding.UTF8.GetChars(byteArray);
		}
	}
}
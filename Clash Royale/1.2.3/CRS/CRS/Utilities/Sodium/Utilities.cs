using System;
using System.Runtime.InteropServices;
using System.Text;

namespace UCS.Utilities.Sodium
{
	// Token: 0x0200004A RID: 74
	public static class Utilities
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x000105B0 File Offset: 0x0000E7B0
		public static string BinaryToHex(byte[] data)
		{
			byte[] array = new byte[data.Length * 2 + 1];
			IntPtr intPtr = SodiumLibrary.sodium_bin2hex(array, array.Length, data, data.Length);
			if (intPtr == IntPtr.Zero)
			{
				throw new OverflowException("Internal error, encoding failed.");
			}
			return Marshal.PtrToStringAnsi(intPtr);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000105FC File Offset: 0x0000E7FC
		public static string BinaryToHex(byte[] data, Utilities.HexFormat format, Utilities.HexCase hcase = Utilities.HexCase.Lower)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				if (i != 0 && format != Utilities.HexFormat.None)
				{
					switch (format)
					{
					case Utilities.HexFormat.Colon:
						stringBuilder.Append(':');
						break;
					case Utilities.HexFormat.Hyphen:
						stringBuilder.Append('-');
						break;
					case Utilities.HexFormat.Space:
						stringBuilder.Append(' ');
						break;
					}
				}
				int num = data[i] >> 4;
				if (hcase == Utilities.HexCase.Lower)
				{
					stringBuilder.Append((char)(87 + num + ((num - 10 >> 31) & -39)));
				}
				else
				{
					stringBuilder.Append((char)(55 + num + ((num - 10 >> 31) & -7)));
				}
				num = (int)(data[i] & 15);
				if (hcase == Utilities.HexCase.Lower)
				{
					stringBuilder.Append((char)(87 + num + ((num - 10 >> 31) & -39)));
				}
				else
				{
					stringBuilder.Append((char)(55 + num + ((num - 10 >> 31) & -7)));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000106D8 File Offset: 0x0000E8D8
		public static byte[] HexToBinary(string hex)
		{
			byte[] array = new byte[hex.Length >> 1];
			IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
			int num;
			bool flag = SodiumLibrary.sodium_hex2bin(intPtr, array.Length, hex, hex.Length, ":- ", out num, null) != 0;
			Marshal.Copy(intPtr, array, 0, num);
			Marshal.FreeHGlobal(intPtr);
			if (flag)
			{
				throw new Exception("Internal error, decoding failed.");
			}
			if (array.Length != num)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				return array2;
			}
			return array;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00010750 File Offset: 0x0000E950
		public static byte[] Increment(byte[] value)
		{
			SodiumLibrary.sodium_increment(value, (long)value.Length);
			return value;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0001076F File Offset: 0x0000E96F
		public static bool Compare(byte[] a, byte[] b)
		{
			return SodiumLibrary.sodium_compare(a, b, (long)a.Length) == 0;
		}

		// Token: 0x0200011B RID: 283
		public enum HexCase
		{
			// Token: 0x040003BA RID: 954
			Lower,
			// Token: 0x040003BB RID: 955
			Upper
		}

		// Token: 0x0200011C RID: 284
		public enum HexFormat
		{
			// Token: 0x040003BD RID: 957
			None,
			// Token: 0x040003BE RID: 958
			Colon,
			// Token: 0x040003BF RID: 959
			Hyphen,
			// Token: 0x040003C0 RID: 960
			Space
		}
	}
}

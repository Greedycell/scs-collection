using System;
using System.Runtime.InteropServices;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000047 RID: 71
	public static class SodiumCore
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000FA33 File Offset: 0x0000DC33
		static SodiumCore()
		{
			SodiumCore.Init();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		public static byte[] GetRandomBytes(int count)
		{
			byte[] array = new byte[count];
			SodiumLibrary.randombytes_buff(array, count);
			return array;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000FA5D File Offset: 0x0000DC5D
		public static int GetRandomNumber(int upperBound)
		{
			return SodiumLibrary.randombytes_uniform(upperBound);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000FA6A File Offset: 0x0000DC6A
		public static string SodiumVersionString()
		{
			return Marshal.PtrToStringAnsi(SodiumLibrary.sodium_version_string());
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000FA7B File Offset: 0x0000DC7B
		internal static void Init()
		{
			if (!SodiumCore._isInit)
			{
				SodiumLibrary.init();
				SodiumCore._isInit = true;
			}
		}

		// Token: 0x040001C5 RID: 453
		private static bool _isInit;
	}
}

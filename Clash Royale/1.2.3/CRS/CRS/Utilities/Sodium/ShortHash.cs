using System;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000046 RID: 70
	public static class ShortHash
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000DD71 File Offset: 0x0000BF71
		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(16);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000F9B1 File Offset: 0x0000DBB1
		public static byte[] Hash(string message, string key)
		{
			return ShortHash.Hash(message, Encoding.UTF8.GetBytes(key));
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		public static byte[] Hash(string message, byte[] key)
		{
			return ShortHash.Hash(Encoding.UTF8.GetBytes(message), key);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
		public static byte[] Hash(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 16)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 16));
			}
			byte[] array = new byte[8];
			SodiumLibrary.crypto_shorthash(array, message, (long)message.Length, key);
			return array;
		}

		// Token: 0x040001C3 RID: 451
		private const int BYTES = 8;

		// Token: 0x040001C4 RID: 452
		private const int KEY_BYTES = 16;
	}
}

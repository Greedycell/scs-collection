using System;
using System.Text;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000036 RID: 54
	public class CryptoHash
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x0000D73A File Offset: 0x0000B93A
		public static byte[] Hash(string message)
		{
			return CryptoHash.Hash(Encoding.UTF8.GetBytes(message));
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D74C File Offset: 0x0000B94C
		public static byte[] Hash(byte[] message)
		{
			byte[] array = new byte[64];
			SodiumLibrary.crypto_hash(array, message, (long)message.Length);
			return array;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000D773 File Offset: 0x0000B973
		public static byte[] Sha512(string message)
		{
			return CryptoHash.Sha512(Encoding.UTF8.GetBytes(message));
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000D788 File Offset: 0x0000B988
		public static byte[] Sha512(byte[] message)
		{
			byte[] array = new byte[64];
			SodiumLibrary.crypto_hash_sha512(array, message, (long)message.Length);
			return array;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D7AF File Offset: 0x0000B9AF
		public static byte[] Sha256(string message)
		{
			return CryptoHash.Sha256(Encoding.UTF8.GetBytes(message));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		public static byte[] Sha256(byte[] message)
		{
			byte[] array = new byte[32];
			SodiumLibrary.crypto_hash_sha256(array, message, (long)message.Length);
			return array;
		}

		// Token: 0x0400017F RID: 383
		private const int SHA512_BYTES = 64;

		// Token: 0x04000180 RID: 384
		private const int SHA256_BYTES = 32;
	}
}

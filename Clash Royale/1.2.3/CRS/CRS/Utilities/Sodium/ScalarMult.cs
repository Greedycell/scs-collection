using System;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000040 RID: 64
	public static class ScalarMult
	{
		// Token: 0x0600022F RID: 559 RVA: 0x0000EC30 File Offset: 0x0000CE30
		public static int Bytes()
		{
			return SodiumLibrary.crypto_scalarmult_bytes();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000EC3C File Offset: 0x0000CE3C
		public static int ScalarBytes()
		{
			return SodiumLibrary.crypto_scalarmult_scalarbytes();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000EC48 File Offset: 0x0000CE48
		private static byte Primitive()
		{
			return SodiumLibrary.crypto_scalarmult_primitive();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000EC54 File Offset: 0x0000CE54
		public static byte[] Base(byte[] secretKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("secretKey must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[32];
			SodiumLibrary.crypto_scalarmult_base(array, secretKey);
			return array;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000ECAC File Offset: 0x0000CEAC
		public static byte[] Mult(byte[] secretKey, byte[] publicKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("secretKey must be {0} bytes in length.", 32));
			}
			if (publicKey == null || publicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("publicKey", (publicKey == null) ? 0 : publicKey.Length, string.Format("publicKey must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[32];
			SodiumLibrary.crypto_scalarmult(array, secretKey, publicKey);
			return array;
		}

		// Token: 0x040001AE RID: 430
		private const int BYTES = 32;

		// Token: 0x040001AF RID: 431
		private const int SCALAR_BYTES = 32;
	}
}

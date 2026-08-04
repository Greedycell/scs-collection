using System;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000045 RID: 69
	public static class SecretKeyAuth
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000DC49 File Offset: 0x0000BE49
		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(32);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000F683 File Offset: 0x0000D883
		public static byte[] Sign(string message, byte[] key)
		{
			return SecretKeyAuth.Sign(Encoding.UTF8.GetBytes(message), key);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000F698 File Offset: 0x0000D898
		public static byte[] Sign(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[32];
			SodiumLibrary.crypto_auth(array, message, (long)message.Length, key);
			return array;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		public static bool Verify(string message, byte[] signature, byte[] key)
		{
			return SecretKeyAuth.Verify(Encoding.UTF8.GetBytes(message), signature, key);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000F708 File Offset: 0x0000D908
		public static bool Verify(byte[] message, byte[] signature, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (signature == null || signature.Length != 32)
			{
				throw new SignatureOutOfRangeException("signature", (signature == null) ? 0 : signature.Length, string.Format("signature must be {0} bytes in length.", 32));
			}
			return SodiumLibrary.crypto_auth_verify(signature, message, (long)message.Length, key) == 0;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000F794 File Offset: 0x0000D994
		public static byte[] SignHmacSha256(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[32];
			SodiumLibrary.crypto_auth_hmacsha256(array, message, (long)message.Length, key);
			return array;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		public static byte[] SignHmacSha256(string message, byte[] key)
		{
			return SecretKeyAuth.SignHmacSha256(Encoding.UTF8.GetBytes(message), key);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000F804 File Offset: 0x0000DA04
		public static byte[] SignHmacSha512(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[64];
			SodiumLibrary.crypto_auth_hmacsha512(array, message, (long)message.Length, key);
			return array;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000F860 File Offset: 0x0000DA60
		public static byte[] SignHmacSha512(string message, byte[] key)
		{
			return SecretKeyAuth.SignHmacSha512(Encoding.UTF8.GetBytes(message), key);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000F873 File Offset: 0x0000DA73
		public static bool VerifyHmacSha256(string message, byte[] signature, byte[] key)
		{
			return SecretKeyAuth.VerifyHmacSha256(Encoding.UTF8.GetBytes(message), signature, key);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000F888 File Offset: 0x0000DA88
		public static bool VerifyHmacSha256(byte[] message, byte[] signature, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (signature == null || signature.Length != 32)
			{
				throw new SignatureOutOfRangeException("signature", (signature == null) ? 0 : signature.Length, string.Format("signature must be {0} bytes in length.", 32));
			}
			return SodiumLibrary.crypto_auth_hmacsha256_verify(signature, message, (long)message.Length, key) == 0;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000F911 File Offset: 0x0000DB11
		public static bool VerifyHmacSha512(string message, byte[] signature, byte[] key)
		{
			return SecretKeyAuth.VerifyHmacSha512(Encoding.UTF8.GetBytes(message), signature, key);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000F928 File Offset: 0x0000DB28
		public static bool VerifyHmacSha512(byte[] message, byte[] signature, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (signature == null || signature.Length != 64)
			{
				throw new SignatureOutOfRangeException("signature", (signature == null) ? 0 : signature.Length, string.Format("signature must be {0} bytes in length.", 64));
			}
			return SodiumLibrary.crypto_auth_hmacsha512_verify(signature, message, (long)message.Length, key) == 0;
		}

		// Token: 0x040001BD RID: 445
		private const int KEY_BYTES = 32;

		// Token: 0x040001BE RID: 446
		private const int BYTES = 32;

		// Token: 0x040001BF RID: 447
		private const int CRYPTO_AUTH_HMACSHA256_KEY_BYTES = 32;

		// Token: 0x040001C0 RID: 448
		private const int CRYPTO_AUTH_HMACSHA256_BYTES = 32;

		// Token: 0x040001C1 RID: 449
		private const int CRYPTO_AUTH_HMACSHA512_KEY_BYTES = 32;

		// Token: 0x040001C2 RID: 450
		private const int CRYPTO_AUTH_HMACSHA512_BYTES = 64;
	}
}

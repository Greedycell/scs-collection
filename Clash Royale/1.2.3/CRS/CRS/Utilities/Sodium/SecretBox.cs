using System;
using System.Security.Cryptography;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000044 RID: 68
	public static class SecretBox
	{
		// Token: 0x06000243 RID: 579 RVA: 0x0000DC49 File Offset: 0x0000BE49
		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(32);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000E7F5 File Offset: 0x0000C9F5
		public static byte[] GenerateNonce()
		{
			return SodiumCore.GetRandomBytes(24);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000F334 File Offset: 0x0000D534
		public static byte[] Create(string message, byte[] nonce, byte[] key)
		{
			return SecretBox.Create(Encoding.UTF8.GetBytes(message), nonce, key);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000F348 File Offset: 0x0000D548
		public static byte[] Create(byte[] message, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[message.Length + 32];
			Array.Copy(message, 0, array, 32, message.Length);
			byte[] array2 = new byte[array.Length];
			if (SodiumLibrary.crypto_secretbox(array2, array, (long)array.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Failed to create SecretBox");
			}
			return array2;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000F3FF File Offset: 0x0000D5FF
		public static DetachedBox CreateDetached(string message, byte[] nonce, byte[] key)
		{
			return SecretBox.CreateDetached(Encoding.UTF8.GetBytes(message), nonce, key);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000F414 File Offset: 0x0000D614
		public static DetachedBox CreateDetached(byte[] message, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[message.Length];
			byte[] array2 = new byte[16];
			if (SodiumLibrary.crypto_secretbox_detached(array, array2, message, (long)message.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Failed to create detached SecretBox");
			}
			return new DetachedBox(array, array2);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000F4C1 File Offset: 0x0000D6C1
		public static byte[] Open(string cipherText, byte[] nonce, byte[] key)
		{
			return SecretBox.Open(Utilities.HexToBinary(cipherText), nonce, key);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		public static byte[] Open(byte[] cipherText, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[cipherText.Length];
			if (SodiumLibrary.crypto_secretbox_open(array, cipherText, (long)cipherText.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Failed to open SecretBox");
			}
			byte[] array2 = new byte[array.Length - 32];
			Array.Copy(array, 32, array2, 0, array.Length - 32);
			return array2;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000F58A File Offset: 0x0000D78A
		public static byte[] OpenDetached(string cipherText, byte[] mac, byte[] nonce, byte[] key)
		{
			return SecretBox.OpenDetached(Utilities.HexToBinary(cipherText), mac, nonce, key);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000F59A File Offset: 0x0000D79A
		public static byte[] OpenDetached(DetachedBox detached, byte[] nonce, byte[] key)
		{
			return SecretBox.OpenDetached(detached.CipherText, detached.Mac, nonce, key);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		public static byte[] OpenDetached(byte[] cipherText, byte[] mac, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			if (mac == null || mac.Length != 16)
			{
				throw new MacOutOfRangeException("mac", (mac == null) ? 0 : mac.Length, string.Format("mac must be {0} bytes in length.", 16));
			}
			byte[] array = new byte[cipherText.Length];
			if (SodiumLibrary.crypto_secretbox_open_detached(array, cipherText, mac, (long)cipherText.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Failed to open detached SecretBox");
			}
			return array;
		}

		// Token: 0x040001B9 RID: 441
		private const int KEY_BYTES = 32;

		// Token: 0x040001BA RID: 442
		private const int NONCE_BYTES = 24;

		// Token: 0x040001BB RID: 443
		private const int ZERO_BYTES = 32;

		// Token: 0x040001BC RID: 444
		private const int MAC_BYTES = 16;
	}
}

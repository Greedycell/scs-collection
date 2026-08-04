using System;
using System.Security.Cryptography;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000049 RID: 73
	public static class StreamEncryption
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x0000DC49 File Offset: 0x0000BE49
		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(32);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000E7F5 File Offset: 0x0000C9F5
		public static byte[] GenerateNonce()
		{
			return SodiumCore.GetRandomBytes(24);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000EEBD File Offset: 0x0000D0BD
		public static byte[] GenerateNonceChaCha20()
		{
			return SodiumCore.GetRandomBytes(8);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000102ED File Offset: 0x0000E4ED
		public static byte[] Encrypt(string message, byte[] nonce, byte[] key)
		{
			return StreamEncryption.Encrypt(Encoding.UTF8.GetBytes(message), nonce, key);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00010304 File Offset: 0x0000E504
		public static byte[] Encrypt(byte[] message, byte[] nonce, byte[] key)
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
			if (SodiumLibrary.crypto_stream_xor(array, message, (long)message.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Error encrypting message.");
			}
			return array;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000103A2 File Offset: 0x0000E5A2
		public static byte[] EncryptChaCha20(string message, byte[] nonce, byte[] key)
		{
			return StreamEncryption.EncryptChaCha20(Encoding.UTF8.GetBytes(message), nonce, key);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000103B8 File Offset: 0x0000E5B8
		public static byte[] EncryptChaCha20(byte[] message, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 8)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 8));
			}
			byte[] array = new byte[message.Length];
			if (SodiumLibrary.crypto_stream_chacha20_xor(array, message, (long)message.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Error encrypting message.");
			}
			return array;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00010454 File Offset: 0x0000E654
		public static byte[] Decrypt(string cipherText, byte[] nonce, byte[] key)
		{
			return StreamEncryption.Decrypt(Utilities.HexToBinary(cipherText), nonce, key);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00010464 File Offset: 0x0000E664
		public static byte[] Decrypt(byte[] cipherText, byte[] nonce, byte[] key)
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
			if (SodiumLibrary.crypto_stream_xor(array, cipherText, (long)cipherText.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Error derypting message.");
			}
			return array;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00010502 File Offset: 0x0000E702
		public static byte[] DecryptChaCha20(string cipherText, byte[] nonce, byte[] key)
		{
			return StreamEncryption.DecryptChaCha20(Utilities.HexToBinary(cipherText), nonce, key);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00010514 File Offset: 0x0000E714
		public static byte[] DecryptChaCha20(byte[] cipherText, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 8)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 8));
			}
			byte[] array = new byte[cipherText.Length];
			if (SodiumLibrary.crypto_stream_chacha20_xor(array, cipherText, (long)cipherText.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Error derypting message.");
			}
			return array;
		}

		// Token: 0x04000206 RID: 518
		private const int XSALSA20_KEY_BYTES = 32;

		// Token: 0x04000207 RID: 519
		private const int XSALSA20_NONCE_BYTES = 24;

		// Token: 0x04000208 RID: 520
		private const int CHACHA20_KEY_BYTES = 32;

		// Token: 0x04000209 RID: 521
		private const int CHACHA20_NONCEBYTES = 8;
	}
}

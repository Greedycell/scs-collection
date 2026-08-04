using System;
using System.Security.Cryptography;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x0200003F RID: 63
	public static class PublicKeyBox
	{
		// Token: 0x06000224 RID: 548 RVA: 0x0000E774 File Offset: 0x0000C974
		public static KeyPair GenerateKeyPair()
		{
			byte[] array = new byte[32];
			byte[] array2 = new byte[32];
			SodiumLibrary.crypto_box_keypair(array, array2);
			return new KeyPair(array, array2);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		public static KeyPair GenerateKeyPair(byte[] privateKey)
		{
			if (privateKey == null || privateKey.Length != 32)
			{
				throw new SeedOutOfRangeException("privateKey", (privateKey == null) ? 0 : privateKey.Length, string.Format("privateKey must be {0} bytes in length.", 32));
			}
			return new KeyPair(ScalarMult.Base(privateKey), privateKey);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000E7F5 File Offset: 0x0000C9F5
		public static byte[] GenerateNonce()
		{
			return SodiumCore.GetRandomBytes(24);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000E7FE File Offset: 0x0000C9FE
		public static byte[] Create(string message, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			return PublicKeyBox.Create(Encoding.UTF8.GetBytes(message), nonce, secretKey, publicKey);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000E814 File Offset: 0x0000CA14
		public static byte[] Create(byte[] message, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (publicKey == null || publicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("publicKey", (publicKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[message.Length + 16];
			if (SodiumLibrary.crypto_box_easy(array, message, (long)message.Length, nonce, publicKey, secretKey) != 0)
			{
				throw new CryptographicException("Failed to create PublicKeyBox");
			}
			return array;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000E8EA File Offset: 0x0000CAEA
		public static DetachedBox CreateDetached(string message, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			return PublicKeyBox.CreateDetached(Encoding.UTF8.GetBytes(message), nonce, secretKey, publicKey);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000E900 File Offset: 0x0000CB00
		public static DetachedBox CreateDetached(byte[] message, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (publicKey == null || publicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("publicKey", (publicKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[message.Length];
			byte[] array2 = new byte[16];
			if (SodiumLibrary.crypto_box_detached(array, array2, message, (long)message.Length, nonce, secretKey, publicKey) != 0)
			{
				throw new CryptographicException("Failed to create public detached Box");
			}
			return new DetachedBox(array, array2);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		public static byte[] Open(byte[] cipherText, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (publicKey == null || publicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("publicKey", (publicKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			if (cipherText[0] == 0)
			{
				bool flag = true;
				for (int i = 0; i < 15; i++)
				{
					if (cipherText[i] != 0)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					byte[] array = new byte[cipherText.Length - 16];
					Array.Copy(cipherText, 16, array, 0, cipherText.Length - 16);
					cipherText = array;
				}
			}
			byte[] array2 = new byte[cipherText.Length - 16];
			if (SodiumLibrary.crypto_box_open_easy(array2, cipherText, (long)cipherText.Length, nonce, publicKey, secretKey) != 0)
			{
				throw new CryptographicException("Failed to open PublicKeyBox");
			}
			return array2;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000EAF9 File Offset: 0x0000CCF9
		public static byte[] OpenDetached(string cipherText, byte[] mac, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			return PublicKeyBox.OpenDetached(Utilities.HexToBinary(cipherText), mac, nonce, secretKey, publicKey);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000EB0B File Offset: 0x0000CD0B
		public static byte[] OpenDetached(DetachedBox detached, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			return PublicKeyBox.OpenDetached(detached.CipherText, detached.Mac, nonce, secretKey, publicKey);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000EB24 File Offset: 0x0000CD24
		public static byte[] OpenDetached(byte[] cipherText, byte[] mac, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (publicKey == null || publicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("publicKey", (publicKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (mac == null || mac.Length != 16)
			{
				throw new MacOutOfRangeException("mac", (mac == null) ? 0 : mac.Length, string.Format("mac must be {0} bytes in length.", 16));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[cipherText.Length];
			if (SodiumLibrary.crypto_box_open_detached(array, cipherText, mac, (long)cipherText.Length, nonce, secretKey, publicKey) != 0)
			{
				throw new CryptographicException("Failed to open public detached Box");
			}
			return array;
		}

		// Token: 0x040001AA RID: 426
		public const int PublicKeyBytes = 32;

		// Token: 0x040001AB RID: 427
		public const int SecretKeyBytes = 32;

		// Token: 0x040001AC RID: 428
		private const int NONCE_BYTES = 24;

		// Token: 0x040001AD RID: 429
		private const int MAC_BYTES = 16;
	}
}

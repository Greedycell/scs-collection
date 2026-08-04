using System;
using System.Security.Cryptography;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000041 RID: 65
	public static class SealedPublicKeyBox
	{
		// Token: 0x06000234 RID: 564 RVA: 0x0000ED38 File Offset: 0x0000CF38
		public static byte[] Create(string message, KeyPair recipientKeyPair)
		{
			return SealedPublicKeyBox.Create(Encoding.UTF8.GetBytes(message), recipientKeyPair.PublicKey);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000ED50 File Offset: 0x0000CF50
		public static byte[] Create(byte[] message, KeyPair recipientKeyPair)
		{
			return SealedPublicKeyBox.Create(message, recipientKeyPair.PublicKey);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000ED5E File Offset: 0x0000CF5E
		public static byte[] Create(string message, byte[] recipientPublicKey)
		{
			return SealedPublicKeyBox.Create(Encoding.UTF8.GetBytes(message), recipientPublicKey);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public static byte[] Create(byte[] message, byte[] recipientPublicKey)
		{
			if (recipientPublicKey == null || recipientPublicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("recipientPublicKey", (recipientPublicKey == null) ? 0 : recipientPublicKey.Length, string.Format("recipientPublicKey must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[message.Length + 48];
			if (SodiumLibrary.crypto_box_seal(array, message, (long)message.Length, recipientPublicKey) != 0)
			{
				throw new CryptographicException("Failed to create SealedBox");
			}
			return array;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
		public static byte[] Open(string cipherText, KeyPair recipientKeyPair)
		{
			return SealedPublicKeyBox.Open(Utilities.HexToBinary(cipherText), recipientKeyPair.PrivateKey, recipientKeyPair.PublicKey);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000EDF9 File Offset: 0x0000CFF9
		public static byte[] Open(byte[] cipherText, KeyPair recipientKeyPair)
		{
			return SealedPublicKeyBox.Open(cipherText, recipientKeyPair.PrivateKey, recipientKeyPair.PublicKey);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000EE0D File Offset: 0x0000D00D
		public static byte[] Open(string cipherText, byte[] recipientSecretKey, byte[] recipientPublicKey)
		{
			return SealedPublicKeyBox.Open(Utilities.HexToBinary(cipherText), recipientSecretKey, recipientPublicKey);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public static byte[] Open(byte[] cipherText, byte[] recipientSecretKey, byte[] recipientPublicKey)
		{
			if (recipientSecretKey == null || recipientSecretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("recipientPublicKey", (recipientSecretKey == null) ? 0 : recipientSecretKey.Length, string.Format("recipientSecretKey must be {0} bytes in length.", 32));
			}
			if (recipientPublicKey == null || recipientPublicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("recipientPublicKey", (recipientPublicKey == null) ? 0 : recipientPublicKey.Length, string.Format("recipientPublicKey must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[cipherText.Length - 48];
			if (SodiumLibrary.crypto_box_seal_open(array, cipherText, (long)cipherText.Length, recipientPublicKey, recipientSecretKey) != 0)
			{
				throw new CryptographicException("Failed to open SealedBox");
			}
			return array;
		}

		// Token: 0x040001B0 RID: 432
		public const int RecipientPublicKeyBytes = 32;

		// Token: 0x040001B1 RID: 433
		public const int RecipientSecretKeyBytes = 32;

		// Token: 0x040001B2 RID: 434
		private const int CryptoBoxSealbytes = 48;
	}
}

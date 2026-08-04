using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000043 RID: 67
	public static class SecretAeadAes
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000F0EE File Offset: 0x0000D2EE
		public static bool IsAvailable()
		{
			SodiumCore.Init();
			return SodiumLibrary.crypto_aead_aes256gcm_is_available() != 0;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000F102 File Offset: 0x0000D302
		public static byte[] GenerateNonce()
		{
			return SodiumCore.GetRandomBytes(12);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000F10C File Offset: 0x0000D30C
		public static byte[] Encrypt(byte[] message, byte[] nonce, byte[] key, byte[] additionalData = null)
		{
			if (additionalData == null)
			{
				additionalData = new byte[0];
			}
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 12)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 12));
			}
			if (additionalData.Length > 16 || additionalData.Length < 0)
			{
				throw new AdditionalDataOutOfRangeException(string.Format("additionalData must be between {0} and {1} bytes in length.", 0, 16));
			}
			byte[] array = new byte[message.Length + 16];
			IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
			long num;
			bool flag = SodiumLibrary.crypto_aead_aes256gcm_encrypt(intPtr, out num, message, (long)message.Length, additionalData, (long)additionalData.Length, null, nonce, key) != 0;
			Marshal.Copy(intPtr, array, 0, (int)num);
			Marshal.FreeHGlobal(intPtr);
			if (flag)
			{
				throw new CryptographicException("Error encrypting message.");
			}
			if ((long)array.Length == num)
			{
				return array;
			}
			byte[] array2 = new byte[num];
			Array.Copy(array, 0L, array2, 0L, num);
			return array2;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000F220 File Offset: 0x0000D420
		public static byte[] Decrypt(byte[] cipher, byte[] nonce, byte[] key, byte[] additionalData = null)
		{
			if (additionalData == null)
			{
				additionalData = new byte[0];
			}
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 12)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 12));
			}
			if (additionalData.Length > 16 || additionalData.Length < 0)
			{
				throw new AdditionalDataOutOfRangeException(string.Format("additionalData must be between {0} and {1} bytes in length.", 0, 16));
			}
			byte[] array = new byte[cipher.Length - 16];
			IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
			long num;
			bool flag = SodiumLibrary.crypto_aead_aes256gcm_decrypt(intPtr, out num, null, cipher, (long)cipher.Length, additionalData, (long)additionalData.Length, nonce, key) != 0;
			Marshal.Copy(intPtr, array, 0, (int)num);
			Marshal.FreeHGlobal(intPtr);
			if (flag)
			{
				throw new CryptographicException("Error decrypting message.");
			}
			if ((long)array.Length == num)
			{
				return array;
			}
			byte[] array2 = new byte[num];
			Array.Copy(array, 0L, array2, 0L, num);
			return array2;
		}

		// Token: 0x040001B6 RID: 438
		private const int KEYBYTES = 32;

		// Token: 0x040001B7 RID: 439
		private const int NPUBBYTES = 12;

		// Token: 0x040001B8 RID: 440
		private const int ABYTES = 16;
	}
}

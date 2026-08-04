using System;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x0200003C RID: 60
	public static class OneTimeAuth
	{
		// Token: 0x060001FF RID: 511 RVA: 0x0000DC49 File Offset: 0x0000BE49
		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(32);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000DC52 File Offset: 0x0000BE52
		public static byte[] Sign(string message, byte[] key)
		{
			return OneTimeAuth.Sign(Encoding.UTF8.GetBytes(message), key);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000DC68 File Offset: 0x0000BE68
		public static byte[] Sign(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[16];
			SodiumLibrary.crypto_onetimeauth(array, message, (long)message.Length, key);
			return array;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
		public static bool Verify(string message, byte[] signature, byte[] key)
		{
			return OneTimeAuth.Verify(Encoding.UTF8.GetBytes(message), signature, key);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		public static bool Verify(byte[] message, byte[] signature, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (signature == null || signature.Length != 16)
			{
				throw new SignatureOutOfRangeException("signature", (signature == null) ? 0 : signature.Length, string.Format("signature must be {0} bytes in length.", 16));
			}
			return SodiumLibrary.crypto_onetimeauth_verify(signature, message, (long)message.Length, key) == 0;
		}

		// Token: 0x04000190 RID: 400
		private const int KEY_BYTES = 32;

		// Token: 0x04000191 RID: 401
		private const int BYTES = 16;
	}
}

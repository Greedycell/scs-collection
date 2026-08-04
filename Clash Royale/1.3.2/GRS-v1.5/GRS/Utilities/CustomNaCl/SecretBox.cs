using System;
using CRS.Utilities.TweetNaCl;

namespace CRS.Utilities.CustomNaCl
{
	// Token: 0x0200005F RID: 95
	public class SecretBox
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0001394B File Offset: 0x00011B4B
		public SecretBox(byte[] s)
		{
			this.KnownSharedKey = s;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00013968 File Offset: 0x00011B68
		public byte[] create(byte[] plain, byte[] nonce)
		{
			int num = plain.Length;
			byte[] array = new byte[num + 32];
			Array.Copy(plain, 0, array, 32, num);
			byte[] array2 = new byte[array.Length];
			if (xsalsa20poly1305.crypto_secretbox(array2, array, (long)array.Length, nonce, this.KnownSharedKey) != 0)
			{
				throw new Exception("SecretBox Encryption failed");
			}
			return array2;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000139B8 File Offset: 0x00011BB8
		public byte[] open(byte[] cipher, byte[] nonce)
		{
			int num = cipher.Length;
			byte[] array = new byte[num];
			if (xsalsa20poly1305.crypto_secretbox_open(array, cipher, (long)num, nonce, this.KnownSharedKey) != 0)
			{
				throw new Exception("SecretBox Decryption failed");
			}
			byte[] array2 = new byte[array.Length - 32];
			Array.Copy(array, 32, array2, 0, array.Length - 32);
			return array2;
		}

		// Token: 0x04000284 RID: 644
		private const int SHAREDKEYLENGTH = 32;

		// Token: 0x04000285 RID: 645
		private readonly byte[] KnownSharedKey = new byte[32];
	}
}

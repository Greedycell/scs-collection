using System;
using CRS.Utilities.TweetNaCl;

namespace CRS.Utilities.CustomNaCl
{
	// Token: 0x0200005E RID: 94
	public class PublicBox
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00013866 File Offset: 0x00011A66
		public PublicBox(byte[] privatekey, byte[] publickey)
		{
			curve25519xsalsa20poly1305.crypto_box_beforenm(this.PrecomputedSharedKey, publickey, privatekey);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001388C File Offset: 0x00011A8C
		public byte[] create(byte[] plain, byte[] nonce)
		{
			int num = plain.Length;
			byte[] array = new byte[num + 32];
			Array.Copy(plain, 0, array, 32, num);
			if (curve25519xsalsa20poly1305.crypto_box_afternm(array, array, (long)array.Length, nonce, this.PrecomputedSharedKey) != 0)
			{
				throw new Exception("PublicBox Encryption failed");
			}
			byte[] array2 = new byte[num + 16];
			Array.Copy(array, 16, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000138EC File Offset: 0x00011AEC
		public byte[] open(byte[] cipher, byte[] nonce)
		{
			int num = cipher.Length;
			byte[] array = new byte[num + 16];
			Array.Copy(cipher, 0, array, 16, num);
			if (curve25519xsalsa20poly1305.crypto_box_afternm(array, array, (long)array.Length, nonce, this.PrecomputedSharedKey) != 0)
			{
				throw new Exception("PublicBox Decryption failed");
			}
			byte[] array2 = new byte[array.Length - 32];
			Array.Copy(array, 32, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x0400027E RID: 638
		private const int KEYBYTES = 32;

		// Token: 0x0400027F RID: 639
		private const int NONCEBYTES = 24;

		// Token: 0x04000280 RID: 640
		private const int ZEROBYTES = 32;

		// Token: 0x04000281 RID: 641
		private const int BOXZEROBYTES = 16;

		// Token: 0x04000282 RID: 642
		private const int BEFORENMBYTES = 32;

		// Token: 0x04000283 RID: 643
		private readonly byte[] PrecomputedSharedKey = new byte[32];
	}
}

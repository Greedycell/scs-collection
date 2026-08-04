using System;

namespace CRS.Utilities.TweetNaCl
{
	// Token: 0x02000032 RID: 50
	public class curve25519xsalsa20poly1305
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x0000CE56 File Offset: 0x0000B056
		public static int crypto_box_getpublickey(byte[] pk, byte[] sk)
		{
			return curve25519.crypto_scalarmult_base(pk, sk);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000CE5F File Offset: 0x0000B05F
		public static int crypto_box_keypair(byte[] pk, byte[] sk)
		{
			new Random().NextBytes(sk);
			return curve25519.crypto_scalarmult_base(pk, sk);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000CE73 File Offset: 0x0000B073
		public static int crypto_box_afternm(byte[] c, byte[] m, long mlen, byte[] n, byte[] k)
		{
			return xsalsa20poly1305.crypto_secretbox(c, m, mlen, n, k);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000CE80 File Offset: 0x0000B080
		public static int crypto_box_beforenm(byte[] k, byte[] pk, byte[] sk)
		{
			byte[] array = new byte[32];
			byte[] sigma = xsalsa20.sigma;
			curve25519.crypto_scalarmult(array, sk, pk);
			return hsalsa20.crypto_core(k, null, array, sigma);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		public static int crypto_box(byte[] c, byte[] m, long mlen, byte[] n, byte[] pk, byte[] sk)
		{
			byte[] array = new byte[32];
			curve25519xsalsa20poly1305.crypto_box_beforenm(array, pk, sk);
			return curve25519xsalsa20poly1305.crypto_box_afternm(c, m, mlen, n, array);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000CEDC File Offset: 0x0000B0DC
		public static int crypto_box_open(byte[] m, byte[] c, long clen, byte[] n, byte[] pk, byte[] sk)
		{
			byte[] array = new byte[32];
			curve25519xsalsa20poly1305.crypto_box_beforenm(array, pk, sk);
			return curve25519xsalsa20poly1305.crypto_box_open_afternm(m, c, clen, n, array);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000CF06 File Offset: 0x0000B106
		public static int crypto_box_open_afternm(byte[] m, byte[] c, long clen, byte[] n, byte[] k)
		{
			return xsalsa20poly1305.crypto_secretbox_open(m, c, clen, n, k);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000CF14 File Offset: 0x0000B114
		public static int crypto_box_afternm(byte[] c, byte[] m, byte[] n, byte[] k)
		{
			return curve25519xsalsa20poly1305.crypto_box_afternm(c, m, (long)m.Length, n, k);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000CF34 File Offset: 0x0000B134
		public static int crypto_box_open_afternm(byte[] m, byte[] c, byte[] n, byte[] k)
		{
			return curve25519xsalsa20poly1305.crypto_box_open_afternm(m, c, (long)c.Length, n, k);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000CF54 File Offset: 0x0000B154
		public static int crypto_box(byte[] c, byte[] m, byte[] n, byte[] pk, byte[] sk)
		{
			return curve25519xsalsa20poly1305.crypto_box(c, m, (long)m.Length, n, pk, sk);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000CF78 File Offset: 0x0000B178
		public static int crypto_box_open(byte[] m, byte[] c, byte[] n, byte[] pk, byte[] sk)
		{
			return curve25519xsalsa20poly1305.crypto_box_open(m, c, (long)c.Length, n, pk, sk);
		}

		// Token: 0x0400017C RID: 380
		public const int crypto_secretbox_PUBLICKEYBYTES = 32;

		// Token: 0x0400017D RID: 381
		public const int crypto_secretbox_SECRETKEYBYTES = 32;

		// Token: 0x0400017E RID: 382
		public const int crypto_secretbox_BEFORENMBYTES = 32;

		// Token: 0x0400017F RID: 383
		public const int crypto_secretbox_NONCEBYTES = 24;

		// Token: 0x04000180 RID: 384
		public const int crypto_secretbox_ZEROBYTES = 32;

		// Token: 0x04000181 RID: 385
		public const int crypto_secretbox_BOXZEROBYTES = 16;
	}
}

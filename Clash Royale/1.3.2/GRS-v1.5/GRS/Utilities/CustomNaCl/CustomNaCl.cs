using System;

namespace CRS.Utilities.CustomNaCl
{
	// Token: 0x0200005B RID: 91
	internal class CustomNaCl
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0001373C File Offset: 0x0001193C
		public static byte[] OpenPublicBox(byte[] c, byte[] n, byte[] sk, byte[] pk)
		{
			return new PublicBox(sk, pk).open(c, n);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0001374C File Offset: 0x0001194C
		public static byte[] CreatePublicBox(byte[] p, byte[] n, byte[] sk, byte[] pk)
		{
			return new PublicBox(sk, pk).create(p, n);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0001375C File Offset: 0x0001195C
		public static byte[] OpenSecretBox(byte[] c, byte[] n, byte[] s)
		{
			return new SecretBox(s).open(c, n);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0001376B File Offset: 0x0001196B
		public static byte[] CreateSecretBox(byte[] p, byte[] n, byte[] s)
		{
			return new SecretBox(s).create(p, n);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0001377A File Offset: 0x0001197A
		public static KeyPairGL GenerateKeyPair(byte[] p, byte[] s)
		{
			return new KeyPairGL(p, s);
		}
	}
}

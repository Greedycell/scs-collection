using System;
using CRS.Utilities.TweetNaCl;

namespace CRS.Utilities.CustomNaCl
{
	// Token: 0x0200005C RID: 92
	internal class KeyPair
	{
		// Token: 0x060002BB RID: 699 RVA: 0x00013783 File Offset: 0x00011983
		public KeyPair()
		{
			curve25519xsalsa20poly1305.crypto_box_keypair(this.PublicKey, this.SecretKey);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000137B7 File Offset: 0x000119B7
		public byte[] PublicKey { get; } = new byte[32];

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000137BF File Offset: 0x000119BF
		public byte[] SecretKey { get; } = new byte[32];
	}
}

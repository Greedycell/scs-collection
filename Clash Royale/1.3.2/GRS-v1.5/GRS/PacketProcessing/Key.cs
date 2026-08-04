using System;

namespace CRS.PacketProcessing
{
	// Token: 0x02000002 RID: 2
	public static class Key
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static Crypto Crypto
		{
			get
			{
				return new Crypto((byte[])Key._standardPublicKey.Clone(), (byte[])Key._standardPrivateKey.Clone());
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly byte[] _standardPrivateKey = new byte[]
		{
			24, 145, 212, 1, 250, 219, 81, 210, 93, 58,
			145, 116, 212, 114, 169, 246, 145, 164, 91, 151,
			66, 133, 212, 119, 41, 196, 92, 101, 56, 7,
			13, 133
		};

		// Token: 0x04000002 RID: 2
		private static readonly byte[] _standardPublicKey = new byte[]
		{
			114, 241, 164, 164, 196, 142, 68, 218, 12, 66,
			49, 15, 128, 14, 150, 98, 78, 109, 198, 166,
			65, 169, 212, 28, 59, 80, 57, 216, 223, 173,
			194, 126
		};
	}
}

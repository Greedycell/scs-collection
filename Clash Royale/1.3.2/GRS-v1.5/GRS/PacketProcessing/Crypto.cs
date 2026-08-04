using System;
using CRS.Utilities.CustomNaCl;

namespace CRS.PacketProcessing
{
	// Token: 0x02000003 RID: 3
	public class Crypto : IDisposable
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020A5 File Offset: 0x000002A5
		public Crypto(byte[] publicKey, byte[] privateKey)
		{
			this._keyPair = CustomNaCl.GenerateKeyPair(publicKey, privateKey);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020BA File Offset: 0x000002BA
		public void Dispose()
		{
			if (this._disposed)
			{
				return;
			}
			this._keyPair.Dispose();
			this._disposed = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020DD File Offset: 0x000002DD
		public byte[] PrivateKey
		{
			get
			{
				return this._keyPair.PrivateKey;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020EA File Offset: 0x000002EA
		public byte[] PublicKey
		{
			get
			{
				return this._keyPair.PublicKey;
			}
		}

		// Token: 0x04000003 RID: 3
		public const int KeyLength = 32;

		// Token: 0x04000004 RID: 4
		public const int NonceLength = 24;

		// Token: 0x04000005 RID: 5
		private readonly KeyPairGL _keyPair;

		// Token: 0x04000006 RID: 6
		private bool _disposed;
	}
}

using System;
using UCS.Utilities.Sodium;

namespace UCS.PacketProcessing
{
	// Token: 0x0200005F RID: 95
	public class ClashKeyPair : IDisposable
	{
		// Token: 0x060002FF RID: 767 RVA: 0x000108CC File Offset: 0x0000EACC
		public ClashKeyPair(byte[] publicKey, byte[] privateKey)
		{
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			if (publicKey.Length != 32)
			{
				throw new ArgumentOutOfRangeException("publicKey", "publicKey must be 32 bytes in length.");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			if (privateKey.Length != 32)
			{
				throw new ArgumentOutOfRangeException("privateKey", "publicKey must be 32 bytes in length.");
			}
			this._keyPair = new KeyPair(publicKey, privateKey);
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00010936 File Offset: 0x0000EB36
		public byte[] PrivateKey
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(null, "Cannot access CoCKeyPair object because it was disposed.");
				}
				return this._keyPair.PrivateKey;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00010957 File Offset: 0x0000EB57
		public byte[] PublicKey
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(null, "Cannot access CoCKeyPair object because it was disposed.");
				}
				return this._keyPair.PublicKey;
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00010978 File Offset: 0x0000EB78
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

		// Token: 0x04000211 RID: 529
		public const int KeyLength = 32;

		// Token: 0x04000212 RID: 530
		public const int NonceLength = 24;

		// Token: 0x04000213 RID: 531
		private readonly KeyPair _keyPair;

		// Token: 0x04000214 RID: 532
		private bool _disposed;
	}
}

using System;

namespace CRS.Utilities.CustomNaCl
{
	// Token: 0x0200005D RID: 93
	public class KeyPairGL : IDisposable
	{
		// Token: 0x060002BE RID: 702 RVA: 0x000137C7 File Offset: 0x000119C7
		public KeyPairGL(byte[] publicKey, byte[] privateKey)
		{
			this._publicKey = publicKey;
			this._privateKey = privateKey;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000137E0 File Offset: 0x000119E0
		~KeyPairGL()
		{
			this.Dispose();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0001380C File Offset: 0x00011A0C
		public byte[] PublicKey
		{
			get
			{
				return this._publicKey;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00013814 File Offset: 0x00011A14
		public byte[] PrivateKey
		{
			get
			{
				byte[] array = new byte[this._privateKey.Length];
				Array.Copy(this._privateKey, array, array.Length);
				return array;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001383F File Offset: 0x00011A3F
		public void Dispose()
		{
			if (this._privateKey != null && this._privateKey.Length != 0)
			{
				Array.Clear(this._privateKey, 0, this._privateKey.Length);
			}
		}

		// Token: 0x0400027C RID: 636
		private readonly byte[] _publicKey;

		// Token: 0x0400027D RID: 637
		private readonly byte[] _privateKey;
	}
}

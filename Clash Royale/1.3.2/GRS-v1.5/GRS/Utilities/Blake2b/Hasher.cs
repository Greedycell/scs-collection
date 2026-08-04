using System;
using System.Security.Cryptography;

namespace CRS.Utilities.Blake2b
{
	// Token: 0x02000066 RID: 102
	public abstract class Hasher
	{
		// Token: 0x060002F9 RID: 761
		public abstract void Init();

		// Token: 0x060002FA RID: 762
		public abstract byte[] Finish();

		// Token: 0x060002FB RID: 763
		public abstract void Update(byte[] data, int start, int count);

		// Token: 0x060002FC RID: 764 RVA: 0x00016EB7 File Offset: 0x000150B7
		public void Update(byte[] data)
		{
			this.Update(data, 0, data.Length);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00016EC4 File Offset: 0x000150C4
		public HashAlgorithm AsHashAlgorithm()
		{
			return new Hasher.HashAlgorithmAdapter(this);
		}

		// Token: 0x02000102 RID: 258
		internal class HashAlgorithmAdapter : HashAlgorithm
		{
			// Token: 0x06000629 RID: 1577 RVA: 0x00021A9D File Offset: 0x0001FC9D
			public HashAlgorithmAdapter(Hasher hasher)
			{
				this._hasher = hasher;
			}

			// Token: 0x0600062A RID: 1578 RVA: 0x00021AAC File Offset: 0x0001FCAC
			protected override void HashCore(byte[] array, int ibStart, int cbSize)
			{
				this._hasher.Update(array, ibStart, cbSize);
			}

			// Token: 0x0600062B RID: 1579 RVA: 0x00021ABC File Offset: 0x0001FCBC
			protected override byte[] HashFinal()
			{
				return this._hasher.Finish();
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x00021AC9 File Offset: 0x0001FCC9
			public override void Initialize()
			{
				this._hasher.Init();
			}

			// Token: 0x0400048A RID: 1162
			private readonly Hasher _hasher;
		}
	}
}

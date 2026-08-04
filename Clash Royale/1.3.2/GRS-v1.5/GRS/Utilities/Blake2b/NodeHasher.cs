using System;

namespace CRS.Utilities.Blake2b
{
	// Token: 0x02000067 RID: 103
	public abstract class NodeHasher
	{
		// Token: 0x060002FF RID: 767
		public abstract void Init(int depth, long nodeOffset);

		// Token: 0x06000300 RID: 768
		public abstract byte[] Finish(bool isEndOfLayer);

		// Token: 0x06000301 RID: 769
		public abstract void Update(byte[] data, int start, int count);

		// Token: 0x06000302 RID: 770 RVA: 0x00016ECC File Offset: 0x000150CC
		public void Update(byte[] data)
		{
			this.Update(data, 0, data.Length);
		}
	}
}

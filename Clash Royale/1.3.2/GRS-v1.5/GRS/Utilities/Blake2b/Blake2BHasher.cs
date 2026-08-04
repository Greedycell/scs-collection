using System;

namespace CRS.Utilities.Blake2b
{
	// Token: 0x02000063 RID: 99
	internal class Blake2BHasher : Hasher
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x00016B2C File Offset: 0x00014D2C
		public Blake2BHasher(Blake2BConfig config)
		{
			if (config == null)
			{
				config = Blake2BHasher.DefaultConfig;
			}
			this.rawConfig = Blake2IvBuilder.ConfigB(config, null);
			if (config.Key != null && config.Key.Length != 0)
			{
				this.key = new byte[128];
				Array.Copy(config.Key, this.key, config.Key.Length);
			}
			this.outputSizeInBytes = config.OutputSizeInBytes;
			this.Init();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00016BAD File Offset: 0x00014DAD
		public override void Init()
		{
			this.core.Initialize(this.rawConfig);
			if (this.key != null)
			{
				this.core.HashCore(this.key, 0, this.key.Length);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00016BE4 File Offset: 0x00014DE4
		public override byte[] Finish()
		{
			byte[] array = this.core.HashFinal();
			if (this.outputSizeInBytes != array.Length)
			{
				byte[] array2 = new byte[this.outputSizeInBytes];
				Array.Copy(array, array2, array2.Length);
				return array2;
			}
			return array;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00016C21 File Offset: 0x00014E21
		public override void Update(byte[] data, int start, int count)
		{
			this.core.HashCore(data, start, count);
		}

		// Token: 0x0400029E RID: 670
		private static readonly Blake2BConfig DefaultConfig = new Blake2BConfig();

		// Token: 0x0400029F RID: 671
		private readonly Blake2BCore core = new Blake2BCore();

		// Token: 0x040002A0 RID: 672
		private readonly byte[] key;

		// Token: 0x040002A1 RID: 673
		private readonly int outputSizeInBytes;

		// Token: 0x040002A2 RID: 674
		private readonly ulong[] rawConfig;
	}
}

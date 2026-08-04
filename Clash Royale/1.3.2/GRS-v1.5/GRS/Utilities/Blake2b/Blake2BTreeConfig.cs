using System;

namespace CRS.Utilities.Blake2b
{
	// Token: 0x02000064 RID: 100
	public sealed class Blake2BTreeConfig : ICloneable
	{
		// Token: 0x060002EA RID: 746 RVA: 0x00016C3D File Offset: 0x00014E3D
		public Blake2BTreeConfig()
		{
			this.IntermediateHashSize = 64;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00016C4D File Offset: 0x00014E4D
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00016C55 File Offset: 0x00014E55
		public int IntermediateHashSize { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00016C5E File Offset: 0x00014E5E
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00016C66 File Offset: 0x00014E66
		public int MaxHeight { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00016C6F File Offset: 0x00014E6F
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00016C77 File Offset: 0x00014E77
		public long LeafSize { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00016C80 File Offset: 0x00014E80
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00016C88 File Offset: 0x00014E88
		public int FanOut { get; set; }

		// Token: 0x060002F3 RID: 755 RVA: 0x00016C91 File Offset: 0x00014E91
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00016C99 File Offset: 0x00014E99
		public Blake2BTreeConfig Clone()
		{
			return new Blake2BTreeConfig
			{
				IntermediateHashSize = this.IntermediateHashSize,
				MaxHeight = this.MaxHeight,
				LeafSize = this.LeafSize,
				FanOut = this.FanOut
			};
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00016CD0 File Offset: 0x00014ED0
		public static Blake2BTreeConfig CreateInterleaved(int parallelism)
		{
			return new Blake2BTreeConfig
			{
				FanOut = parallelism,
				MaxHeight = 2,
				IntermediateHashSize = 64
			};
		}
	}
}

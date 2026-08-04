using System;

namespace CRS.Utilities.Blake2b
{
	// Token: 0x02000061 RID: 97
	public sealed class Blake2BConfig : ICloneable
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00013A59 File Offset: 0x00011C59
		public Blake2BConfig()
		{
			this.OutputSizeInBytes = 64;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00013A69 File Offset: 0x00011C69
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00013A71 File Offset: 0x00011C71
		public byte[] Personalization { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00013A7A File Offset: 0x00011C7A
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00013A82 File Offset: 0x00011C82
		public byte[] Salt { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00013A8B File Offset: 0x00011C8B
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00013A93 File Offset: 0x00011C93
		public byte[] Key { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00013A9C File Offset: 0x00011C9C
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00013AA4 File Offset: 0x00011CA4
		public int OutputSizeInBytes { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00013AAD File Offset: 0x00011CAD
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00013AB7 File Offset: 0x00011CB7
		public int OutputSizeInBits
		{
			get
			{
				return this.OutputSizeInBytes * 8;
			}
			set
			{
				if (value % 8 == 0)
				{
					throw new ArgumentException("Output size must be a multiple of 8 bits");
				}
				this.OutputSizeInBytes = value / 8;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00013AD2 File Offset: 0x00011CD2
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00013ADC File Offset: 0x00011CDC
		public Blake2BConfig Clone()
		{
			Blake2BConfig blake2BConfig = new Blake2BConfig();
			blake2BConfig.OutputSizeInBytes = this.OutputSizeInBytes;
			if (this.Key != null)
			{
				blake2BConfig.Key = (byte[])this.Key.Clone();
			}
			if (this.Personalization != null)
			{
				blake2BConfig.Personalization = (byte[])this.Personalization.Clone();
			}
			if (this.Salt != null)
			{
				blake2BConfig.Salt = (byte[])this.Salt.Clone();
			}
			return blake2BConfig;
		}
	}
}

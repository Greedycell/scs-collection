using System;

namespace CRS.Utilities.Blake2b
{
	// Token: 0x02000065 RID: 101
	internal static class Blake2IvBuilder
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x00016CF0 File Offset: 0x00014EF0
		public static ulong[] ConfigB(Blake2BConfig config, Blake2BTreeConfig treeConfig)
		{
			bool flag = treeConfig == null;
			if (flag)
			{
				treeConfig = Blake2IvBuilder.SequentialTreeConfig;
			}
			ulong[] array = new ulong[8];
			//new ulong[8];
			if ((config.OutputSizeInBytes <= 0) | (config.OutputSizeInBytes > 64))
			{
				throw new ArgumentOutOfRangeException("config.OutputSize");
			}
			array[0] |= (ulong)config.OutputSizeInBytes;
			if (config.Key != null)
			{
				if (config.Key.Length > 64)
				{
					throw new ArgumentException("config.Key", "Key too long");
				}
				array[0] |= (ulong)((ulong)config.Key.Length << 8);
			}
			array[0] |= (ulong)((ulong)treeConfig.FanOut << 16);
			array[0] |= (ulong)((ulong)treeConfig.MaxHeight << 24);
			array[0] |= (ulong)((uint)treeConfig.LeafSize) << 32;
			if (!flag && (treeConfig.IntermediateHashSize <= 0 || treeConfig.IntermediateHashSize > 64))
			{
				throw new ArgumentOutOfRangeException("treeConfig.TreeIntermediateHashSize");
			}
			array[2] |= (ulong)((ulong)treeConfig.IntermediateHashSize << 8);
			if (config.Salt != null)
			{
				if (config.Salt.Length != 16)
				{
					throw new ArgumentException("config.Salt has invalid length");
				}
				array[4] = Blake2BCore.BytesToUInt64(config.Salt, 0);
				array[5] = Blake2BCore.BytesToUInt64(config.Salt, 8);
			}
			if (config.Personalization != null)
			{
				if (config.Personalization.Length != 16)
				{
					throw new ArgumentException("config.Personalization has invalid length");
				}
				array[6] = Blake2BCore.BytesToUInt64(config.Personalization, 0);
				array[6] = Blake2BCore.BytesToUInt64(config.Personalization, 8);
			}
			return array;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00016E78 File Offset: 0x00015078
		public static void ConfigBSetNode(ulong[] rawConfig, byte depth, ulong nodeOffset)
		{
			rawConfig[1] = nodeOffset;
			rawConfig[2] = (rawConfig[2] & 18446744073709551360UL) | (ulong)depth;
		}

		// Token: 0x040002A7 RID: 679
		private static readonly Blake2BTreeConfig SequentialTreeConfig = new Blake2BTreeConfig
		{
			IntermediateHashSize = 0,
			LeafSize = 0L,
			FanOut = 1,
			MaxHeight = 1
		};
	}
}

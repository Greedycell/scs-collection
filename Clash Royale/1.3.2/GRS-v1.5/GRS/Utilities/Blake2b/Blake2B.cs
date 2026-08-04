using System;

namespace CRS.Utilities.Blake2b
{
	// Token: 0x02000060 RID: 96
	public static class Blake2B
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x00013A0A File Offset: 0x00011C0A
		public static Hasher Create()
		{
			return Blake2B.Create(new Blake2BConfig());
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00013A16 File Offset: 0x00011C16
		public static Hasher Create(Blake2BConfig config)
		{
			return new Blake2BHasher(config);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00013A1E File Offset: 0x00011C1E
		public static byte[] ComputeHash(byte[] data, int start, int count)
		{
			return Blake2B.ComputeHash(data, start, count, null);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00013A29 File Offset: 0x00011C29
		public static byte[] ComputeHash(byte[] data)
		{
			return Blake2B.ComputeHash(data, 0, data.Length, null);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00013A36 File Offset: 0x00011C36
		public static byte[] ComputeHash(byte[] data, Blake2BConfig config)
		{
			return Blake2B.ComputeHash(data, 0, data.Length, config);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00013A43 File Offset: 0x00011C43
		public static byte[] ComputeHash(byte[] data, int start, int count, Blake2BConfig config)
		{
			Hasher hasher = Blake2B.Create(config);
			hasher.Update(data, start, count);
			return hasher.Finish();
		}
	}
}

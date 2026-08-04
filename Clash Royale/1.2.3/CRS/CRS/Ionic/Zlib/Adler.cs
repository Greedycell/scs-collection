using System;

namespace Ionic.Zlib
{
	// Token: 0x02000018 RID: 24
	public sealed class Adler
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000096A8 File Offset: 0x000078A8
		public static uint Adler32(uint adler, byte[] buf, int index, int len)
		{
			if (buf == null)
			{
				return 1U;
			}
			uint num = adler & 65535U;
			uint num2 = (adler >> 16) & 65535U;
			while (len > 0)
			{
				int i = ((len < Adler.NMAX) ? len : Adler.NMAX);
				len -= i;
				while (i >= 16)
				{
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					i -= 16;
				}
				if (i != 0)
				{
					do
					{
						num += (uint)buf[index++];
						num2 += num;
					}
					while (--i != 0);
				}
				num %= Adler.BASE;
				num2 %= Adler.BASE;
			}
			return (num2 << 16) | num;
		}

		// Token: 0x04000111 RID: 273
		private static readonly uint BASE = 65521U;

		// Token: 0x04000112 RID: 274
		private static readonly int NMAX = 5552;
	}
}

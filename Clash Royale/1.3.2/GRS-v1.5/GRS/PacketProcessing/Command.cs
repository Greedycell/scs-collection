using System;
using System.Collections.Generic;
using CRS.Logic;

namespace CRS.PacketProcessing
{
	// Token: 0x0200006A RID: 106
	internal class Command
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00017BBC File Offset: 0x00015DBC
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00017BC4 File Offset: 0x00015DC4
		internal int Depth { get; set; }

		// Token: 0x06000324 RID: 804 RVA: 0x00017BCD File Offset: 0x00015DCD
		public virtual byte[] Encode()
		{
			return new List<byte>().ToArray();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000123B6 File Offset: 0x000105B6
		public virtual void Execute(Level level)
		{
		}

		// Token: 0x040002B5 RID: 693
		public const int MaxEmbeddedDepth = 10;
	}
}

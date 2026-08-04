using System;
using System.Collections.Generic;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	// Token: 0x0200008C RID: 140
	internal class Command
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x000121E7 File Offset: 0x000103E7
		// (set) Token: 0x060003BA RID: 954 RVA: 0x000121EF File Offset: 0x000103EF
		internal int Depth { get; set; }

		// Token: 0x060003BB RID: 955 RVA: 0x000121F8 File Offset: 0x000103F8
		public virtual byte[] Encode()
		{
			return new List<byte>().ToArray();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public virtual void Execute(Level level)
		{
		}

		// Token: 0x04000269 RID: 617
		public const int MaxEmbeddedDepth = 10;
	}
}

using System;
using System.Collections.Generic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000087 RID: 135
	internal class SectorHerboardMessage : Message
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x0001A233 File Offset: 0x00018433
		public SectorHerboardMessage(Device client)
			: base(client)
		{
			base.SetMessageType(21902);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001A248 File Offset: 0x00018448
		public override void Encode()
		{
			base.Encrypt(new List<byte> { 10, 242, 168, 235, 194, 10 }.ToArray());
		}
	}
}

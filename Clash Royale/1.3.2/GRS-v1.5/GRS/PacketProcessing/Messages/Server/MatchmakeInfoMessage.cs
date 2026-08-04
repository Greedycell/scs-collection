using System;
using System.Collections.Generic;
using CRS.Helpers;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200008D RID: 141
	internal class MatchmakeInfoMessage : Message
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0001A5E7 File Offset: 0x000187E7
		public MatchmakeInfoMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24107);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001A5FC File Offset: 0x000187FC
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(300); // Seconds
			base.Encrypt(list.ToArray());
		}
	}
}

using System;
using System.Collections.Generic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000086 RID: 134
	internal class RequestSectorHerboardMessage : Message
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0001A1B5 File Offset: 0x000183B5
		public RequestSectorHerboardMessage(Device client)
			: base(client)
		{
			base.SetMessageType(21903);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001A1CC File Offset: 0x000183CC
		public override void Encode()
		{
			base.Encrypt(new List<byte> { 30, 242, 168, 235, 194, 10 }.ToArray());
		}

		// Token: 0x040002D7 RID: 727
		private static string hex = "05BA84B2C206";
	}
}

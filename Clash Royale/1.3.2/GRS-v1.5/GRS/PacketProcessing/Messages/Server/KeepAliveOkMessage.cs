using System;
using System.Collections.Generic;
using CRS.PacketProcessing.Messages.Client;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000091 RID: 145
	internal class KeepAliveOkMessage : Message
	{
		// Token: 0x060003BC RID: 956 RVA: 0x0001A75C File Offset: 0x0001895C
		public KeepAliveOkMessage(Device client, KeepAliveMessage cka)
			: base(client)
		{
			base.SetMessageType(20108);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001A770 File Offset: 0x00018970
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}
	}
}

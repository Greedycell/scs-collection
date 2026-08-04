using System;
using System.Collections.Generic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200008C RID: 140
	internal class StopHomeLogicMessage : Message
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x0001A5B1 File Offset: 0x000187B1
		public StopHomeLogicMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24106);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001A5C8 File Offset: 0x000187C8
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}
	}
}

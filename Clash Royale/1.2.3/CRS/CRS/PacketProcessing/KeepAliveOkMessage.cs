using System;
using System.Collections.Generic;

namespace UCS.PacketProcessing
{
	// Token: 0x02000083 RID: 131
	internal class KeepAliveOkMessage : Message
	{
		// Token: 0x06000368 RID: 872 RVA: 0x000119D6 File Offset: 0x0000FBD6
		public KeepAliveOkMessage(Client client, KeepAliveMessage cka)
			: base(client)
		{
			base.SetMessageType(20108);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000119EC File Offset: 0x0000FBEC
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}
	}
}

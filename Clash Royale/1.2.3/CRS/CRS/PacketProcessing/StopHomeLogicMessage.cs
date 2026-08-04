using System;
using System.Collections.Generic;

namespace UCS.PacketProcessing
{
	// Token: 0x0200007E RID: 126
	internal class StopHomeLogicMessage : Message
	{
		// Token: 0x0600035C RID: 860 RVA: 0x0001184D File Offset: 0x0000FA4D
		public StopHomeLogicMessage(Client client)
			: base(client)
		{
			base.SetMessageType(24106);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00011864 File Offset: 0x0000FA64
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}
	}
}

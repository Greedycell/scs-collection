using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	// Token: 0x0200007F RID: 127
	internal class MatchmakeInfoMessage : Message
	{
		// Token: 0x0600035E RID: 862 RVA: 0x00011883 File Offset: 0x0000FA83
		public MatchmakeInfoMessage(Client client)
			: base(client)
		{
			base.SetMessageType(24107);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011898 File Offset: 0x0000FA98
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(0);
			base.Encrypt(list.ToArray());
		}
	}
}

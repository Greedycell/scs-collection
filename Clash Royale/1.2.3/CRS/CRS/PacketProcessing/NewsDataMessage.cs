using System;
using System.Collections.Generic;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	// Token: 0x02000086 RID: 134
	internal class NewsDataMessage : Message
	{
		// Token: 0x06000388 RID: 904 RVA: 0x00011D7C File Offset: 0x0000FF7C
		public NewsDataMessage(Client client, Level level)
			: base(client)
		{
			base.SetMessageType(24445);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00011D90 File Offset: 0x0000FF90
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}
	}
}

using System;
using System.Collections.Generic;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	// Token: 0x02000074 RID: 116
	internal class AllianceDataMessage : Message
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0001110A File Offset: 0x0000F30A
		public AllianceDataMessage(Client client, Level level)
			: base(client)
		{
			base.SetMessageType(24302);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00011120 File Offset: 0x0000F320
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}
	}
}

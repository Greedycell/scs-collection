using System;
using System.Collections.Generic;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000078 RID: 120
	internal class CancelChallengeOkMessage : Message
	{
		// Token: 0x06000367 RID: 871 RVA: 0x00019020 File Offset: 0x00017220
		public CancelChallengeOkMessage(Device client, Level level)
			: base(client)
		{
			base.SetMessageType(24124);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00019034 File Offset: 0x00017234
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}
	}
}

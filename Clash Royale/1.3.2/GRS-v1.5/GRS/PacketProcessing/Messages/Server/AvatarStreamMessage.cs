using System;
using System.Collections.Generic;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000076 RID: 118
	internal class AvatarStreamMessage : Message
	{
		// Token: 0x06000363 RID: 867 RVA: 0x00018F22 File Offset: 0x00017122
		public AvatarStreamMessage(Device client, Level level)
			: base(client)
		{
			base.SetMessageType(24411);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00018F38 File Offset: 0x00017138
		public override void Encode()
		{
			base.Encrypt(new List<byte> { 0 }.ToArray());
		}
	}
}

using System;
using System.Collections.Generic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000073 RID: 115
	internal class AvatarStreamEntryMessage : Message
	{
		// Token: 0x06000357 RID: 855 RVA: 0x00018D99 File Offset: 0x00016F99
		public AvatarStreamEntryMessage(Device client)
			: base(client)
		{
			base.SetMessageType(20000);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00018DB0 File Offset: 0x00016FB0
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}
	}
}

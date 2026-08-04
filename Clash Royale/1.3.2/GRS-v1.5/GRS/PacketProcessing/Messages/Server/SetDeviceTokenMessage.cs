using System;
using System.Collections.Generic;
using CRS.Helpers;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000079 RID: 121
	internal class SetDeviceTokenMessage : Message
	{
		// Token: 0x06000369 RID: 873 RVA: 0x00019053 File Offset: 0x00017253
		public SetDeviceTokenMessage(Device client, Level level)
			: base(client)
		{
			base.SetMessageType(20113);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00019068 File Offset: 0x00017268
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddString("12345678910112548950");
			base.Encrypt(list.ToArray());
		}
	}
}

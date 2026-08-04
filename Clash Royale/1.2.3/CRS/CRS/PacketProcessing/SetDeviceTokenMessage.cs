using System;
using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	// Token: 0x02000073 RID: 115
	internal class SetDeviceTokenMessage : Message
	{
		// Token: 0x06000336 RID: 822 RVA: 0x000110CB File Offset: 0x0000F2CB
		public SetDeviceTokenMessage(Client client, Level level)
			: base(client)
		{
			base.SetMessageType(20113);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000110E0 File Offset: 0x0000F2E0
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddString("12345678910112548950");
			base.Encrypt(list.ToArray());
		}
	}
}

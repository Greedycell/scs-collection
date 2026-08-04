using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	// Token: 0x02000075 RID: 117
	internal class AvailableServerCommandsMessage : Message
	{
		// Token: 0x0600033A RID: 826 RVA: 0x0001113F File Offset: 0x0000F33F
		public AvailableServerCommandsMessage(Client client)
			: base(client)
		{
			base.SetMessageType(24111);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00011154 File Offset: 0x0000F354
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(Helpers.Helpers.HexaToBytes("950301031A02008CAA9D17010000001A0D008CAA9D17010000001C01008CAA9D1701000000180200020E7F7F0000"));
			base.Encrypt(list.ToArray());
		}
	}
}

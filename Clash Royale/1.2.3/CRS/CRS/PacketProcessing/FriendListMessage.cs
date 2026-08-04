using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	// Token: 0x02000081 RID: 129
	internal class FriendListMessage : Message
	{
		// Token: 0x06000364 RID: 868 RVA: 0x00011955 File Offset: 0x0000FB55
		public FriendListMessage(Client client)
			: base(client)
		{
			base.SetMessageType(20105);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001196C File Offset: 0x0000FB6C
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(1);
			list.AddInt32(0);
			base.Encrypt(list.ToArray());
		}
	}
}

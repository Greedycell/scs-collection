using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	// Token: 0x02000088 RID: 136
	internal class SessionSuccess : Message
	{
		// Token: 0x06000390 RID: 912 RVA: 0x00011E5C File Offset: 0x0001005C
		public SessionSuccess(Client client, SessionRequest cka)
			: base(client)
		{
			base.SetMessageType(20100);
			this.SessionKey = Client.GenerateSessionKey();
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00011E7C File Offset: 0x0001007C
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(this.SessionKey.Length);
			list.AddRange(this.SessionKey);
			base.SetData(list.ToArray());
		}

		// Token: 0x04000258 RID: 600
		public byte[] SessionKey;
	}
}

using System;
using System.Collections.Generic;

namespace UCS.PacketProcessing
{
	// Token: 0x02000082 RID: 130
	internal class CancelAttackMessage : Message
	{
		// Token: 0x06000366 RID: 870 RVA: 0x00011999 File Offset: 0x0000FB99
		public CancelAttackMessage(Client client)
			: base(client)
		{
			base.SetMessageType(24125);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000119B0 File Offset: 0x0000FBB0
		public override void Encode()
		{
			base.Encrypt(new List<byte> { 1 }.ToArray());
		}
	}
}

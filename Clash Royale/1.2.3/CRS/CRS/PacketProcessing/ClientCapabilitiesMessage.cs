using System;
using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	// Token: 0x0200006B RID: 107
	internal class ClientCapabilitiesMessage : Message
	{
		// Token: 0x0600031B RID: 795 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public ClientCapabilitiesMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public override void Process(Level level)
		{
		}
	}
}

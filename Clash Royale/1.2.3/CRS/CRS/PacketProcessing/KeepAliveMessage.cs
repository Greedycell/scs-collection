using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000070 RID: 112
	internal class KeepAliveMessage : Message
	{
		// Token: 0x0600032E RID: 814 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public KeepAliveMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00010C8B File Offset: 0x0000EE8B
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new KeepAliveOkMessage(base.Client, this));
		}
	}
}

using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000062 RID: 98
	internal class AskForAllianceDataMessage : Message
	{
		// Token: 0x06000308 RID: 776 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public AskForAllianceDataMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public override void Decode()
		{
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00010A13 File Offset: 0x0000EC13
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new AllianceDataMessage(base.Client, level));
		}
	}
}

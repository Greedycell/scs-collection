using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x0200009F RID: 159
	internal class AskForCancelAttackMessage : Message
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public AskForCancelAttackMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001AE88 File Offset: 0x00019088
		public override void Process(Level level)
		{
			PacketManager.Send(new CancelAttackMessage(base.Client));
		}
	}
}

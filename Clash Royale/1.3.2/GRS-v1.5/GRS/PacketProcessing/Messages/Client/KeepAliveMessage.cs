using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000AF RID: 175
	internal class KeepAliveMessage : Message
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public KeepAliveMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001B60C File Offset: 0x0001980C
		public override void Process(Level level)
		{
			PacketManager.Send(new KeepAliveOkMessage(base.Client, this));
		}
	}
}

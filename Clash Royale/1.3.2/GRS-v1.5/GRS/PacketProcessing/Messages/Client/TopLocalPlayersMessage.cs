using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x02000098 RID: 152
	internal class TopLocalPlayersMessage : Message
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public TopLocalPlayersMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Decode()
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001ACD6 File Offset: 0x00018ED6
		public override void Process(Level level)
		{
			PacketManager.Send(new LocalPlayersMessage(base.Client));
		}
	}
}

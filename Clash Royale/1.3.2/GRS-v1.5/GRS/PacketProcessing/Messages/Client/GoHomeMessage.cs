using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000AD RID: 173
	internal class GoHomeMessage : Message
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public GoHomeMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001B583 File Offset: 0x00019783
		public override void Process(Level level)
		{
			PacketManager.Send(new OwnHomeDataMessage(base.Client, level));
		}
	}
}

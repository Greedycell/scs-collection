using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000A1 RID: 161
	internal class AskForJoinableAllianceListMessage : Message
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public AskForJoinableAllianceListMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Decode()
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001AF5F File Offset: 0x0001915F
		public override void Process(Level level)
		{
			PacketManager.Send(new JoinableAllianceListMessage(base.Client, level));
		}
	}
}

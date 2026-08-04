using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x0200009D RID: 157
	internal class AskForBattleReplayMessage : Message
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public AskForBattleReplayMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001AE14 File Offset: 0x00019014
		public override void Process(Level level)
		{
			PacketManager.Send(new BattleReplayMessage(base.Client));
		}
	}
}

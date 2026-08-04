using System;
using System.Collections.Generic;
using System.IO;
using CRS.Core;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000AE RID: 174
	internal class HomeLogicStoppedMessage : Message
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public HomeLogicStoppedMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001B598 File Offset: 0x00019798
		public override void Process(Level level)
		{
			PacketManager.Send(new UdpConnectionInfoMessage(base.Client));
			if (ResourcesManager.GetAllWaitingLevels().Count <= 0)
			{
				ResourcesManager.AddWaitingLevel(level);
				return;
			}
			KeyValuePair<long, Level> randomWaitingLevel = ResourcesManager.GetRandomWaitingLevel();
			ResourcesManager.AddBattle(base.Client, randomWaitingLevel.Value.GetClient());
			PacketManager.Send(new SectorStateMessage(randomWaitingLevel.Value.GetClient()));
			PacketManager.Send(new SectorStateMessage(base.Client));
		}
	}
}

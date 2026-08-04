using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000B7 RID: 183
	internal class StartMissionMessage : Message
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public StartMissionMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001BEDB File Offset: 0x0001A0DB
		public override void Process(Level level)
		{
			PacketManager.Send(new SectorStateNpcMessage(base.Client));
		}
	}
}

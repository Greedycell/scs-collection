using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x02000099 RID: 153
	internal class TopGlobalPlayersMessage : Message
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public TopGlobalPlayersMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Decode()
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001ACE8 File Offset: 0x00018EE8
		public override void Process(Level level)
		{
			PacketManager.Send(new GlobalPlayersMessage(base.Client));
		}
	}
}

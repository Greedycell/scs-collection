using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000B4 RID: 180
	internal class SectorCommandMessage : Message
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public SectorCommandMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Decode()
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001BCCC File Offset: 0x00019ECC
		public override void Process(Level level)
		{
			PacketManager.Send(new SectorHerboardMessage(base.Client));
		}
	}
}

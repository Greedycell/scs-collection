using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000B2 RID: 178
	internal class RequestSectorMessage : Message
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public RequestSectorMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Decode()
		{
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001BB94 File Offset: 0x00019D94
		public override void Process(Level level)
		{
			PacketManager.Send(new RequestSectorHerboardMessage(base.Client));
		}
	}
}

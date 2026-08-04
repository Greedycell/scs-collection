using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x0200009C RID: 156
	internal class AskForAvatarStreamMessage : Message
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public AskForAvatarStreamMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001AE01 File Offset: 0x00019001
		public override void Process(Level level)
		{
			PacketManager.Send(new AvatarStreamMessage(base.Client, level));
		}
	}
}

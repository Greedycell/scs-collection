using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000A5 RID: 165
	internal class CancelChallengeMessage : Message
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public CancelChallengeMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Decode()
		{
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001B000 File Offset: 0x00019200
		public override void Process(Level level)
		{
			PacketManager.Send(new CancelChallengeOkMessage(base.Client, level));
		}
	}
}

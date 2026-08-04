using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000A2 RID: 162
	internal class AskForNewsDataMessage : Message
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public AskForNewsDataMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Decode()
		{
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001AF72 File Offset: 0x00019172
		public override void Process(Level level)
		{
			PacketManager.Send(new NewsDataMessage(base.Client, level));
		}
	}
}

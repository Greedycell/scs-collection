using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000AC RID: 172
	internal class GetDeviceTokenMessage : Message
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public GetDeviceTokenMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Decode()
		{
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001B570 File Offset: 0x00019770
		public override void Process(Level level)
		{
			PacketManager.Send(new SetDeviceTokenMessage(base.Client, level));
		}
	}
}

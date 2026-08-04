using System;
using System.IO;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000A9 RID: 169
	internal class ClientCapabilitiesMessage : Message
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public ClientCapabilitiesMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Decode()
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Process(Level level)
		{
		}
	}
}

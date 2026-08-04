using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000A4 RID: 164
	internal class AskForTvContentMessage : Message
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public AskForTvContentMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001AF98 File Offset: 0x00019198
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				binaryReader.ReadByte();
				this.Arena = binaryReader.ReadByte();
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001AFE8 File Offset: 0x000191E8
		public override void Process(Level level)
		{
			PacketManager.Send(new RoyalTvContentMessage(base.Client, this.Arena));
		}

		// Token: 0x040002FF RID: 767
		public byte Arena;
	}
}

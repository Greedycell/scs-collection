using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000B6 RID: 182
	internal class SessionRequest : Message
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public SessionRequest(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001BDFC File Offset: 0x00019FFC
		public override void Decode()
		{
			using (PacketReader packetReader = new PacketReader(new MemoryStream(base.GetData())))
			{
				this.Unknown1 = packetReader.ReadInt32();
				this.Unknown2 = packetReader.ReadInt32();
				this.MajorVersion = packetReader.ReadInt32();
				this.Unknown4 = packetReader.ReadInt32();
				this.MinorVersion = packetReader.ReadInt32();
				this.Hash = packetReader.ReadString();
				this.Unknown6 = packetReader.ReadInt32();
				this.Unknown7 = packetReader.ReadInt32();
			}
			if (this.MajorVersion == 2 && this.MinorVersion == 1666)
			{
				base.Client.CState = 1;
				return;
			}
			base.Client.CState = 0;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001BEC8 File Offset: 0x0001A0C8
		public override void Process(Level level)
		{
			PacketManager.Send(new SessionSuccess(base.Client, this));
		}

		// Token: 0x04000339 RID: 825
		public string Hash;

		// Token: 0x0400033A RID: 826
		public int MajorVersion;

		// Token: 0x0400033B RID: 827
		public int MinorVersion;

		// Token: 0x0400033C RID: 828
		public int Unknown1;

		// Token: 0x0400033D RID: 829
		public int Unknown2;

		// Token: 0x0400033E RID: 830
		public int Unknown4;

		// Token: 0x0400033F RID: 831
		public int Unknown6;

		// Token: 0x04000340 RID: 832
		public int Unknown7;
	}
}

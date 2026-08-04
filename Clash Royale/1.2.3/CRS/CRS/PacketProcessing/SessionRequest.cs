using System;
using System.IO;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000072 RID: 114
	internal class SessionRequest : Message
	{
		// Token: 0x06000333 RID: 819 RVA: 0x00010FE0 File Offset: 0x0000F1E0
		public SessionRequest(Client client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00010FEC File Offset: 0x0000F1EC
		public override void Decode()
		{
			using (CoCSharpPacketReader coCSharpPacketReader = new CoCSharpPacketReader(new MemoryStream(base.GetData())))
			{
				this.Unknown1 = coCSharpPacketReader.ReadInt32();
				this.Unknown2 = coCSharpPacketReader.ReadInt32();
				this.MajorVersion = coCSharpPacketReader.ReadInt32();
				this.Unknown4 = coCSharpPacketReader.ReadInt32();
				this.MinorVersion = coCSharpPacketReader.ReadInt32();
				this.Hash = coCSharpPacketReader.ReadString();
				this.Unknown6 = coCSharpPacketReader.ReadInt32();
				this.Unknown7 = coCSharpPacketReader.ReadInt32();
			}
			if (this.MajorVersion == 2 && this.MinorVersion == 1507)
			{
				base.Client.CState = 1;
				return;
			}
			base.Client.CState = 0;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000110B8 File Offset: 0x0000F2B8
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new SessionSuccess(base.Client, this));
		}

		// Token: 0x0400022C RID: 556
		public string Hash;

		// Token: 0x0400022D RID: 557
		public int MajorVersion;

		// Token: 0x0400022E RID: 558
		public int MinorVersion;

		// Token: 0x0400022F RID: 559
		public int Unknown1;

		// Token: 0x04000230 RID: 560
		public int Unknown2;

		// Token: 0x04000231 RID: 561
		public int Unknown4;

		// Token: 0x04000232 RID: 562
		public int Unknown6;

		// Token: 0x04000233 RID: 563
		public int Unknown7;
	}
}

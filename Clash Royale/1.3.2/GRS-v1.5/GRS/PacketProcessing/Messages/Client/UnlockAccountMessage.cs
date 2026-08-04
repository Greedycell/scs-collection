using System;
using System.IO;
using CRS.Core.Network;
using CRS.Helpers;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000B8 RID: 184
	internal class UnlockAccountMessage : Message
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0001BEED File Offset: 0x0001A0ED
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0001BEF5 File Offset: 0x0001A0F5
		public string PlayerName { get; set; }

		// Token: 0x06000451 RID: 1105 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public UnlockAccountMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001BF00 File Offset: 0x0001A100
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.m_vId = binaryReader.ReadInt64();
				this.m_vToken = binaryReader.ReadScString();
				this.PlayerName = binaryReader.ReadScString();
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001BF60 File Offset: 0x0001A160
		public override void Process(Level level)
		{
			Console.WriteLine(string.Concat(new object[] { "[CRS]    ", this.m_vId, " ", this.m_vToken, " ", this.PlayerName }));
			PacketManager.Send(new SectorStateNpcMessage(base.Client));
		}

		// Token: 0x04000341 RID: 833
		private long m_vId;

		// Token: 0x04000342 RID: 834
		private string m_vToken;
	}
}

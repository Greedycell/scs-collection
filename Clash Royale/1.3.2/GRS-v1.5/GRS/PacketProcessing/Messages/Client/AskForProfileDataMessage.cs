using System;
using System.IO;
using CRS.Core.Network;
using CRS.Helpers;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x0200009A RID: 154
	internal class AskForProfileDataMessage : Message
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public AskForProfileDataMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001ACFC File Offset: 0x00018EFC
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.m_vId = binaryReader.ReadInt64WithEndian();
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0001AD44 File Offset: 0x00018F44
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0001AD4C File Offset: 0x00018F4C
		public long m_vId { get; set; }

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001AD55 File Offset: 0x00018F55
		public override void Process(Level level)
		{
			Console.WriteLine(this.m_vId);
			PacketManager.Send(new ProfileDataMessage(level.GetClient(), this.m_vId));
		}
	}
}

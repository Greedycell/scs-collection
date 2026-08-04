using System;
using System.IO;
using CRS.Core;
using CRS.Core.Network;
using CRS.Helpers;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x0200009B RID: 155
	internal class AskForAllianceDataMessage : Message
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public AskForAllianceDataMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001AD78 File Offset: 0x00018F78
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.m_vAllianceId = binaryReader.ReadInt64WithEndian();
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0001ADC0 File Offset: 0x00018FC0
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		public long m_vAllianceId { get; set; }

		// Token: 0x060003FA RID: 1018 RVA: 0x0001ADD4 File Offset: 0x00018FD4
		public override void Process(Level level)
		{
			Alliance alliance = ObjectManager.GetAlliance(this.m_vAllianceId);
			if (alliance != null)
			{
				PacketManager.Send(new AllianceDataMessage(base.Client, alliance));
			}
		}
	}
}

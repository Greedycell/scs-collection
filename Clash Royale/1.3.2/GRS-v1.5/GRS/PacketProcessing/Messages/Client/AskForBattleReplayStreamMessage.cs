using System;
using System.IO;
using CRS.Core.Network;
using CRS.Helpers;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x0200009E RID: 158
	internal class AskForBattleReplayStreamMessage : Message
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public AskForBattleReplayStreamMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001AE28 File Offset: 0x00019028
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.m_vID = binaryReader.ReadInt64WithEndian();
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001AE70 File Offset: 0x00019070
		public override void Process(Level level)
		{
			PacketManager.Send(new BattleReportStreamMessage(base.Client, this.m_vID));
		}

		// Token: 0x040002FD RID: 765
		private long m_vID;
	}
}

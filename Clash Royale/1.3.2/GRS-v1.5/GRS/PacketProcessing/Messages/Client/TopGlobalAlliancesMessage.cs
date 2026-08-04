using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x02000097 RID: 151
	internal class TopGlobalAlliancesMessage : Message
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public TopGlobalAlliancesMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Decode()
		{
			using (PacketReader packetReader = new PacketReader(new MemoryStream(base.GetData())))
			{
				this.IsLocal = packetReader.ReadBoolean();

				Console.WriteLine(this.IsLocal);
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001ACC4 File Offset: 0x00018EC4
		public override void Process(Level level)
		{
			if (this.IsLocal)
			{
				PacketManager.Send(new LocalAlliancesMessage(base.Client));
				Console.WriteLine("Local True");
			}
			else
            {
				PacketManager.Send(new GlobalAlliancesMessage(base.Client));
				Console.WriteLine("Local False");
			}
		}

		public bool IsLocal;
	}
}

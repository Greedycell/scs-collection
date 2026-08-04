using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000067 RID: 103
	internal class HomeLogicStoppedMessage : Message
	{
		// Token: 0x06000313 RID: 787 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public HomeLogicStoppedMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00010A6F File Offset: 0x0000EC6F
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new UdpConnectionInfoMessage(base.Client));
			PacketManager.ProcessOutgoingPacket(new SectorStateMessage(base.Client));
		}
	}
}

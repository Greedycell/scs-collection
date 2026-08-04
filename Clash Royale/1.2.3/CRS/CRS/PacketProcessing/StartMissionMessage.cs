using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000066 RID: 102
	internal class StartMissionMessage : Message
	{
		// Token: 0x06000311 RID: 785 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public StartMissionMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00010A5D File Offset: 0x0000EC5D
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new SectorStateNpcMessage(base.Client));
		}
	}
}

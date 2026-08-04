using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000068 RID: 104
	internal class AskForTvContentMessage : Message
	{
		// Token: 0x06000315 RID: 789 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public AskForTvContentMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00010A91 File Offset: 0x0000EC91
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new RoyalTvContentMessage(base.Client));
		}
	}
}

using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x0200006E RID: 110
	internal class AskForCancelAttackMessage : Message
	{
		// Token: 0x06000323 RID: 803 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public AskForCancelAttackMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00010A38 File Offset: 0x0000EC38
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new CancelAttackMessage(base.Client));
		}
	}
}

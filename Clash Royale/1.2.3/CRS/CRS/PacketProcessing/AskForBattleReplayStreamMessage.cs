using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000064 RID: 100
	internal class AskForBattleReplayStreamMessage : Message
	{
		// Token: 0x0600030D RID: 781 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public AskForBattleReplayStreamMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00010A38 File Offset: 0x0000EC38
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new CancelAttackMessage(base.Client));
		}
	}
}

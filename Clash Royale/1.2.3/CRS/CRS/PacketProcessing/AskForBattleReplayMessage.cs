using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000063 RID: 99
	internal class AskForBattleReplayMessage : Message
	{
		// Token: 0x0600030B RID: 779 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public AskForBattleReplayMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00010A26 File Offset: 0x0000EC26
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new HomeBattleReplayMessage(base.Client));
		}
	}
}

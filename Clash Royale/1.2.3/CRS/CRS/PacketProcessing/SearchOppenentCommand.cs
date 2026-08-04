using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000056 RID: 86
	internal class SearchOppenentCommand : Command
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x00010836 File Offset: 0x0000EA36
		public SearchOppenentCommand(BinaryReader br)
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001083E File Offset: 0x0000EA3E
		public override void Execute(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new MatchmakeInfoMessage(level.GetClient()));
			PacketManager.ProcessOutgoingPacket(new StopHomeLogicMessage(level.GetClient()));
		}

		// Token: 0x0400020A RID: 522
		private ulong Unknown;
	}
}

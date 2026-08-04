using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Commands
{
	// Token: 0x020000BF RID: 191
	internal class SearchOppenentCommand : Command
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x0001C01A File Offset: 0x0001A21A
		public SearchOppenentCommand(BinaryReader br)
		{
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001C081 File Offset: 0x0001A281
		public override void Execute(Level level)
		{
			PacketManager.Send(new MatchmakeInfoMessage(level.GetClient()));
			PacketManager.Send(new StopHomeLogicMessage(level.GetClient()));
		}

		// Token: 0x04000346 RID: 838
		private ulong Unknown;
	}
}

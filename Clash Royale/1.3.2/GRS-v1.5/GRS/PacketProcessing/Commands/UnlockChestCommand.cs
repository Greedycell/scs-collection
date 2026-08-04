using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Commands
{
	// Token: 0x020000C4 RID: 196
	internal class UnlockChestCommand : Command
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0001C01A File Offset: 0x0001A21A
		public UnlockChestCommand(BinaryReader br)
		{
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001C0A3 File Offset: 0x0001A2A3
		public override void Execute(Level level)
		{
			PacketManager.Send(new ChestDataMessage(level.GetClient()));
		}
	}
}

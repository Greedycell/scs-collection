using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Commands
{
	// Token: 0x020000C1 RID: 193
	internal class OpenFreeChestCommand : Command
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x0001C01A File Offset: 0x0001A21A
		public OpenFreeChestCommand(BinaryReader br)
		{
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001C0A3 File Offset: 0x0001A2A3
		public override void Execute(Level level)
		{
			PacketManager.Send(new ChestDataMessage(level.GetClient()));
		}
	}
}

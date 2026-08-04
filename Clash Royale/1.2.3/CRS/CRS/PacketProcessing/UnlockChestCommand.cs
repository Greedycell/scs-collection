using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x0200005A RID: 90
	internal class UnlockChestCommand : Command
	{
		// Token: 0x060002EF RID: 751 RVA: 0x00010836 File Offset: 0x0000EA36
		public UnlockChestCommand(BinaryReader br)
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001088D File Offset: 0x0000EA8D
		public override void Execute(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new ChestDataMessage(level.GetClient()));
		}
	}
}

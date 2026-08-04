using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Commands
{
	// Token: 0x020000C3 RID: 195
	internal class BuyChestCommand : Command
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x0001C01A File Offset: 0x0001A21A
		public BuyChestCommand(BinaryReader br)
		{
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0001C0B5 File Offset: 0x0001A2B5
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0001C0BC File Offset: 0x0001A2BC
		public static int Unknown1 { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0001C0CB File Offset: 0x0001A2CB
		public static int Tick { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001C0D3 File Offset: 0x0001A2D3
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0001C0DA File Offset: 0x0001A2DA
		public static byte[] Packet { get; set; }

		// Token: 0x06000473 RID: 1139 RVA: 0x0001C0A3 File Offset: 0x0001A2A3
		public override void Execute(Level level)
		{
			PacketManager.Send(new ChestDataMessage(level.GetClient()));
		}
	}
}

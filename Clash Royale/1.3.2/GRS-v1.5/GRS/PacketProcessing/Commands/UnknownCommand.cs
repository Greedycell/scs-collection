using System;
using System.IO;
using CRS.Logic;

namespace CRS.PacketProcessing.Commands
{
	// Token: 0x020000C9 RID: 201
	internal class UnknownCommand : Command
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x0001C01A File Offset: 0x0001A21A
		public UnknownCommand(BinaryReader br)
		{
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001C106 File Offset: 0x0001A306
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0001C10D File Offset: 0x0001A30D
		public static int Unknown1 { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001C115 File Offset: 0x0001A315
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0001C11C File Offset: 0x0001A31C
		public static int Tick { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001C124 File Offset: 0x0001A324
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0001C12B File Offset: 0x0001A32B
		public static byte[] Packet { get; set; }

		// Token: 0x06000485 RID: 1157 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Execute(Level level)
		{
		}
	}
}

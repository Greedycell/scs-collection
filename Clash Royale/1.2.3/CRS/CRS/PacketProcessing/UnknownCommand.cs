using System;
using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	// Token: 0x0200005E RID: 94
	internal class UnknownCommand : Command
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x00010836 File Offset: 0x0000EA36
		public UnknownCommand(BinaryReader br)
		{
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0001089F File Offset: 0x0000EA9F
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public static int Unknown1 { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002FA RID: 762 RVA: 0x000108AE File Offset: 0x0000EAAE
		// (set) Token: 0x060002FB RID: 763 RVA: 0x000108B5 File Offset: 0x0000EAB5
		public static int Tick { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000108BD File Offset: 0x0000EABD
		// (set) Token: 0x060002FD RID: 765 RVA: 0x000108C4 File Offset: 0x0000EAC4
		public static byte[] Packet { get; set; }

		// Token: 0x060002FE RID: 766 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public override void Execute(Level level)
		{
		}
	}
}

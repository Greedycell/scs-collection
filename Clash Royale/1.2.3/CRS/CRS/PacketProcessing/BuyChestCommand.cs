using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000059 RID: 89
	internal class BuyChestCommand : Command
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x00010836 File Offset: 0x0000EA36
		public BuyChestCommand(BinaryReader br)
		{
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00010860 File Offset: 0x0000EA60
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00010867 File Offset: 0x0000EA67
		public static int Unknown1 { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0001086F File Offset: 0x0000EA6F
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00010876 File Offset: 0x0000EA76
		public static int Tick { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0001087E File Offset: 0x0000EA7E
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00010885 File Offset: 0x0000EA85
		public static byte[] Packet { get; set; }

		// Token: 0x060002EE RID: 750 RVA: 0x0001088D File Offset: 0x0000EA8D
		public override void Execute(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new ChestDataMessage(level.GetClient()));
		}
	}
}

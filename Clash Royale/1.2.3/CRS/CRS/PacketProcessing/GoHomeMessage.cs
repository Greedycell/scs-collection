using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000065 RID: 101
	internal class GoHomeMessage : Message
	{
		// Token: 0x0600030F RID: 783 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public GoHomeMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00010A4A File Offset: 0x0000EC4A
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new OwnHomeDataMessage(base.Client, level));
		}
	}
}

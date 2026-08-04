using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x0200006C RID: 108
	internal class AskForNewsDataMessage : Message
	{
		// Token: 0x0600031D RID: 797 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public AskForNewsDataMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public override void Decode()
		{
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00010AB5 File Offset: 0x0000ECB5
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new NewsDataMessage(base.Client, level));
		}
	}
}

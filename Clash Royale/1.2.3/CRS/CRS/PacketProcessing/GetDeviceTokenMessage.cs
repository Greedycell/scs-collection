using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x02000061 RID: 97
	internal class GetDeviceTokenMessage : Message
	{
		// Token: 0x06000305 RID: 773 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public GetDeviceTokenMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public override void Decode()
		{
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00010A00 File Offset: 0x0000EC00
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new SetDeviceTokenMessage(base.Client, level));
		}
	}
}

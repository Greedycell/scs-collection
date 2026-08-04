using System;
using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	// Token: 0x02000069 RID: 105
	internal class AskForAvatarStreamEntryMessage : Message
	{
		// Token: 0x06000317 RID: 791 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public AskForAvatarStreamEntryMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public override void Process(Level level)
		{
		}
	}
}

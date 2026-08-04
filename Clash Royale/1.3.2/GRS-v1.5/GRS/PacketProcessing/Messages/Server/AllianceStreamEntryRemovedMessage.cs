using System;
using System.Collections.Generic;
using CRS.Logic.StreamEntry;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200007D RID: 125
	internal class AllianceStreamEntryRemovedMessage : Message
	{
		// Token: 0x06000374 RID: 884 RVA: 0x000192AC File Offset: 0x000174AC
		public AllianceStreamEntryRemovedMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24318);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000192C0 File Offset: 0x000174C0
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000192DF File Offset: 0x000174DF
		public void SetStreamEntry(StreamEntry entry)
		{
			this.m_vStreamEntry = entry;
		}

		// Token: 0x040002C9 RID: 713
		private StreamEntry m_vStreamEntry;
	}
}

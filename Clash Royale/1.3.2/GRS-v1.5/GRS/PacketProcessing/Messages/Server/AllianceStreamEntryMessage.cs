using System;
using System.Collections.Generic;
using CRS.Helpers;
using CRS.Logic.StreamEntry;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200007C RID: 124
	internal class AllianceStreamEntryMessage : Message
	{
		// Token: 0x06000371 RID: 881 RVA: 0x00019248 File Offset: 0x00017448
		public AllianceStreamEntryMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24312);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001925C File Offset: 0x0001745C
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.Add(2);
			list.AddRange(Helpers.Helpers.HexaToBytes("0390EDBAEB01"));
			list.AddRange(this.m_vStreamEntry.Encode());
			base.Encrypt(list.ToArray());
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000192A3 File Offset: 0x000174A3
		public void SetStreamEntry(StreamEntry entry)
		{
			this.m_vStreamEntry = entry;
		}

		// Token: 0x040002C8 RID: 712
		private StreamEntry m_vStreamEntry;
	}
}

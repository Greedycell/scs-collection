using System;
using System.Collections.Generic;
using System.Linq;
using CRS.Helpers;
using CRS.Logic;
using CRS.Logic.StreamEntry;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200007B RID: 123
	internal class AllianceStreamMessage : Message
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0001917C File Offset: 0x0001737C
		public AllianceStreamMessage(Device client, Alliance alliance)
			: base(client)
		{
			base.SetMessageType(24311);
			this.m_vAlliance = alliance;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00019198 File Offset: 0x00017398
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			List<StreamEntry> list2 = this.m_vAlliance.GetChatMessages().ToList<StreamEntry>();
			list.Add((byte)list2.Count);
			int num = 0;
			foreach (StreamEntry streamEntry in list2)
			{
				list.Add(2);
				num++;
				list.AddRange(Helpers.Helpers.HexaToBytes("90EDBAEB01"));
				list.Add((byte)num++);
				list.AddRange(streamEntry.Encode());
			}
			base.Encrypt(list.ToArray());
		}

		// Token: 0x040002C7 RID: 711
		private readonly Alliance m_vAlliance;
	}
}

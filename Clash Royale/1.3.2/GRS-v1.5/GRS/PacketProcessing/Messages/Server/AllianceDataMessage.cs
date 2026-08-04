using System;
using System.Collections.Generic;
using CRS.Helpers;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200007A RID: 122
	internal class AllianceDataMessage : Message
	{
		// Token: 0x0600036B RID: 875 RVA: 0x00019092 File Offset: 0x00017292
		public AllianceDataMessage(Device client, Alliance alliance)
			: base(client)
		{
			base.SetMessageType(24301);
			this.m_vAlliance = alliance;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600036C RID: 876 RVA: 0x000190AD File Offset: 0x000172AD
		// (set) Token: 0x0600036D RID: 877 RVA: 0x000190B5 File Offset: 0x000172B5
		public Alliance m_vAlliance { get; set; }

		// Token: 0x0600036E RID: 878 RVA: 0x000190C0 File Offset: 0x000172C0
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			List<AllianceMemberEntry> allianceMembers = this.m_vAlliance.GetAllianceMembers();
			list.AddRange(this.m_vAlliance.EncodeFullEntry());
			list.Add((byte)allianceMembers.Count);
			foreach (AllianceMemberEntry allianceMemberEntry in allianceMembers)
			{
				list.AddInt32(1);
				list.AddRange(Helpers.Helpers.HexaToBytes("0000DE74000000103135363536373732"));
				list.AddRange(allianceMemberEntry.Encode());
				list.AddInt32(0);
				list.Add(0);
				list.Add(0);
			}
			base.Encrypt(list.ToArray());
		}
	}
}

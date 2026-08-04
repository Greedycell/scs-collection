using System;
using System.Collections.Generic;
using CRS.Core;
using CRS.Helpers;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000085 RID: 133
	internal class LeaveAllianceOkMessage : Message
	{
		// Token: 0x0600039B RID: 923 RVA: 0x0001A122 File Offset: 0x00018322
		public LeaveAllianceOkMessage(Device client, long alliance)
			: base(client)
		{
			base.SetMessageType(24111);
			this.m_vAlliance = alliance;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001A13D File Offset: 0x0001833D
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0001A145 File Offset: 0x00018345
		public long m_vAlliance { get; set; }

		// Token: 0x0600039E RID: 926 RVA: 0x0001A150 File Offset: 0x00018350
		public override void Encode()
		{
			Alliance alliance = ObjectManager.GetAlliance(this.m_vAlliance);
			List<byte> list = new List<byte>();
			list.Add(144);
			list.Add(3);
			list.AddInt64(alliance.GetAllianceId());
			list.AddString(alliance.GetAllianceName());
			list.AddRange(Helpers.Helpers.HexaToBytes("10B10200087F7F0000"));
			base.Encrypt(list.ToArray());
		}
	}
}

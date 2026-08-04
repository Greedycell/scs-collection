using System;
using System.Collections.Generic;
using CRS.Core;
using CRS.Helpers;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000084 RID: 132
	internal class JoinAllianceMessage : Message
	{
		// Token: 0x06000397 RID: 919 RVA: 0x0001A075 File Offset: 0x00018275
		public JoinAllianceMessage(Device client, long alliance)
			: base(client)
		{
			base.SetMessageType(24111);
			this.m_vAlliance = alliance;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0001A090 File Offset: 0x00018290
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0001A098 File Offset: 0x00018298
		public long m_vAlliance { get; set; }

		// Token: 0x0600039A RID: 922 RVA: 0x0001A0A4 File Offset: 0x000182A4
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			Alliance alliance = ObjectManager.GetAlliance(this.m_vAlliance);
			list.Add(145);
			list.Add(3);
			list.AddInt64(alliance.GetAllianceId());
			list.AddString(alliance.GetAllianceName());
			list.Add(16);
			list.AddRange(Message.AddVInt(alliance.GetAllianceBadgeData()));
			list.AddRange(Helpers.Helpers.HexaToBytes("0101047F7F0000"));
			base.Encrypt(list.ToArray());
		}
	}
}

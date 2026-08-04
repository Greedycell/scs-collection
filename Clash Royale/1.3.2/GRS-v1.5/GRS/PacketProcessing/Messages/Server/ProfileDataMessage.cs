using System;
using System.Collections.Generic;
using CRS.Helpers;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000074 RID: 116
	internal class ProfileDataMessage : Message
	{
		// Token: 0x06000359 RID: 857 RVA: 0x00018DCF File Offset: 0x00016FCF
		public ProfileDataMessage(Device client, long id)
			: base(client)
		{
			base.SetMessageType(24113);
			this.m_vId = id;
			this.m_vPlayer = client.GetLevel().GetPlayerAvatar();
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00018DFB File Offset: 0x00016FFB
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00018E03 File Offset: 0x00017003
		public ClientAvatar m_vPlayer { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00018E0C File Offset: 0x0001700C
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00018E14 File Offset: 0x00017014
		public long m_vId { get; set; }

		// Token: 0x0600035E RID: 862 RVA: 0x00018E20 File Offset: 0x00017020
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.Add(1);
			list.AddInt64(this.m_vId);
			list.AddRange(this.m_vPlayer.EncodeProfile());
			base.Encrypt(list.ToArray());
		}
	}
}

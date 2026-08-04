using System;
using System.Collections.Generic;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000095 RID: 149
	internal class OwnHomeDataMessage : Message
	{
		// Token: 0x060003DF RID: 991 RVA: 0x0001AB9D File Offset: 0x00018D9D
		public OwnHomeDataMessage(Device client, Level level)
			: base(client)
		{
			base.SetMessageType(24101);
			this.Player = level.GetPlayerAvatar();
			this.PlayerClient = client;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001ABC4 File Offset: 0x00018DC4
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0001ABCC File Offset: 0x00018DCC
		public ClientAvatar Player { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0001ABD5 File Offset: 0x00018DD5
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0001ABDD File Offset: 0x00018DDD
		public Device PlayerClient { get; set; }

		// Token: 0x060003E4 RID: 996 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(this.Player.Encode());
			base.Encrypt(list.ToArray());
		}
	}
}

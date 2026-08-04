using System;
using System.Collections.Generic;
using CRS.Core;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000090 RID: 144
	internal class CancelAttackMessage : Message
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0001A6F1 File Offset: 0x000188F1
		public CancelAttackMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24125);
			CancelAttackMessage.PlayerID = client.GetLevel().GetPlayerAvatar().GetId();
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0001A71A File Offset: 0x0001891A
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0001A721 File Offset: 0x00018921
		public static long PlayerID { get; set; }

		// Token: 0x060003BB RID: 955 RVA: 0x0001A72C File Offset: 0x0001892C
		public override void Encode()
		{
			ResourcesManager.DropWaitingLevel(CancelAttackMessage.PlayerID);
			base.Encrypt(new List<byte> { 1 }.ToArray());
		}
	}
}

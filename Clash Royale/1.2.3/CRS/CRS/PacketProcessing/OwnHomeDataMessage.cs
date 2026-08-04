using System;
using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	// Token: 0x02000087 RID: 135
	internal class OwnHomeDataMessage : Message
	{
		// Token: 0x0600038A RID: 906 RVA: 0x00011DAF File Offset: 0x0000FFAF
		public OwnHomeDataMessage(Client client, Level level)
			: base(client)
		{
			base.SetMessageType(24101);
			this.Player = level.GetPlayerAvatar();
			this.PlayerClient = client;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00011DD6 File Offset: 0x0000FFD6
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00011DDE File Offset: 0x0000FFDE
		public ClientAvatar Player { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00011DE7 File Offset: 0x0000FFE7
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00011DEF File Offset: 0x0000FFEF
		public Client PlayerClient { get; set; }

		// Token: 0x0600038F RID: 911 RVA: 0x00011DF8 File Offset: 0x0000FFF8
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(this.Player.Encode());
			list.AddInt32(this.PlayerClient.ClientSeed.ToString().Length);
			list.AddRange(BitConverter.GetBytes(this.PlayerClient.ClientSeed));
			base.Encrypt(list.ToArray());
		}
	}
}

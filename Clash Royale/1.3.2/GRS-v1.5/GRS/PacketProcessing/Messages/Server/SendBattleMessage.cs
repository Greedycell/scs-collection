using System;
using System.Collections.Generic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200008A RID: 138
	internal class SendBattleMessage : Message
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0001A4A7 File Offset: 0x000186A7
		public SendBattleMessage(Device client, byte value)
			: base(client)
		{
			base.SetMessageType(22952);
			this.m_vBattleCommand = value;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001A4C4 File Offset: 0x000186C4
		public override void Encode()
		{
			base.Encrypt(new List<byte>
			{
				3, 1, 174, 223, 0, 1, 177, 56, 0, 1,
				1
			}.ToArray());
		}

		// Token: 0x040002DA RID: 730
		private byte m_vBattleCommand;
	}
}

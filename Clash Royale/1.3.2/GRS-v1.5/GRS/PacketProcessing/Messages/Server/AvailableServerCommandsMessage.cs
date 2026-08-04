using System;
using System.Collections.Generic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200007E RID: 126
	internal class AvailableServerCommandsMessage : Message
	{
		// Token: 0x06000377 RID: 887 RVA: 0x000192E8 File Offset: 0x000174E8
		public AvailableServerCommandsMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24111);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000192FC File Offset: 0x000174FC
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(Message.AddVInt(this.m_vServerCommandId));
			list.AddRange(this.m_vCommand.Encode());
			base.Encrypt(list.ToArray());
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001933D File Offset: 0x0001753D
		public void SetCommand(Command c)
		{
			this.m_vCommand = c;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00019346 File Offset: 0x00017546
		public void SetCommandId(int id)
		{
			this.m_vServerCommandId = id;
		}

		// Token: 0x040002CA RID: 714
		private Command m_vCommand;

		// Token: 0x040002CB RID: 715
		private int m_vServerCommandId;
	}
}

using System;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	// Token: 0x0200008B RID: 139
	internal class GameOpCommand
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public virtual void Execute(Level level)
		{
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000121D6 File Offset: 0x000103D6
		public byte GetRequiredAccountPrivileges()
		{
			return this.m_vRequiredAccountPrivileges;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public static void SendCommandFailedMessage(Client c)
		{
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000121DE File Offset: 0x000103DE
		public void SetRequiredAccountPrivileges(byte level)
		{
			this.m_vRequiredAccountPrivileges = level;
		}

		// Token: 0x04000268 RID: 616
		private byte m_vRequiredAccountPrivileges;
	}
}

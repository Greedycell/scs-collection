using System;
using System.Collections.Generic;
using System.Text;

namespace UCS.PacketProcessing
{
	// Token: 0x02000078 RID: 120
	internal class AvatarNameChangeOkMessage : Message
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00011494 File Offset: 0x0000F694
		public AvatarNameChangeOkMessage(Client client)
			: base(client)
		{
			base.SetMessageType(24111);
			this.m_vAvatarName = "";
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000114B4 File Offset: 0x0000F6B4
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.Add(137);
			list.Add(3);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.Add((byte)this.m_vAvatarName.Length);
			list.AddRange(Encoding.Default.GetBytes(this.m_vAvatarName));
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.Add(1);
			list.Add(7);
			list.Add(127);
			list.Add(127);
			list.Add(0);
			list.Add(0);
			base.Encrypt(list.ToArray());
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001156A File Offset: 0x0000F76A
		public string GetAvatarName()
		{
			return this.m_vAvatarName;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00011572 File Offset: 0x0000F772
		public void SetAvatarName(string name)
		{
			this.m_vAvatarName = name;
		}

		// Token: 0x04000235 RID: 565
		private string m_vAvatarName;
	}
}

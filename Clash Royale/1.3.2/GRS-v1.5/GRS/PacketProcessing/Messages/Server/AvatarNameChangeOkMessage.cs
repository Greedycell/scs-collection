using System;
using System.Collections.Generic;
using System.Text;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000081 RID: 129
	internal class AvatarNameChangeOkMessage : Message
	{
		// Token: 0x0600038B RID: 907 RVA: 0x00019E41 File Offset: 0x00018041
		public AvatarNameChangeOkMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24111);
			this.m_vAvatarName = "";
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00019E60 File Offset: 0x00018060
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

		// Token: 0x0600038D RID: 909 RVA: 0x00019F16 File Offset: 0x00018116
		public string GetAvatarName()
		{
			return this.m_vAvatarName;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00019F1E File Offset: 0x0001811E
		public void SetAvatarName(string name)
		{
			this.m_vAvatarName = name;
		}

		// Token: 0x040002D1 RID: 721
		private string m_vAvatarName;
	}
}

using System;
using System.Collections.Generic;
using CRS.Helpers;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000075 RID: 117
	internal class AllianceListMessage : Message
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00018E63 File Offset: 0x00017063
		public AllianceListMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24310);
			this.m_vAlliances = new List<Alliance>();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00018E84 File Offset: 0x00017084
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddString(this.m_vSearchString);
			list.AddRange(Message.AddVInt(this.m_vAlliances.Count));
			foreach (Alliance alliance in this.m_vAlliances)
			{
				list.AddRange(alliance.EncodeFullEntry());
			}
			base.Encrypt(list.ToArray());
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00018F10 File Offset: 0x00017110
		public void SetAlliances(List<Alliance> alliances)
		{
			this.m_vAlliances = alliances;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00018F19 File Offset: 0x00017119
		public void SetSearchString(string searchString)
		{
			this.m_vSearchString = searchString;
		}

		// Token: 0x040002C2 RID: 706
		private List<Alliance> m_vAlliances;

		// Token: 0x040002C3 RID: 707
		private string m_vSearchString;
	}
}

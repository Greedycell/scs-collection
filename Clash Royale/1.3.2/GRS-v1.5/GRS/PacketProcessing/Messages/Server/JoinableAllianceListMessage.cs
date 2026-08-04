using System;
using System.Collections.Generic;
using CRS.Core;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000077 RID: 119
	internal class JoinableAllianceListMessage : Message
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00018F5E File Offset: 0x0001715E
		public JoinableAllianceListMessage(Device client, Level level)
			: base(client)
		{
			base.SetMessageType(24304);
			this.m_vAlliances = new List<Alliance>();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00018F88 File Offset: 0x00017188
		public override void Encode()
		{
			List<Alliance> inMemoryAlliances = ObjectManager.GetInMemoryAlliances();
			List<byte> list = new List<byte>();
			List<byte> list2 = new List<byte>();
			int num = 0;
			foreach (Alliance alliance in inMemoryAlliances)
			{
				list.AddRange(alliance.EncodeJoinableAlliance());
				num++;
				if (num >= this.m_vAllianceLimit)
				{
					break;
				}
			}
			list2.Add((byte)num);
			list2.AddRange(list.ToArray());
			base.Encrypt(list2.ToArray());
		}

		// Token: 0x040002C4 RID: 708
		private List<Alliance> m_vAlliances;

		// Token: 0x040002C5 RID: 709
		private int m_vAllianceLimit = 50;
	}
}

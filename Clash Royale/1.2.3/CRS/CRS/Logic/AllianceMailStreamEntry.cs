using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x0200009D RID: 157
	internal class AllianceMailStreamEntry : AvatarStreamEntry
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x00013D24 File Offset: 0x00011F24
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddInt32(2);
			list.AddString(this.m_vMessage);
			list.Add(1);
			list.AddInt64(this.m_vSenderId);
			list.AddInt64(this.m_vAllianceId);
			list.AddString(this.m_vAllianceName);
			list.AddInt32(this.m_vAllianceBadgeData);
			list.AddInt32(-1);
			return list.ToArray();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00013D98 File Offset: 0x00011F98
		public string GetMessage()
		{
			return this.m_vMessage;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00013DA0 File Offset: 0x00011FA0
		public override int GetStreamEntryType()
		{
			return 6;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00013DA3 File Offset: 0x00011FA3
		public void SetAllianceBadgeData(int data)
		{
			this.m_vAllianceBadgeData = data;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00013DAC File Offset: 0x00011FAC
		public void SetAllianceId(long id)
		{
			this.m_vAllianceId = id;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00013DB5 File Offset: 0x00011FB5
		public void SetAllianceName(string name)
		{
			this.m_vAllianceName = name;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00013DBE File Offset: 0x00011FBE
		public void SetMessage(string message)
		{
			this.m_vMessage = message;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00013DC7 File Offset: 0x00011FC7
		public void SetSenderId(long id)
		{
			this.m_vSenderId = id;
		}

		// Token: 0x040002AC RID: 684
		private int m_vAllianceBadgeData;

		// Token: 0x040002AD RID: 685
		private long m_vAllianceId;

		// Token: 0x040002AE RID: 686
		private string m_vAllianceName;

		// Token: 0x040002AF RID: 687
		private string m_vMessage;

		// Token: 0x040002B0 RID: 688
		private long m_vSenderId;
	}
}

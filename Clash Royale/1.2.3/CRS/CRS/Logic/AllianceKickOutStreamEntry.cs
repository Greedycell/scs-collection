using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x02000098 RID: 152
	internal class AllianceKickOutStreamEntry : AvatarStreamEntry
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x00013778 File Offset: 0x00011978
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddInt32(2);
			list.AddString(this.m_vMessage);
			list.AddInt64(this.m_vAllianceId);
			list.AddString(this.m_vAllianceName);
			list.AddInt32(this.m_vAllianceBadgeData);
			list.Add(1);
			list.AddInt32(41);
			list.AddInt32(8710265);
			return list.ToArray();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00013325 File Offset: 0x00011525
		public override int GetStreamEntryType()
		{
			return 5;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000137EC File Offset: 0x000119EC
		public void SetAllianceBadgeData(int data)
		{
			this.m_vAllianceBadgeData = data;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000137F5 File Offset: 0x000119F5
		public void SetAllianceId(long id)
		{
			this.m_vAllianceId = id;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000137FE File Offset: 0x000119FE
		public void SetAllianceName(string name)
		{
			this.m_vAllianceName = name;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00013807 File Offset: 0x00011A07
		public void SetMessage(string message)
		{
			this.m_vMessage = message;
		}

		// Token: 0x0400028D RID: 653
		private int m_vAllianceBadgeData;

		// Token: 0x0400028E RID: 654
		private long m_vAllianceId;

		// Token: 0x0400028F RID: 655
		private string m_vAllianceName;

		// Token: 0x04000290 RID: 656
		private string m_vMessage;
	}
}

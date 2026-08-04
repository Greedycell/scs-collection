using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CRS.Core;
using CRS.Core.Network;
using CRS.Helpers;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000B3 RID: 179
	internal class SearchAlliancesMessage : Message
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public SearchAlliancesMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.m_vSearchString = binaryReader.ReadScString();
				this.m_vAllianceOrigin = binaryReader.ReadInt32WithEndian();
				this.m_vMinimumAllianceMembers = binaryReader.ReadInt32WithEndian();
				this.m_vMaximumAllianceMembers = binaryReader.ReadInt32WithEndian();
				this.m_vAllianceScore = binaryReader.ReadInt32WithEndian();
				this.m_vShowOnlyJoinableAlliances = binaryReader.ReadByte();
				binaryReader.ReadInt32WithEndian();
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001BC34 File Offset: 0x00019E34
		public override void Process(Level level)
		{
			List<Alliance> inMemoryAlliances = ObjectManager.GetInMemoryAlliances();
			List<Alliance> list = new List<Alliance>();
			int num = 0;
			int num2 = 0;
			while (num2 < 20 && num < inMemoryAlliances.Count)
			{
				if (inMemoryAlliances[num].GetAllianceMembers().Count != 0 && inMemoryAlliances[num].GetAllianceName().Contains(this.m_vSearchString))
				{
					list.Add(inMemoryAlliances[num]);
					num2++;
				}
				num++;
			}
			list = list.ToList<Alliance>();
			AllianceListMessage allianceListMessage = new AllianceListMessage(base.Client);
			allianceListMessage.SetAlliances(list);
			allianceListMessage.SetSearchString(this.m_vSearchString);
			PacketManager.Send(allianceListMessage);
		}

		// Token: 0x04000327 RID: 807
		private const int m_vAllianceLimit = 20;

		// Token: 0x04000328 RID: 808
		private int m_vAllianceOrigin;

		// Token: 0x04000329 RID: 809
		private int m_vAllianceScore;

		// Token: 0x0400032A RID: 810
		private int m_vMaximumAllianceMembers;

		// Token: 0x0400032B RID: 811
		private int m_vMinimumAllianceMembers;

		// Token: 0x0400032C RID: 812
		private string m_vSearchString;

		// Token: 0x0400032D RID: 813
		private byte m_vShowOnlyJoinableAlliances;
	}
}

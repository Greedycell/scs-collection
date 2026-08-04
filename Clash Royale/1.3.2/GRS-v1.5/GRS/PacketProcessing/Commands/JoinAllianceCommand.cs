using System;
using System.Collections.Generic;
using System.IO;
using CRS.Logic;

namespace CRS.PacketProcessing.Commands
{
	// Token: 0x020000BE RID: 190
	internal class JoinAllianceCommand : Command
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x0001C01A File Offset: 0x0001A21A
		public JoinAllianceCommand()
		{
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001C022 File Offset: 0x0001A222
		public JoinAllianceCommand(BinaryReader br)
		{
			br.ReadInt64();
			br.ReadString();
			br.ReadInt32();
			br.ReadByte();
			br.ReadInt32();
			br.ReadInt32();
			br.ReadInt32();
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001C05B File Offset: 0x0001A25B
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(this.m_vAlliance.EncodeHeader());
			return list.ToArray();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001C078 File Offset: 0x0001A278
		public void SetAlliance(Alliance alliance)
		{
			this.m_vAlliance = alliance;
		}

		// Token: 0x04000345 RID: 837
		private Alliance m_vAlliance;
	}
}

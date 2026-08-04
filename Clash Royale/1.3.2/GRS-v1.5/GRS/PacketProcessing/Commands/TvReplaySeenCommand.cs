using System;
using System.IO;
using CRS.Logic;

namespace CRS.PacketProcessing.Commands
{
	// Token: 0x020000C8 RID: 200
	internal class TvReplaySeenCommand : Command
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x0001C0E2 File Offset: 0x0001A2E2
		public TvReplaySeenCommand(BinaryReader br)
		{
			br.ReadInt32();
			br.ReadInt32();
			br.ReadByte();
			br.ReadByte();
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Execute(Level level)
		{
		}
	}
}

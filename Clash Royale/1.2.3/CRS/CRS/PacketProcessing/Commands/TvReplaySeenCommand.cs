using System;
using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing.Commands
{
	// Token: 0x02000092 RID: 146
	internal class TvReplaySeenCommand : Command
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x000130A1 File Offset: 0x000112A1
		public TvReplaySeenCommand(BinaryReader br)
		{
			br.ReadInt32();
			br.ReadInt32();
			br.ReadByte();
			br.ReadByte();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public override void Execute(Level level)
		{
		}
	}
}

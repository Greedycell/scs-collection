using System;
using System.IO;
using CRS.Logic;

namespace CRS.PacketProcessing.Commands
{
	// Token: 0x020000B9 RID: 185
	internal class ChangeDeckCommand : Command
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x0001BFC5 File Offset: 0x0001A1C5
		public ChangeDeckCommand(BinaryReader br)
		{
			br.ReadInt32();
			br.ReadInt32();
			br.Read();
			this.Deck = br.Read() + 1;
			br.ReadInt32();
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0001BFF7 File Offset: 0x0001A1F7
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0001BFFF File Offset: 0x0001A1FF
		public int Deck { get; set; }

		// Token: 0x06000457 RID: 1111 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Execute(Level level)
		{
		}
	}
}

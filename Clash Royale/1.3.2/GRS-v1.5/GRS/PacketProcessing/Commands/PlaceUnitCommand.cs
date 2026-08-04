using System;
using System.IO;
using CRS.Logic;

namespace CRS.PacketProcessing.Commands
{
	// Token: 0x020000BA RID: 186
	internal class PlaceUnitCommand : Command
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x0001C008 File Offset: 0x0001A208
		public PlaceUnitCommand(BinaryReader br)
		{
			Console.WriteLine("PLACE UNIT COMMAND RUNNED !");
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Execute(Level level)
		{
		}
	}
}

using System;
using System.IO;
using CRS.Helpers;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000AB RID: 171
	internal class ExecuteCommandsMessage : Message
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public ExecuteCommandsMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001B464 File Offset: 0x00019664
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.Subtick = (uint)binaryReader.ReadVInt();
				this.Checksum = (uint)binaryReader.ReadVInt();
				this.NumberOfCommands = (uint)binaryReader.ReadVInt();
				if (this.NumberOfCommands > 0U)
				{
					this.NestedCommands = binaryReader.ReadBytes(base.GetLength());
				}
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001B4E0 File Offset: 0x000196E0
		public override void Process(Level level)
		{
			try
			{
				level.Tick();
				if (this.NumberOfCommands > 0U)
				{
					using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(this.NestedCommands)))
					{
						int num = 0;
						while ((long)num < (long)((ulong)this.NumberOfCommands))
						{
							object obj = CommandFactory.Read(binaryReader);
							if (obj == null)
							{
								break;
							}
							((Command)obj).Execute(level);
							num++;
						}
					}
				}
			}
			catch (Exception)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.ResetColor();
			}
		}

		// Token: 0x04000313 RID: 787
		public uint Checksum;

		// Token: 0x04000314 RID: 788
		public byte[] NestedCommands;

		// Token: 0x04000315 RID: 789
		public uint NumberOfCommands;

		// Token: 0x04000316 RID: 790
		public uint Subtick;
	}
}

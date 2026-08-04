using System;
using System.IO;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	// Token: 0x0200006D RID: 109
	internal class ExecuteCommandsMessage : Message
	{
		// Token: 0x06000320 RID: 800 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public ExecuteCommandsMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00010AC8 File Offset: 0x0000ECC8
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

		// Token: 0x06000322 RID: 802 RVA: 0x00010B44 File Offset: 0x0000ED44
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

		// Token: 0x04000217 RID: 535
		public uint Checksum;

		// Token: 0x04000218 RID: 536
		public byte[] NestedCommands;

		// Token: 0x04000219 RID: 537
		public uint NumberOfCommands;

		// Token: 0x0400021A RID: 538
		public uint Subtick;
	}
}

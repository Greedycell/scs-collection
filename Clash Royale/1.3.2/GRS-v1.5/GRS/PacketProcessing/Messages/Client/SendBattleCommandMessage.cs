using System;
using System.Collections.Generic;
using System.IO;
using CRS.Core;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000B5 RID: 181
	internal class SendBattleCommandMessage : Message
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public SendBattleCommandMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.Unknown1 = binaryReader.ReadByte();
				this.Unknown2 = binaryReader.ReadByte();
				this.Unknown3 = binaryReader.ReadByte();
				this.Unknown4 = binaryReader.ReadByte();
				this.Unknown5 = binaryReader.ReadByte();
				this.Unknown6 = binaryReader.ReadByte();
				this.Unknown7 = binaryReader.ReadByte();
				this.Unknown8 = binaryReader.ReadByte();
				this.Unknown9 = binaryReader.ReadByte();
				this.Unknown10 = binaryReader.ReadByte();
				this.Unknown11 = binaryReader.ReadByte();
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001BDA0 File Offset: 0x00019FA0
		public override void Process(Level level)
		{
			KeyValuePair<Device, Device> battle = ResourcesManager.GetBattle(base.Client);
			if (battle.Value.Equals(level.GetClient()))
			{
				PacketManager.Send(new SendBattleMessage(battle.Key, this.Unknown11));
				return;
			}
			PacketManager.Send(new SendBattleMessage(battle.Value, this.Unknown11));
		}

		// Token: 0x0400032E RID: 814
		private byte Unknown1;

		// Token: 0x0400032F RID: 815
		private byte Unknown2;

		// Token: 0x04000330 RID: 816
		private byte Unknown3;

		// Token: 0x04000331 RID: 817
		private byte Unknown4;

		// Token: 0x04000332 RID: 818
		private byte Unknown5;

		// Token: 0x04000333 RID: 819
		private byte Unknown6;

		// Token: 0x04000334 RID: 820
		private byte Unknown7;

		// Token: 0x04000335 RID: 821
		private byte Unknown8;

		// Token: 0x04000336 RID: 822
		private byte Unknown9;

		// Token: 0x04000337 RID: 823
		private byte Unknown10;

		// Token: 0x04000338 RID: 824
		private byte Unknown11;
	}
}

using System;
using System.IO;
using CRS.Core.Network;
using CRS.Helpers;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000A7 RID: 167
	internal class ChangeAvatarNameMessage : Message
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public ChangeAvatarNameMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001B0F0 File Offset: 0x000192F0
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0001B0F8 File Offset: 0x000192F8
		public string PlayerName { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0001B101 File Offset: 0x00019301
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0001B109 File Offset: 0x00019309
		public int PlayerNameLength { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001B112 File Offset: 0x00019312
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0001B11A File Offset: 0x0001931A
		public byte Unknown1 { get; set; }

		// Token: 0x0600041F RID: 1055 RVA: 0x0001B124 File Offset: 0x00019324
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.PlayerName = binaryReader.ReadScString();
				this.Unknown1 = binaryReader.ReadByte();
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001B178 File Offset: 0x00019378
		public override void Process(Level level)
		{
			level.GetPlayerAvatar().SetName(this.PlayerName);
			AvatarNameChangeOkMessage avatarNameChangeOkMessage = new AvatarNameChangeOkMessage(base.Client);
			avatarNameChangeOkMessage.SetAvatarName(this.PlayerName);
			PacketManager.Send(avatarNameChangeOkMessage);
		}
	}
}

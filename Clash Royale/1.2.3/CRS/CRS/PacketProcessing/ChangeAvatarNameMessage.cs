using System;
using System.IO;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x0200006F RID: 111
	internal class ChangeAvatarNameMessage : Message
	{
		// Token: 0x06000325 RID: 805 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public ChangeAvatarNameMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00010BD4 File Offset: 0x0000EDD4
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00010BDC File Offset: 0x0000EDDC
		public string PlayerName { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00010BE5 File Offset: 0x0000EDE5
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00010BED File Offset: 0x0000EDED
		public int PlayerNameLength { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00010BF6 File Offset: 0x0000EDF6
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00010BFE File Offset: 0x0000EDFE
		public byte Unknown1 { get; set; }

		// Token: 0x0600032C RID: 812 RVA: 0x00010C08 File Offset: 0x0000EE08
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.PlayerName = binaryReader.ReadScString();
				this.Unknown1 = binaryReader.ReadByte();
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00010C5C File Offset: 0x0000EE5C
		public override void Process(Level level)
		{
			level.GetPlayerAvatar().SetName(this.PlayerName);
			AvatarNameChangeOkMessage avatarNameChangeOkMessage = new AvatarNameChangeOkMessage(base.Client);
			avatarNameChangeOkMessage.SetAvatarName(this.PlayerName);
			PacketManager.ProcessOutgoingPacket(avatarNameChangeOkMessage);
		}
	}
}

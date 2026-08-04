using System;
using System.IO;
using CRS.Core;
using CRS.Helpers;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000A6 RID: 166
	internal class ChangeAllianceSettingMessage : Message
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public ChangeAllianceSettingMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001B014 File Offset: 0x00019214
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.AllianceDescription = binaryReader.ReadScString();
				this.Unknown1 = binaryReader.ReadByte();
				this.AllianceBadge = binaryReader.ReadVInt();
				this.AllianceType = binaryReader.ReadByte();
				this.AllianceTrophies = binaryReader.ReadVInt();
				this.AllianceOrigin = binaryReader.ReadVInt();
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001B098 File Offset: 0x00019298
		public override void Process(Level level)
		{
			Alliance alliance = ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId());
			alliance.SetAllianceDescription(this.AllianceDescription);
			alliance.SetAllianceBadgeData(this.AllianceBadge);
			alliance.SetAllianceType(this.AllianceType);
			alliance.SetRequiredScore(this.AllianceTrophies);
			alliance.SetAllianceOrigin(this.AllianceOrigin);
		}

		// Token: 0x04000300 RID: 768
		private string AllianceDescription;

		// Token: 0x04000301 RID: 769
		private int AllianceTrophies;

		// Token: 0x04000302 RID: 770
		private int AllianceBadge;

		// Token: 0x04000303 RID: 771
		private byte AllianceType;

		// Token: 0x04000304 RID: 772
		private byte Unknown1;

		// Token: 0x04000305 RID: 773
		private int AllianceOrigin;
	}
}

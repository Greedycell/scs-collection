using System;
using System.IO;
using CRS.Core;
using CRS.Core.Network;
using CRS.Helpers;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000AA RID: 170
	internal class CreateAllianceMessage : Message
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public CreateAllianceMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001B300 File Offset: 0x00019500
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.AllianceName = binaryReader.ReadScString();
				this.AllianceDescription = binaryReader.ReadScString();
				this.Unknown1 = binaryReader.ReadByte();
				this.AllianceBadge = binaryReader.ReadVInt();
				this.AllianceType = binaryReader.ReadByte();
				this.AllianceTrophies = binaryReader.ReadVInt();
				this.AllianceOrigin = binaryReader.ReadVInt();
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001B390 File Offset: 0x00019590
		public override void Process(Level level)
		{
			Alliance alliance = ObjectManager.CreateAlliance(0L);
			alliance.SetAllianceName(this.AllianceName);
			alliance.SetAllianceDescription(this.AllianceDescription);
			alliance.SetAllianceBadgeData(this.AllianceBadge);
			alliance.SetAllianceType(this.AllianceType);
			alliance.SetRequiredScore(this.AllianceTrophies);
			alliance.SetAllianceOrigin(this.AllianceOrigin);
			Console.WriteLine("[CRS]        " + this.AllianceTrophies);
			level.GetPlayerAvatar().SetAllianceId(alliance.GetAllianceId());
			AllianceMemberEntry allianceMemberEntry = new AllianceMemberEntry(level.GetPlayerAvatar().GetId());
			allianceMemberEntry.SetRole(2);
			alliance.AddAllianceMember(allianceMemberEntry);
			PacketManager.Send(new JoinAllianceMessage(base.Client, level.GetPlayerAvatar().GetAllianceId()));
			PacketManager.Send(new AllianceStreamMessage(base.Client, alliance));
		}

		// Token: 0x0400030A RID: 778
		private string AllianceName;

		// Token: 0x0400030B RID: 779
		private string AllianceDescription;

		// Token: 0x0400030C RID: 780
		private int AllianceTrophies;

		// Token: 0x0400030D RID: 781
		private int AllianceBadge;

		// Token: 0x0400030E RID: 782
		private byte AllianceType;

		// Token: 0x0400030F RID: 783
		private byte Unknown1;

		// Token: 0x04000310 RID: 784
		private byte Unknown2;

		// Token: 0x04000311 RID: 785
		private byte Unknown3;

		// Token: 0x04000312 RID: 786
		private int AllianceOrigin;
	}
}

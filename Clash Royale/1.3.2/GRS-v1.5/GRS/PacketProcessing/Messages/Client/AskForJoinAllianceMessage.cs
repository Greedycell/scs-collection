using System;
using System.IO;
using CRS.Core;
using CRS.Core.Network;
using CRS.Helpers;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000A0 RID: 160
	internal class AskForJoinAllianceMessage : Message
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public AskForJoinAllianceMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001AE9C File Offset: 0x0001909C
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.ClanID = binaryReader.ReadInt64WithEndian();
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001AEE4 File Offset: 0x000190E4
		public override void Process(Level level)
		{
			Alliance alliance = ObjectManager.GetAlliance(this.ClanID);
			if (alliance != null && !alliance.IsAllianceFull())
			{
				level.GetPlayerAvatar().SetAllianceId(alliance.GetAllianceId());
				AllianceMemberEntry allianceMemberEntry = new AllianceMemberEntry(level.GetPlayerAvatar().GetId());
				allianceMemberEntry.SetRole(1);
				alliance.AddAllianceMember(allianceMemberEntry);
				PacketManager.Send(new AllianceStreamMessage(base.Client, alliance));
				PacketManager.Send(new JoinAllianceMessage(base.Client, this.ClanID));
			}
		}

		// Token: 0x040002FE RID: 766
		private long ClanID;
	}
}

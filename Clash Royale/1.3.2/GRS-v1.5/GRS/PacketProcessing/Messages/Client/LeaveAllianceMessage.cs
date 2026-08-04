using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CRS.Core;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000B0 RID: 176
	internal class LeaveAllianceMessage : Message
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public LeaveAllianceMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001B620 File Offset: 0x00019820
		public override void Process(Level level)
		{
			ClientAvatar playerAvatar = level.GetPlayerAvatar();
			Alliance alliance = ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId());
			if (playerAvatar.GetAllianceRole() == 2 && alliance.GetAllianceMembers().Count > 1)
			{
				List<AllianceMemberEntry> allianceMembers = alliance.GetAllianceMembers();
				using (IEnumerator<AllianceMemberEntry> enumerator = allianceMembers.Where((AllianceMemberEntry player) => player.GetRole() >= 3).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						enumerator.Current.SetRole(2);
						LeaveAllianceMessage.done = true;
					}
				}
				if (!LeaveAllianceMessage.done)
				{
					int count = alliance.GetAllianceMembers().Count;
					Random random = new Random();
					int num = random.Next(1, count);
					while ((long)num != level.GetPlayerAvatar().GetId())
					{
						num = random.Next(1, count);
					}
					int num2 = 0;
					foreach (AllianceMemberEntry allianceMemberEntry in allianceMembers)
					{
						num2++;
						if (num2 == num)
						{
							allianceMemberEntry.SetRole(2);
							break;
						}
					}
				}
			}
			alliance.RemoveMember(playerAvatar.GetId());
			playerAvatar.SetAllianceId(0L);
			if (alliance.GetAllianceMembers().Count <= 0)
			{
				DatabaseManager.Singelton.RemoveAlliance(alliance);
			}
			PacketManager.Send(new LeaveAllianceOkMessage(base.Client, alliance.GetAllianceId()));
		}

		// Token: 0x04000317 RID: 791
		public static bool done;
	}
}

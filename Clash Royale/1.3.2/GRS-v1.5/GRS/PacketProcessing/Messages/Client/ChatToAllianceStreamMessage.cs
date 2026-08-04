using System;
using System.IO;
using CRS.Core;
using CRS.Core.Network;
using CRS.Helpers;
using CRS.Logic;
using CRS.Logic.StreamEntry;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000A8 RID: 168
	internal class ChatToAllianceStreamMessage : Message
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public ChatToAllianceStreamMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001B1A8 File Offset: 0x000193A8
		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.m_vChatMessage = binaryReader.ReadScString();
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001B1F0 File Offset: 0x000193F0
		public override void Process(Level level)
		{
			if (this.m_vChatMessage.Length > 0)
			{
				ClientAvatar playerAvatar = level.GetPlayerAvatar();
				long allianceId = playerAvatar.GetAllianceId();
				if (allianceId > 0L)
				{
					ChatStreamEntry chatStreamEntry = new ChatStreamEntry();
					chatStreamEntry.SetId((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
					chatStreamEntry.SetAvatar(playerAvatar);
					chatStreamEntry.SetMessage(this.m_vChatMessage);
					Alliance alliance = ObjectManager.GetAlliance(allianceId);
					if (alliance != null)
					{
						alliance.AddChatMessage(chatStreamEntry);
						foreach (Level level2 in ResourcesManager.GetOnlinePlayers())
						{
							if (object.Equals(level2.GetPlayerAvatar().GetAllianceId(), allianceId))
							{
								AllianceStreamEntryMessage allianceStreamEntryMessage = new AllianceStreamEntryMessage(level2.GetClient());
								allianceStreamEntryMessage.SetStreamEntry(chatStreamEntry);
								PacketManager.Send(allianceStreamEntryMessage);
								PacketManager.Send(new AllianceStreamEntryRemovedMessage(base.Client));
							}
						}
					}
				}
			}
		}

		// Token: 0x04000309 RID: 777
		private string m_vChatMessage;
	}
}

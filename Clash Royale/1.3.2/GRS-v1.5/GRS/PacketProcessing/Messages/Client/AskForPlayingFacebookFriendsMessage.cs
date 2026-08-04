using System;
using System.IO;
using CRS.Core.Network;
using CRS.Logic;
using CRS.PacketProcessing.Messages.Server;

namespace CRS.PacketProcessing.Messages.Client
{
	// Token: 0x020000A3 RID: 163
	internal class AskForPlayingFacebookFriendsMessage : Message
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0001ACBA File Offset: 0x00018EBA
		public AskForPlayingFacebookFriendsMessage(Device client, BinaryReader br)
			: base(client, br)
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001AF85 File Offset: 0x00019185
		public override void Process(Level level)
		{
			PacketManager.Send(new FriendListMessage(base.Client));
		}
	}
}

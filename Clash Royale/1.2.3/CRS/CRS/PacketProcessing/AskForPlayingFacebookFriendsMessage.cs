using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	// Token: 0x0200006A RID: 106
	internal class AskForPlayingFacebookFriendsMessage : Message
	{
		// Token: 0x06000319 RID: 793 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public AskForPlayingFacebookFriendsMessage(Client client, BinaryReader br)
			: base(client, br)
		{
			base.Decrypt();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00010AA3 File Offset: 0x0000ECA3
		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new FriendListMessage(base.Client));
		}
	}
}

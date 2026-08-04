using System;
using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	internal class VisitedHomeDataMessage : Message
	{
		public VisitedHomeDataMessage(Client client, Level level)
			: base(client)
		{
			base.SetMessageType(24113);
			this.Player = level.GetPlayerAvatar();
			this.PlayerClient = client;
		}

		public ClientAvatar Player { get; set; }
		public Client PlayerClient { get; set; }

		public override void Encode()
		{
			List<byte> list = new List<byte>();

			list.AddInt32(8);
			list.Add(1);
			list.Add(1);
			list.Add(1);
			list.Add(1);
			list.Add(1);
			list.Add(1);
			list.Add(1);
			list.Add(1);
			list.AddInt32(1);
			// Deck
			{
				list.AddInt32(26000000);
				list.AddInt32(26000001);
				list.AddInt32(26000002);
				list.AddInt32(26000003);
				list.AddInt32(26000004);
				list.AddInt32(26000005);
				list.AddInt32(26000006);
				list.AddInt32(26000007);
			}

			list.AddInt64(Player.GetId()); // HighID, LowID
			list.AddInt32(0);
			list.AddInt32(0);
			list.AddInt32(1);


			list.AddInt32(0);
			list.AddInt64(Player.GetId()); // HighID, LowID
			list.AddInt64(Player.GetId()); // HighID, LowID
			list.AddInt64(Player.GetId()); // HighID, LowID
			list.AddString(Player.GetAvatarName()); // Name
			list.Add(1); // NameSet

			list.AddInt32(0);
			list.AddInt32(54);
			list.AddInt32(1); // Arena
			list.AddInt32(0); // Score

			list.AddInt32(1);
			list.AddInt32(0);
			list.AddInt32(0);
			list.AddInt32(0);
			list.AddInt32(0);
			list.AddInt32(1);
			list.AddInt32(0);
			list.AddInt32(0);
			list.Add(0);
			list.AddInt32(6);
			list.AddInt32(5);

			list.AddInt32(5);
			list.AddInt32(1);
			list.AddInt32(1000000000); // Gold

			list.AddInt32(5);
			list.AddInt32(2);
			list.AddInt32(7);
			list.AddInt32(5);
			list.AddInt32(3);
			list.AddInt32(10);
			list.AddInt32(5);
			list.AddInt32(4);
			list.AddInt32(0);
			list.AddInt32(5);
			list.AddInt32(5);
			list.AddInt32(500000);
			list.AddInt32(0);
			list.AddInt32(7);
			list.AddInt32(60);
			list.AddInt32(7);
			list.AddInt32(9);
			list.AddInt32(60);
			list.AddInt32(8);
			list.AddInt32(9);
			list.AddInt32(60);
			list.AddInt32(9);
			list.AddInt32(9);
			list.AddInt32(60);
			list.AddInt32(4);
			list.AddInt32(1);
			list.AddInt32(60);
			list.AddInt32(5);
			list.AddInt32(1);
			list.AddInt32(60);
			list.AddInt32(6);
			list.AddInt32(1);
			list.AddInt32(60);
			list.AddInt32(10);
			list.AddInt32(1);
			list.AddInt32(1);
			list.AddInt32(60);
			list.AddInt32(10);
			list.AddInt32(1);
			list.AddInt32(1);
			list.AddInt32(5);
			list.AddInt32(8);
			list.AddInt32(9);
			list.AddInt32(7);
			list.AddInt32(26);
			list.AddInt32(0);
			list.AddInt32(11);
			list.AddInt32(26);
			list.AddInt32(1);
			list.AddInt32(8);
			list.AddInt32(26);
			list.AddInt32(3);
			list.AddInt32(9);
			list.AddInt32(26);
			list.AddInt32(13);
			list.AddInt32(13);
			list.AddInt32(26);
			list.AddInt32(14);
			list.AddInt32(6);
			list.AddInt32(28);
			list.AddInt32(0);
			list.AddInt32(2);
			list.AddInt32(26);
			list.AddInt32(12);
			list.AddInt32(4);

			list.AddInt32(1000000000); // Diamonds
			list.AddInt32(1000000000); // FreeDiamonds
			list.AddInt32(0); // Experience
			list.AddInt32(12); // Level
			list.AddInt32(0);

			// 7 = Name already set + no clan
			// 8 = Set name popup + clan
			// 9 = Name already set + clan
			// < 7 =  Set name popup
			var InClan = 0;
			if (InClan == 1)
			{
				list.Add(1);
				list.Add(9);
				list.Add(0);
				list.AddString("Clashers"); // Name
				list.AddInt32(16);
				list.AddInt32(0);
				list.AddInt32(0);
				list.AddInt32(1);
				list.AddInt32(0);
				list.AddInt32(0);
				list.AddInt32(0);
				list.AddInt32(8);
				list.Add(1);
				list.AddInt32(-20);
			}
			else
			{
				list.Add(0);
				list.Add(0); // NameSet

				list.AddInt32(0);
				list.AddInt32(0);
				list.AddInt32(0);
				list.AddInt32(6);
				list.AddInt32(0);
				list.Add(1);
				list.AddInt32(-131170575);
			}

			base.Encrypt(list.ToArray());
		}
	}
}

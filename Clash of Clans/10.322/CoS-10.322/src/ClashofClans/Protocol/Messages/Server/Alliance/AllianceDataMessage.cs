using ClashofClans.Logic;
using ClashofClans.Utilities.Netty;

namespace ClashofClans.Protocol.Messages.Server
{
    public class AllianceDataMessage : PiranhaMessage
    {
        public AllianceDataMessage(Device device) : base(device)
        {
            Id = 25413;
        }

        public override void Encode()
        {
            // AllianceFullEntry
            {
                // AllianceHeaderEntry
                {
					Writer.WriteLong(1); // Id
					Writer.WriteScString("Clashers"); // Name
					Writer.WriteInt(1); // Badge
					Writer.WriteInt(1); // Type
					Writer.WriteInt(1); // MemberCount
					Writer.WriteInt(0); // Score
					Writer.WriteInt(0); // DuelScore
					Writer.WriteInt(0); // RequiredScore
					Writer.WriteInt(0); // RequiredDuelScore
					Writer.WriteInt(1); // WinWarCount
					Writer.WriteInt(1); // LostWarCount
					Writer.WriteInt(1); // DrawWarCount
					Writer.WriteInt(0); // LocaleData
					Writer.WriteInt(0); // WarFrequency
					Writer.WriteInt(32000000); // OriginData
					Writer.WriteInt(0); // ExpPoints 
					Writer.WriteInt(1); // ClanLevel
					Writer.WriteInt(0); // ConsecutiveWinWarCount
					Writer.WriteBoolean(false); // PublicWarLog
					Writer.WriteInt(0);
					Writer.WriteBoolean(false); // AmicalWarsEnabled
					Writer.WriteInt(0);
				}

                Writer.WriteScString("Test description"); // Description
			}

            Writer.WriteInt(1); // MemberCount
            {
                Writer.WriteLong(1); // Id
                Writer.WriteScString("Astral"); // Name
                Writer.WriteInt(2); // AllianceRole (1 = Member, 2 = Leader, 3 = Elder, 4 = Co-Leader)
                Writer.WriteInt(500); // ExpLevel
                Writer.WriteInt(0); // League
                Writer.WriteInt(0); // Score
                Writer.WriteInt(0); // DuelScore
                Writer.WriteInt(0); // DonationCount
                Writer.WriteInt(0); // ReceivedDonationCount
                Writer.WriteInt(0); // Order
                Writer.WriteInt(0); // PreviousOrder
                Writer.WriteInt(0); // OrderVillage2
                Writer.WriteInt(0); // PreviousOrderVillage2
                Writer.WriteInt(0); // CreatedTime
                Writer.WriteInt(0); // WarCooldown
                Writer.WriteInt(0); // WarPreference

                /*if (true)
                {*/
                    Writer.WriteBoolean(true);
                    Writer.WriteLong(1); // HomeId
                /*}
                else
                {
                    Writer.WriteBoolean(false);
                }*/
            }

			Writer.WriteInt(0);
			Writer.WriteInt(0);
        }
    }
}

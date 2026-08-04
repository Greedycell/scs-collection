namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;
    using CRS.Logic;
    using CRS.Logic.Slots.Items;

    #endregion

    internal class Sector_PC : Message
    {
        public const ushort PacketID = 21903;

        public Battle Battle = null;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Sector_PC" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Sector_PC(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            
            this.Writer.Add(false);
            this.Writer.AddRange("00-21-0B-00".HexaToBytes());
            this.Writer.AddRange("19-4B-76-9B".HexaToBytes());
            this.Writer.AddVInt(this.Battle.BattleID); // Battle ID - C0-C5-98-CE-00
            Level pl = this.Client.GetLevel();

            if (this.Battle.Player1 == pl)
            {
                this.Writer.AddRange(this.Battle.Player2.GetPlayerAvatar().Data_Part2());
                this.Writer.AddRange(this.Battle.Player1.GetPlayerAvatar().Data_Part2());
            }
            else
            {
                this.Writer.AddRange(this.Battle.Player1.GetPlayerAvatar().Data_Part2());
                this.Writer.AddRange(this.Battle.Player2.GetPlayerAvatar().Data_Part2());
            }

            this.Writer.Add(0x0F);
            this.Writer.AddVInt((int)pl.GetPlayerAvatar().GetArena());
            this.Writer.Add(0x00);
            this.Writer.Add(0x36);
            this.Writer.AddVInt((int)pl.GetPlayerAvatar().GetArena());

            if (this.Battle.Player1 == pl)
            {
                this.Writer.AddVInt(this.Battle.Player2.GetPlayerAvatar().GetId());
                this.Writer.AddVInt(this.Battle.Player1.GetPlayerAvatar().GetId());
            }
            else
            {
                this.Writer.AddVInt(this.Battle.Player1.GetPlayerAvatar().GetId());
                this.Writer.AddVInt(this.Battle.Player2.GetPlayerAvatar().GetId());
            }

            this.Writer.AddRange(
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-B9-03-C7-7C-00-00-06-06-23-01-23-01-23-01-23-01-23-00-23-00-01-00-01-00-00-01-05-00-05-01-05-02-05-03-05-04-05-05-00-0D-A4-E2-01-9C-8E-03-00-00-C0-7C-00-A4-01-00-00-00-00-02-00-00-00-00-00-0D-AC-36-A4-65-00-00-80-04-00-A4-01-00-00-00-00-01-00-00-00-00-00-0D-AC-36-9C-8E-03-00-00-C0-7C-00-A4-01-00-00-00-00-01-00-00-00-00-00-0D-A4-E2-01-A4-65-00-00-80-04-00-A4-01-00-00-00-00-02-00-00-00-00-00-0D-A8-8C-01-B8-2E-00-00-80-04-00-A4-01-00-00-00-00-00-00-00-00-00-00-04-00-00-05-00-00-00-00-7F-7F-7F-7F-7F-7F-7F-7F-00-0D-A8-8C-01-88-C5-03-00-00-C0-7C-00-A4-01-00-00-00-00-00-00-00-00-00-00-05-04-02-05-79-05-04-01-06-04-03-00-7F-7F-00-00-00-00-05-00-00-00-00-7F-7F-7F-7F-7F-7F-7F-7F-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-B8-15-B8-15-B8-15-B8-15-A0-25-A0-25-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-FE-03"
                    .HexaToBytes());

            this.Writer.AddRange(pl.GetPlayerAvatar().Deck.Hand());

            this.Writer.AddRange(
                "05-04-05-05-02-05-01-05-03-02-05-00-05-02-00-00-00-00-00-00-00-0C-00-00-00-95-E2-B0-AA-0C-00"
                    .HexaToBytes());
            
        }
    }
}
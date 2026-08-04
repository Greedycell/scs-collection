#region Usings



#endregion

namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;
    using CRS.Logic;

    #endregion

    internal class Profile_Data : Message
    {
        public const ushort PacketID = 24113;

        public long UserID = 0;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Profile_Data" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Profile_Data(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            Level pl = this.Client.GetLevel();
            if (this.UserID > 0 && this.UserID != pl.GetPlayerAvatar().GetId())
            {
                // Retrieve..
            }
            else
            {
                
                this.Writer.AddRange("00FF".HexaToBytes()); // Prefixe from Deck
                this.Writer.AddRange(pl.GetPlayerAvatar().Deck.ToBytes());
                this.Writer.AddLong(pl.GetPlayerAvatar().GetId());
                this.Writer.AddRange(pl.GetPlayerAvatar().Data_Part2());
                this.Writer.Add(2);
                this.Writer.AddRange(pl.GetPlayerAvatar().Data_Part2());
                this.Writer.Add(0);
                
            }
        }
    }
}
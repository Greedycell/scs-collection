namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    internal class Clan_Local_Ranking : Message
    {
        public const ushort PacketID = 24402;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Clan_Local_Ranking" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Clan_Local_Ranking(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddInt(0);
        }
    }
}
namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    internal class Matchmaking_Info : Message
    {
        public const ushort PacketID = 24107;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Matchmaking_Info" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Matchmaking_Info(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddInt(200);
        }
    }
}
namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    internal class Clan_Join_Failed : Message
    {
        public const ushort PacketID = 24302;

        public int Reason = 0;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Clan_Join_Failed" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Clan_Join_Failed(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            
            this.Writer.AddInt(this.Reason);
            
        }
    }
}
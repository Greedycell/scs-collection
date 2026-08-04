namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    internal class Matchmake_Failed : Message
    {
        public const ushort PacketID = 24108;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Matchmake_Failed" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Matchmake_Failed(Client _Client)
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
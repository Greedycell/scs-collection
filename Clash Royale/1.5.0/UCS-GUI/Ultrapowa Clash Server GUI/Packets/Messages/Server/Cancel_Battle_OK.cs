namespace CRS.Packets
{
    #region Usings



    #endregion

    internal class Cancel_Battle_OK : Message
    {
        public const ushort PacketID = 24125;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Cancel_Battle_OK" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Cancel_Battle_OK(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.Add(1);
        }
    }
}
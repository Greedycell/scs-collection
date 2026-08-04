namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    internal class SetDeviceTokenMessage : Message
    {
        public const ushort PacketID = 20113;

        /// <summary>
        ///     Initialize a new instance of the
        ///     <see cref="SetDeviceTokenMessage" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public SetDeviceTokenMessage(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddString("12345678910112548950");
        }
    }
}
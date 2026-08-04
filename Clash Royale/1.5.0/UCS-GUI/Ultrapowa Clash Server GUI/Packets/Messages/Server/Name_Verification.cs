namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    internal class Name_Verification : Message
    {
        public const ushort PacketID = 20300;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Name_Verification" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Name_Verification(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            
            this.Writer.Add(0);
            this.Writer.AddInt(0);
            this.Writer.AddString(string.Empty);
            
        }
    }
}
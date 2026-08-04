namespace CRS.Packets
{
    #region Usings



    #endregion

    internal class Royale_TV_Data : Message
    {
        public const ushort PacketID = 20000;

        public int Arena = 0;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Royale_TV_Data" />class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Royale_TV_Data(Client _Client)
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
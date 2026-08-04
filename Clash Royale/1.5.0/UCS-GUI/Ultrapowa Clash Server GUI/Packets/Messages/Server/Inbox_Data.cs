namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    internal class Inbox_Data : Message
    {
        public const ushort PacketID = 24445;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Inbox_Data" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Inbox_Data(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            
            this.Writer.AddInt(1);

            this.Writer.AddString(
                "https://56f230c6d142ad8a925f-b174a1d8fb2cf6907e1c742c46071d76.ssl.cf2.rackcdn.com/inbox/ClashRoyale_logo_small.png");
            this.Writer.AddString("ClashRoyaleSpain");
            this.Writer.AddString("Bienvenid@ a Clash Royale Spain - CRS!");
            this.Writer.AddString("Visítanos en:");
            this.Writer.AddString("http://www.crs.es/");
            this.Writer.AddString(string.Empty);
            this.Writer.AddString(string.Empty);
            this.Writer.AddString("http://<asset_path_update>");
            
        }
    }
}
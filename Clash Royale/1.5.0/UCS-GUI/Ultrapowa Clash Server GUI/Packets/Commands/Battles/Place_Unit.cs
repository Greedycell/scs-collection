namespace CRS.Packets.Commands.Battles
{
    #region Usings

    using CRS.Extensions.List;

    using Client = CRS.Packets.Client;

    #endregion

    internal class Place_Unit : Command
    {
        public const int CommandID = 3;

        public long Sender = 0;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Place_Unit" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        public Place_Unit(Client _Client)
            : base(_Client)
        {
            this.ID = CommandID;
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddRange("01-A8-01-7F".HexaToBytes());
            this.Writer.AddVInt(this.Sender); // 11-BB-AE-F2-06
            this.Writer.AddRange("05-83-EA-E5-18-01".HexaToBytes());
            this.Writer.AddRange("1A-03".HexaToBytes());
            this.Writer.AddRange("00-00-01-00-09-00-94-46-8C-EF-02".HexaToBytes());
        }
    }
}
namespace CRS.Packets.Commands.Client
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    using Client = CRS.Packets.Client;

    #endregion

    class New_Card_Seen : Command
    {
        public int Tick;

        /// <summary>
        ///     Initialize a new instance of the <see cref="New_Card_Seen" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public New_Card_Seen(Reader _Reader, Client _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // New_Card_Seen.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();
            Debug.Write("Tick: " + this.Tick);
        }
    }
}